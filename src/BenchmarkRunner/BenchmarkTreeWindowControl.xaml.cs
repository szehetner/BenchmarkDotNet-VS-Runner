namespace BenchmarkRunner
{
    using BenchmarkRunner.Model;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for BenchmarkTreeWindowControl.
    /// </summary>
    public partial class BenchmarkTreeWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkTreeWindowControl"/> class.
        /// </summary>
        public BenchmarkTreeWindowControl()
        {
            this.InitializeComponent();
        }

        public void Refresh(WorkspaceBenchmarkDiscoverer discoverer)
        {
            BenchmarkTreeViewModel viewModel = new BenchmarkTreeViewModel();
            BenchmarkTree.DataContext = viewModel;

            BenchmarkNodeBuilder nodeBuilder = new BenchmarkNodeBuilder();
            nodeBuilder.RebuildNodes(viewModel.Nodes, discoverer);
        }

        public BenchmarkTreeNode SelectedItem => BenchmarkTree.SelectedItem;
    }
}