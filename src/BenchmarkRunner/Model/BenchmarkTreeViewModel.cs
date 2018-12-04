using System.Collections.ObjectModel;

namespace BenchmarkRunner.Model
{
    public class BenchmarkTreeViewModel
    {
        public ObservableCollection<BenchmarkTreeNode> Nodes { get; set; } = new ObservableCollection<BenchmarkTreeNode>();
    }
}
