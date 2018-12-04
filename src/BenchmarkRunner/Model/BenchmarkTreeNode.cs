using System.Collections.ObjectModel;

namespace BenchmarkRunner.Model
{
    public class BenchmarkTreeNode
    {
        public string ProjectName { get; set; }
        public string NodeName { get; set; }
        public string FullName { get; set; }
        public ObservableCollection<BenchmarkTreeNode> Nodes { get; set; }

        public BenchmarkTreeNode(string nodeName, string projectName)
        {
            NodeName = nodeName;
            ProjectName = projectName;
            Nodes = new ObservableCollection<BenchmarkTreeNode>();
        }
    }

    public class ProjectBenchmarkTreeNode : BenchmarkTreeNode
    {
        public ProjectBenchmarkTreeNode(string projectName) : base(projectName, projectName)
        {
        }
    }

    public class ClassBenchmarkTreeNode : BenchmarkTreeNode
    {
        public ClassBenchmarkTreeNode(string projectName, string className) : base(className, projectName)
        {
        }
    }

    public class MethodBenchmarkTreeNode : BenchmarkTreeNode
    {
        public MethodBenchmarkTreeNode(string projectName, string methodName) : base(methodName, projectName)
        {
        }
    }
}
