using BenchmarkRunner.Controls.Helper;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BenchmarkRunner.Model
{
    public class BenchmarkTreeNode : INotifyPropertyChanged
    {
        public string NodeName { get; set; }
        public int TotalNodeCount { get; set; }
        public string NodeCountText => "(" + TotalNodeCount + ")";
        public ObservableCollection<BenchmarkTreeNode> Nodes { get; set; }

        public BenchmarkTreeViewModel TreeViewModel { get; set; }
        public BenchmarkTreeNode Parent { get; set; }

        public AsyncDelegateCommand RunSelectedCommand { get; }
        public AsyncDelegateCommand GoToCodeCommand { get; }

        private bool _isExpanded = true;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                OnPropertyChanged();
            }
        }

        private bool _isSelected = true;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
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

        public BenchmarkTreeNode(BenchmarkTreeNode parent, string nodeName)
        {
            Parent = parent;
            NodeName = nodeName;
            Nodes = new ObservableCollection<BenchmarkTreeNode>();

            RunSelectedCommand = new AsyncDelegateCommand(async () => await TreeViewModel.CommandHandler.RunAsync(false));
            GoToCodeCommand = new AsyncDelegateCommand(async () => await TreeViewModel.CommandHandler.GoToCodeAsync());
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
        public ProjectBenchmarkTreeNode(string projectName) : base(null, projectName)
        {
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
    }
}
