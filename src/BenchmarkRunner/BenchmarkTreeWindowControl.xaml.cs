namespace BenchmarkRunner
{
    using BenchmarkRunner.Model;
    using System;
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

            _viewModel = new BenchmarkTreeViewModel();
            BenchmarkTree.DataContext = _viewModel;
        }

        private BenchmarkTreeViewModel _viewModel = new BenchmarkTreeViewModel();

        public BenchmarkTreeNode SelectedItem => BenchmarkTree.SelectedItem;

        internal BenchmarkTreeViewModel GetViewModel()
        {
            return _viewModel;
        }
    }
}