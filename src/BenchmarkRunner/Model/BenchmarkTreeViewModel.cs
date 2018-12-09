using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BenchmarkRunner.Model
{
    public class BenchmarkTreeViewModel
    {
        public ObservableCollection<BenchmarkTreeNode> Nodes { get; set; } = new ObservableCollection<BenchmarkTreeNode>();

        private List<Benchmark> _discoveredBenchmarks = new List<Benchmark>();

        public void Refresh(WorkspaceBenchmarkDiscoverer discoverer, Grouping grouping)
        {
            _discoveredBenchmarks.Clear();
            var benchmarks = discoverer.FindBenchmarks().Select(b =>
                    {
                        _discoveredBenchmarks.Add(b);
                        return b;
                    });

            BenchmarkNodeBuilder nodeBuilder = new BenchmarkNodeBuilder(grouping);
            nodeBuilder.RebuildNodes(Nodes, benchmarks);
        }

        public void SetGrouping(Grouping grouping)
        {
            if (_discoveredBenchmarks.Count == 0)
                return;

            BenchmarkNodeBuilder nodeBuilder = new BenchmarkNodeBuilder(grouping);
            nodeBuilder.RebuildNodes(Nodes, _discoveredBenchmarks);
        }

        public void ExpandAll()
        {
            SetExpansion(Nodes, true);
        }

        public void CollapseAll()
        {
            SetExpansion(Nodes, false);
        }

        private void SetExpansion(ObservableCollection<BenchmarkTreeNode> nodes, bool isExpanded)
        {
            foreach (var node in nodes)
            {
                node.IsExpanded = isExpanded;

                SetExpansion(node.Nodes, isExpanded);
            }
        }
    }
}
