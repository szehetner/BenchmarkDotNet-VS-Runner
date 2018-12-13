using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace BenchmarkRunner.Model
{
    public class BenchmarkNodeBuilder
    {
        private readonly BenchmarkTreeViewModel _treeViewModel;
        private readonly Grouping _grouping;

        public BenchmarkNodeBuilder(BenchmarkTreeViewModel treeViewModel, Grouping grouping)
        {
            _treeViewModel = treeViewModel;
            _grouping = grouping;
        }

        public async Task RebuildNodesAsync(IEnumerable<Benchmark> benchmarks)
        {
            BufferBlock<Benchmark> buffer = new BufferBlock<Benchmark>();

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(() => RunDiscovery(benchmarks, buffer));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            _treeViewModel.Nodes = new ObservableCollection<BenchmarkTreeNode>();
            
            ProjectBenchmarkTreeNode lastProjectNode = null;
            while (await buffer.OutputAvailableAsync())
            {
                Benchmark currentBenchmark = await buffer.ReceiveAsync();
                var projectNode = (ProjectBenchmarkTreeNode)GetOrInsertNode(_treeViewModel.Nodes, currentBenchmark.ProjectName, () => CreateProjectNode(currentBenchmark));
                if (lastProjectNode != null && projectNode != lastProjectNode)
                    lastProjectNode.IsLoading = false;

                BuildHierarchy(_treeViewModel.Nodes, projectNode, currentBenchmark);
                lastProjectNode = projectNode;
            }
            lastProjectNode.IsLoading = false;
        }

        private void RunDiscovery(IEnumerable<Benchmark> benchmarks, BufferBlock<Benchmark> buffer)
        {
            foreach (var benchmark in benchmarks)
            {
                buffer.Post(benchmark);
            }
            buffer.Complete();
        }

        private void BuildHierarchy(ObservableCollection<BenchmarkTreeNode> nodeList, ProjectBenchmarkTreeNode projectNode, Benchmark benchmark)
        {
            switch (_grouping)
            {
                case Grouping.ProjectClass:
                    BuildProjectClassHierarchy(nodeList, projectNode, benchmark);
                    break;

                case Grouping.ProjectNamespaceClass:
                    BuildNamespaceClassHierarchy(nodeList, projectNode, benchmark);
                    break;

                case Grouping.ProjectCategoryClass:
                    BuildCategoryClassHierarchy(nodeList, projectNode, benchmark);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void BuildProjectClassHierarchy(ObservableCollection<BenchmarkTreeNode> nodeList, ProjectBenchmarkTreeNode projectNode, Benchmark benchmark)
        {
            var classNode = GetOrInsertNode(projectNode, benchmark.ClassName, p => CreateClassNode(p, benchmark));
            GetOrInsertNode(classNode, benchmark.MethodName, p => CreateMethodNode(p, benchmark));
        }

        private void BuildNamespaceClassHierarchy(ObservableCollection<BenchmarkTreeNode> nodeList, ProjectBenchmarkTreeNode projectNode, Benchmark benchmark)
        {
            BenchmarkTreeNode lastNamespaceNode = projectNode;
            string[] namespaceParts = benchmark.Namespace.Split('.');
            foreach (var namespacePart in namespaceParts)
            {
                lastNamespaceNode = GetOrInsertNode(lastNamespaceNode, namespacePart, p => CreateNamespaceNode(p, namespacePart));
            }

            var classNode = GetOrInsertNode(lastNamespaceNode, benchmark.ClassName, p => CreateClassNode(p, benchmark));
            GetOrInsertNode(classNode, benchmark.MethodName, p => CreateMethodNode(p, benchmark));
        }

        private void BuildCategoryClassHierarchy(ObservableCollection<BenchmarkTreeNode> nodeList, ProjectBenchmarkTreeNode projectNode, Benchmark benchmark)
        {
            foreach (string category in benchmark.Categories)
            {
                var categoryNode = GetOrInsertNode(projectNode, category, p => CreateCategoryNode(p, category));
                var classNode = GetOrInsertNode(categoryNode, benchmark.ClassName, p => CreateClassNode(p, benchmark));
                GetOrInsertNode(classNode, benchmark.MethodName, p => CreateMethodNode(p, benchmark));
            }
        }

        private BenchmarkTreeNode GetOrInsertNode(BenchmarkTreeNode parent, string nodeName, Func<BenchmarkTreeNode, BenchmarkTreeNode> createFunc)
        {
            int previousIndex = 0;
            foreach (var currentNode in parent.Nodes)
            {
                int comparisonResult = string.Compare(nodeName, currentNode.NodeName);

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

            var newNode = createFunc(parent);
            newNode.TreeViewModel = _treeViewModel;
            parent.Nodes.Insert(previousIndex, newNode);
            return newNode;
        }

        private BenchmarkTreeNode GetOrInsertNode(ObservableCollection<BenchmarkTreeNode> collection, string nodeName, Func<BenchmarkTreeNode> createFunc)
        {
            int previousIndex = 0;
            foreach (var currentNode in collection)
            {
                int comparisonResult = string.Compare(nodeName, currentNode.NodeName);

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
            newNode.TreeViewModel = _treeViewModel;
            collection.Insert(previousIndex, newNode);
            return newNode;
        }

        private BenchmarkTreeNode CreateProjectNode(Benchmark benchmark)
        {
            return new ProjectBenchmarkTreeNode(benchmark.ProjectName);
        }

        private BenchmarkTreeNode CreateNamespaceNode(BenchmarkTreeNode parent, string namespacePart)
        {
            return new NamespaceBenchmarkTreeNode(parent, namespacePart);
        }

        private BenchmarkTreeNode CreateCategoryNode(BenchmarkTreeNode parent, string category)
        {
            return new CategoryBenchmarkTreeNode(parent, category);
        }

        private BenchmarkTreeNode CreateClassNode(BenchmarkTreeNode parent, Benchmark benchmark)
        {
            return new ClassBenchmarkTreeNode(parent, benchmark, benchmark.ClassName);
        }

        private BenchmarkTreeNode CreateMethodNode(BenchmarkTreeNode parent, Benchmark benchmark)
        {
            return new MethodBenchmarkTreeNode(parent, benchmark, benchmark.MethodName);
        }
    }
}
