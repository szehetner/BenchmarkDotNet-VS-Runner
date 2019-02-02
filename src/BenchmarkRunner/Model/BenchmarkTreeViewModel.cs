using BenchmarkRunner.Controls.Helper;
using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BenchmarkRunner.Model
{
    public class BenchmarkTreeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<BenchmarkTreeNode> _nodes = new ObservableCollection<BenchmarkTreeNode>();

        public ObservableCollection<BenchmarkTreeNode> Nodes
        {
            get { return _nodes; }
            set
            {
                _nodes = value;
                OnPropertyChanged();
            }
        }
        public CommandHandler CommandHandler { get; set; }

        private List<Benchmark> _discoveredBenchmarks = new List<Benchmark>();
        
        public BenchmarkTreeViewModel(CommandHandler commandHandler)
        {
            CommandHandler = commandHandler;
        }

        public async Task RefreshAsync(IBenchmarkDiscoverer discoverer, Grouping grouping)
        {
            _discoveredBenchmarks.Clear();
            var benchmarks = discoverer.FindBenchmarks().Select(b =>
                    {
                        _discoveredBenchmarks.Add(b);
                        return b;
                    });

            BenchmarkTreeNode previousSelection = SelectedBenchmark;
            
            BenchmarkNodeBuilder nodeBuilder = new BenchmarkNodeBuilder(this, grouping);
            await nodeBuilder.RebuildNodesAsync(benchmarks);

            if (Nodes.Sum(n => n.TotalNodeCount) <= 20)
            {
                ExpandAll();
            }
            if (previousSelection != null)
                ExpandAndSelectNode(previousSelection);
        }

        private void ExpandAndSelectNode(BenchmarkTreeNode previousSelection)
        {
            List<BenchmarkTreeNode> previousPath = BuildPreviousPath(previousSelection);

            ObservableCollection<BenchmarkTreeNode> currentNodes = Nodes;
            for (int i = 0; i < previousPath.Count; i++)
            {
                BenchmarkTreeNode previousNode = previousPath[i];
                var newNode = currentNodes.FirstOrDefault(c => c.NodeName == previousNode.NodeName);
                if (newNode == null)
                    return;

                newNode.IsExpanded = true;
                if (i == previousPath.Count - 1)
                {
                    newNode.IsSelected = true;
                    SelectedBenchmark = newNode;
                }

                currentNodes = newNode.Nodes;
            }
        }

        private static List<BenchmarkTreeNode> BuildPreviousPath(BenchmarkTreeNode previousSelection)
        {
            List<BenchmarkTreeNode> previousPath = new List<BenchmarkTreeNode>();
            previousPath.Add(previousSelection);

            BenchmarkTreeNode current = previousSelection;
            while (current.Parent != null)
            {
                previousPath.Add(current.Parent);
                current = current.Parent;
            }
            previousPath.Reverse();
            return previousPath;
        }

        public async Task SetGroupingAsync(Grouping grouping)
        {
            if (_discoveredBenchmarks.Count == 0)
                return;

            BenchmarkNodeBuilder nodeBuilder = new BenchmarkNodeBuilder(this, grouping);
            await nodeBuilder.RebuildNodesAsync(_discoveredBenchmarks);
        }
        
        private BenchmarkTreeNode _selectedBenchmark;
        public BenchmarkTreeNode SelectedBenchmark
        {
            get { return _selectedBenchmark; }
            set
            {
                _selectedBenchmark = value;
                OnPropertyChanged();
            }
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

        private bool _isLoading = false;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsFinished));
                }
            }
        }

        private bool _isEmpty = false;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                if (_isEmpty != value)
                {
                    _isEmpty = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsFinished));
                }
            }
        }
        
        public bool IsFinished => !_isLoading && !_isEmpty;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
