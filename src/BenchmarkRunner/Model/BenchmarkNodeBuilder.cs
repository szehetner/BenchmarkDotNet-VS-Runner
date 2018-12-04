using System;
using System.Collections.ObjectModel;

namespace BenchmarkRunner.Model
{
    public class BenchmarkNodeBuilder
    {
        public void RebuildNodes(ObservableCollection<BenchmarkTreeNode> rootList, IBenchmarkDiscoverer benchmarkDiscoverer)
        {
            rootList.Clear();

            foreach (Benchmark benchmark in benchmarkDiscoverer.FindBenchmarks())
            {
                var projectNode = GetOrInsertNode(rootList, benchmark.Project, () => CreateProjectNode(benchmark));

                // TODO: insert namespace
                var classNode = GetOrInsertNode(projectNode.Nodes, benchmark.ClassName, () => CreateClassNode(benchmark));
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
