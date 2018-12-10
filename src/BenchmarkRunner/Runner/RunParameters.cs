using BenchmarkRunner.Model;
using System;

namespace BenchmarkRunner.Runner
{
    public class RunParameters
    {
        public TargetRuntime Runtime { get; set; }
        public string OutputPath { get; set; }
        public string AssemblyPath { get; set; }
        public BenchmarkTreeNode SelectedNode { get; set; }
        public bool IsDryRun { get; set; }

        public string BuildFilter()
        {
            switch(SelectedNode)
            {
                case ProjectBenchmarkTreeNode _:
                case null:
                    return "--filter *";

                case NamespaceBenchmarkTreeNode n:
                    return $"--filter {n.FullName}.*";

                case CategoryBenchmarkTreeNode c:
                    return $"--anyCategories {c.NodeName}";

                case ClassBenchmarkTreeNode c:
                    return $"--filter {c.FullName}.*";

                case MethodBenchmarkTreeNode c:
                    return $"--filter {c.FullName}";

                default:
                    throw new ArgumentException();
            }
        }
    }
}
