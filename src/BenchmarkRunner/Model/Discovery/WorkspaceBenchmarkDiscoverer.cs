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
                Namespace = methodSymbol.ContainingNamespace.Name,
                ClassName = methodSymbol.ContainingType.Name,
                MethodName = methodSymbol.Name
            };
        }
    }
}
