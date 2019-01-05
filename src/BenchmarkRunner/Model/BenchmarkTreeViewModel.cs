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

            BenchmarkNodeBuilder nodeBuilder = new BenchmarkNodeBuilder(this, grouping);
            await nodeBuilder.RebuildNodesAsync(benchmarks);
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
