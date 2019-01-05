using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.LanguageServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BenchmarkRunner.Model
{
    public class WorkspaceBenchmarkDiscoverer : IBenchmarkDiscoverer
    {
        private readonly VisualStudioWorkspace _workspace;
        private List<Tuple<Project, Compilation>> _projects;
        private Dictionary<Project, Task<BenchmarkResultCollection>> _results;

        public WorkspaceBenchmarkDiscoverer(VisualStudioWorkspace workspace)
        {
            _workspace = workspace;
            _results = new Dictionary<Project, Task<BenchmarkResultCollection>>();
        }

        public async Task InitializeAsync()
        {
            _projects = new List<Tuple<Project, Compilation>>();
            foreach (var project in _workspace.CurrentSolution.Projects)
            {
                _results.Add(project, BenchmarkResultCollection.CreateAsync(project.Name));

                _projects.Add(Tuple.Create(project, await project.GetCompilationAsync()));
            }

            await Task.WhenAll(_results.Values);
        }
        
        public IEnumerable<Benchmark> FindBenchmarks()
        {
            var methodVisitor = new MethodVisitor();

            foreach (var project in _projects)
            {
                foreach (var syntaxTree in project.Item2.SyntaxTrees)
                {
                    methodVisitor.Visit(syntaxTree.GetRoot());
                    if (methodVisitor.BenchmarkMethods.Count == 0)
                        continue;

                    var semanticModel = project.Item2.GetSemanticModel(syntaxTree);
                    foreach (var methodNode in methodVisitor.BenchmarkMethods)
                    {
                        var benchmark = CreateBenchmark(project.Item1, semanticModel, methodNode);
                        yield return benchmark;
                    }
                    methodVisitor.BenchmarkMethods.Clear();
                }
            }
        }

        private Benchmark CreateBenchmark(Project project, SemanticModel semanticModel, MethodDeclarationSyntax methodNode)
        {
            var methodSymbol = semanticModel.GetDeclaredSymbol(methodNode) as IMethodSymbol;
            if (methodSymbol == null)
                return null;

            return new Benchmark
            {
                ProjectName = project.Name,
                Namespace = methodSymbol.ContainingNamespace.ToString(),
                ClassName = methodSymbol.ContainingType.Name,
                MethodName = methodSymbol.Name,
                Categories = GetCategories(methodSymbol) ?? _emptyCategoryList,
                MethodSymbol = methodSymbol,
                ClassSymbol = methodSymbol.ContainingType,
                Project = project,
                LastResult = GetResult(project, methodSymbol)
            };
        }

        private BenchmarkResult GetResult(Project project, IMethodSymbol methodSymbol)
        {
            if (!_results.TryGetValue(project, out var resultTask))
                return null;

            string benchmarkName = methodSymbol.ContainingNamespace.ToString() + "." + methodSymbol.ContainingType.Name + "." + methodSymbol.Name;
#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
            return resultTask.Result.GetResult(benchmarkName);
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits
        }
        
        private static List<string> _emptyCategoryList = new List<string> { "<empty> " };
        private List<string> GetCategories(IMethodSymbol methodSymbol)
        {
            List<string> categories = null;
            System.Collections.Immutable.ImmutableArray<AttributeData> attributes = methodSymbol.GetAttributes();
            if (attributes != null && attributes.Length > 0)
            {
                foreach (var attribute in attributes)
                {
                    if (attribute.AttributeClass.Name != "BenchmarkCategoryAttribute")
                        continue;

                    if (attribute.ConstructorArguments.Length == 0)
                        continue;

                    foreach (var argument in attribute.ConstructorArguments)
                    {
                        if (argument.Kind == TypedConstantKind.Array)
                        {
                            categories = ProcessArrayCategoryArgument(categories, argument);
                        }
                        else
                        {
                            categories = ProcessConstantCategoryArgument(categories, argument);
                        }
                    }
                }
            }

            return categories;
        }

        private static List<string> ProcessArrayCategoryArgument(List<string> categories, TypedConstant argument)
        {
            foreach (var constant in argument.Values)
            {
                categories = ProcessConstantCategoryArgument(categories, constant);
            }

            return categories;
        }

        private static List<string> ProcessConstantCategoryArgument(List<string> categories, TypedConstant constant)
        {
            if (constant.Type.SpecialType == SpecialType.System_String)
            {
                if (constant.Value != null)
                {
                    if (categories == null)
                        categories = new List<string>();

                    categories.Add(constant.Value.ToString());
                }
            }

            return categories;
        }
    }
}
