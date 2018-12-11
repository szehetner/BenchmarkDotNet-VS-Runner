using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BenchmarkRunner.Model
{
    public class BenchmarkTreeNode : INotifyPropertyChanged
    {
        public string NodeName { get; set; }
        public string NodeCount => "(" + Nodes.Count + ")";
        public ObservableCollection<BenchmarkTreeNode> Nodes { get; set; }

        public BenchmarkTreeNode Parent { get; set; }

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
        }

        public string FullName => _benchmark.Namespace + "." + _benchmark.ClassName + "." + _benchmark.MethodName;
    }
}
