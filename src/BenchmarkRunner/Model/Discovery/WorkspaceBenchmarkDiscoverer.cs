using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.LanguageServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BenchmarkRunner.Model
{
    public class WorkspaceBenchmarkDiscoverer : IBenchmarkDiscoverer
    {
        private readonly VisualStudioWorkspace _workspace;
        private List<Tuple<Project, Compilation>> _projects;

        public WorkspaceBenchmarkDiscoverer(VisualStudioWorkspace workspace)
        {
            _workspace = workspace;
        }

        public async Task InitializeAsync()
        {
            _projects = new List<Tuple<Project, Compilation>>();
            foreach (var project in _workspace.CurrentSolution.Projects)
            {
                _projects.Add(Tuple.Create(project, await project.GetCompilationAsync()));
            }
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
                Project = project.Name,
                Namespace = methodSymbol.ContainingNamespace.ToString(),
                ClassName = methodSymbol.ContainingType.Name,
                MethodName = methodSymbol.Name,
                Categories = GetCategories(methodSymbol)
            };
        }

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
