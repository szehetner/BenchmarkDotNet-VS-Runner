using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BenchmarkRunner.Model
{
    public class BenchmarkTreeNode : INotifyPropertyChanged
    {
        public string ProjectName { get; set; }
        public string NodeName { get; set; }
        public string FullName { get; set; }
        public ObservableCollection<BenchmarkTreeNode> Nodes { get; set; }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                OnPropertyChanged();
            }
        }

        public BenchmarkTreeNode(string projectName, string nodeName)
        {
            ProjectName = projectName;
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
        public ProjectBenchmarkTreeNode(string projectName) : base(projectName, projectName)
        {
        }
    }

    public class NamespaceBenchmarkTreeNode : BenchmarkTreeNode
    {
        public NamespaceBenchmarkTreeNode(string projectName, string namespaceName) : base(projectName, namespaceName)
        {
        }
    }

    public class CategoryBenchmarkTreeNode : BenchmarkTreeNode
    {
        public CategoryBenchmarkTreeNode(string projectName, string categoryName) : base(projectName, categoryName)
        {
        }
    }

    public class ClassBenchmarkTreeNode : BenchmarkTreeNode
    {
        public ClassBenchmarkTreeNode(string projectName, string className) : base(projectName, className)
        {
        }
    }

    public class MethodBenchmarkTreeNode : BenchmarkTreeNode
    {
        public MethodBenchmarkTreeNode(string projectName, string methodName) : base(projectName, methodName)
        {
        }
    }
}
