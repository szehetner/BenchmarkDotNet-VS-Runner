using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BenchmarkRunner.Model
{
    public class BenchmarkNodeBuilder
    {
        private readonly Grouping _grouping;

        public BenchmarkNodeBuilder(Grouping grouping)
        {
            _grouping = grouping;
        }

        public void RebuildNodes(ObservableCollection<BenchmarkTreeNode> rootList, IEnumerable<Benchmark> benchmarks)
        {
            rootList.Clear();

            foreach (Benchmark benchmark in benchmarks)
            {
                BuildHierarchy(rootList, benchmark);
            }
        }

        private void BuildHierarchy(ObservableCollection<BenchmarkTreeNode> nodeList, Benchmark benchmark)
        {
            switch (_grouping)
            {
                case Grouping.ProjectClass:
                    BuildProjectClassHierarchy(nodeList, benchmark);
                    break;

                case Grouping.ProjectNamespaceClass:
                    BuildNamespaceClassHierarchy(nodeList, benchmark);
                    break;

                case Grouping.ProjectCategoryClass:
                    BuildCategoryClassHierarchy(nodeList, benchmark);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void BuildProjectClassHierarchy(ObservableCollection<BenchmarkTreeNode> nodeList, Benchmark benchmark)
        {
            var projectNode = GetOrInsertNode(nodeList, benchmark.Project, () => CreateProjectNode(benchmark));
            var classNode = GetOrInsertNode(projectNode.Nodes, benchmark.ClassName, () => CreateClassNode(benchmark));
            GetOrInsertNode(classNode.Nodes, benchmark.MethodName, () => CreateMethodNode(benchmark));
        }

        private void BuildNamespaceClassHierarchy(ObservableCollection<BenchmarkTreeNode> nodeList, Benchmark benchmark)
        {
            var projectNode = GetOrInsertNode(nodeList, benchmark.Project, () => CreateProjectNode(benchmark));

            BenchmarkTreeNode lastNamespaceNode = projectNode;
            string[] namespaceParts = benchmark.Namespace.Split('.');
            foreach (var namespacePart in namespaceParts)
            {
                lastNamespaceNode = GetOrInsertNode(lastNamespaceNode.Nodes, namespacePart, () => CreateNamespaceNode(benchmark, namespacePart));
            }

            var classNode = GetOrInsertNode(lastNamespaceNode.Nodes, benchmark.ClassName, () => CreateClassNode(benchmark));
            GetOrInsertNode(classNode.Nodes, benchmark.MethodName, () => CreateMethodNode(benchmark));
        }

        private void BuildCategoryClassHierarchy(ObservableCollection<BenchmarkTreeNode> nodeList, Benchmark benchmark)
        {
            var projectNode = GetOrInsertNode(nodeList, benchmark.Project, () => CreateProjectNode(benchmark));
            foreach (string category in benchmark.Categories)
            {
                var categoryNode = GetOrInsertNode(projectNode.Nodes, category, () => CreateCategoryNode(benchmark, category));
                var classNode = GetOrInsertNode(categoryNode.Nodes, benchmark.ClassName, () => CreateClassNode(benchmark));
                GetOrInsertNode(classNode.Nodes, benchmark.MethodName, () => CreateMethodNode(benchmark));
            }
        }

        private BenchmarkTreeNode GetOrInsertNode(ObservableCollection<BenchmarkTreeNode> collection, string nodeName, Func<BenchmarkTreeNode> createFunc)
        {
            int previousIndex = 0;
            foreach (var currentNode in collection)
            {
                int comparisonResult = string.Compare(currentNode.NodeName, nodeName);

                if (comparisonResult == 0)
                {
                    return currentNode;
                }
                else if (comparisonResult < 0)
                {
                    break;
                }

                previousIndex++;
            }

            var newNode = createFunc();
            collection.Insert(previousIndex, newNode);
            return newNode;
        }

        private BenchmarkTreeNode CreateProjectNode(Benchmark benchmark)
        {
            return new ProjectBenchmarkTreeNode(benchmark.Project);
        }

        private BenchmarkTreeNode CreateNamespaceNode(Benchmark benchmark, string namespacePart)
        {
            return new NamespaceBenchmarkTreeNode(benchmark.Project, namespacePart);
        }

        private BenchmarkTreeNode CreateCategoryNode(Benchmark benchmark, string category)
        {
            return new CategoryBenchmarkTreeNode(benchmark.Project, category);
        }

        private BenchmarkTreeNode CreateClassNode(Benchmark benchmark)
        {
            return new ClassBenchmarkTreeNode(benchmark.Project, benchmark.ClassName);
        }

        private BenchmarkTreeNode CreateMethodNode(Benchmark benchmark)
        {
            return new MethodBenchmarkTreeNode(benchmark.Project, benchmark.MethodName);
        }
    }
}
