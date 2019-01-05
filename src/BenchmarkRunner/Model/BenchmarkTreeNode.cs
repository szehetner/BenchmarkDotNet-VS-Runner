using BenchmarkRunner.Controls.Helper;
using Microsoft.CodeAnalysis;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BenchmarkRunner.Model
{
    public class BenchmarkTreeNode : INotifyPropertyChanged
    {
        public string NodeName { get; set; }
        public int TotalNodeCount
        {
            get
            {
                return _totalNodeCount;
            }
            set
            {
                _totalNodeCount = value;
                OnPropertyChanged(nameof(NodeCountText));
            }
        }
        public string NodeCountText => "(" + TotalNodeCount + ")";

        private bool _isExpanded = false;
        private bool _isSelected = false;
        private int _totalNodeCount;
        private ObservableCollection<BenchmarkTreeNode> _nodes;

        public ObservableCollection<BenchmarkTreeNode> Nodes
        {
            get { return _nodes; }
            set
            {
                _nodes = value;
                OnPropertyChanged();
            }
        }

        public BenchmarkTreeViewModel TreeViewModel { get; set; }
        public BenchmarkTreeNode Parent { get; set; }

        public AsyncDelegateCommand RunSelectedCommand { get; }
        public AsyncDelegateCommand DryRunSelectedCommand { get; }
        public AsyncDelegateCommand GoToCodeCommand { get; }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                if (_isSelected)
                    TreeViewModel.SelectedBenchmark = this;

                OnPropertyChanged();
            }
        }

        public string ProjectName
        {
            get
            {
                BenchmarkTreeNode current = this;
                while (!(current is ProjectBenchmarkTreeNode))
                    current = current.Parent;

                return current.NodeName;
            }
        }

        public virtual bool SupportsGoToCode => false;
        public virtual Benchmark Benchmark => null;
        public virtual ISymbol TargetSymbol => null;

        public BenchmarkTreeNode(BenchmarkTreeNode parent, string nodeName)
        {
            Parent = parent;
            NodeName = nodeName;
            Nodes = new ObservableCollection<BenchmarkTreeNode>();

            RunSelectedCommand = new AsyncDelegateCommand(async () => await TreeViewModel.CommandHandler.RunAsync(false));
            DryRunSelectedCommand = new AsyncDelegateCommand(async () => await TreeViewModel.CommandHandler.RunAsync(true));
            GoToCodeCommand = new AsyncDelegateCommand(async () => await TreeViewModel.CommandHandler.GoToCodeAsync(Benchmark.Project, TargetSymbol), () => SupportsGoToCode);
        }

        public void IncrementChildNodeCount()
        {
            TotalNodeCount++;
            Parent?.IncrementChildNodeCount();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ProjectBenchmarkTreeNode : BenchmarkTreeNode
    {
        private bool _isLoading = true;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public ProjectBenchmarkTreeNode(string projectName) : base(null, projectName)
        {
            IsLoading = true;
        }
    }

    public class NamespaceBenchmarkTreeNode : BenchmarkTreeNode
    {
        public NamespaceBenchmarkTreeNode(BenchmarkTreeNode parent, string namespaceName) : base(parent, namespaceName)
        {
        }

        public string FullName
        {
            get
            {
                string fullName = "";
                BenchmarkTreeNode current = this;
                while (current is NamespaceBenchmarkTreeNode ns)
                {
                    fullName = current.NodeName + "." + fullName;
                    current = current.Parent;
                }

                return fullName;
            }
        }
    }

    public class CategoryBenchmarkTreeNode : BenchmarkTreeNode
    {
        public CategoryBenchmarkTreeNode(BenchmarkTreeNode parent, string categoryName) : base(parent, categoryName)
        {
        }
    }

    public class ClassBenchmarkTreeNode : BenchmarkTreeNode
    {
        private Benchmark _benchmark;

        public ClassBenchmarkTreeNode(BenchmarkTreeNode parent, Benchmark benchmark, string className) : base(parent, className)
        {
            _benchmark = benchmark;
        }

        public string FullName => _benchmark.Namespace + "." + _benchmark.ClassName;

        public override bool SupportsGoToCode => true;
        public override Benchmark Benchmark => _benchmark;
        public override ISymbol TargetSymbol => _benchmark.ClassSymbol;
    }

    public class MethodBenchmarkTreeNode : BenchmarkTreeNode
    {
        private Benchmark _benchmark;

        public MethodBenchmarkTreeNode(BenchmarkTreeNode parent, Benchmark benchmark, string methodName) : base(parent, methodName)
        {
            _benchmark = benchmark;

            Parent?.IncrementChildNodeCount();
        }

        public string FullName => _benchmark.Namespace + "." + _benchmark.ClassName + "." + _benchmark.MethodName;

        public override bool SupportsGoToCode => true;
        public override Benchmark Benchmark => _benchmark;
        public override ISymbol TargetSymbol => _benchmark.MethodSymbol;
    }
}
