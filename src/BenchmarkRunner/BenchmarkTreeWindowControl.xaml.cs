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

            _viewModel = new ToolWindowViewModel();
            DataContext = _viewModel;
        }

        public ToolWindowViewModel InitializeViewModel(CommandHandler commandHandler)
        {
            _viewModel.TreeViewModel = new BenchmarkTreeViewModel(commandHandler);
            _viewModel.ResultsViewModel = new ResultsViewModel();
            return _viewModel;
        }

        private ToolWindowViewModel _viewModel;

        public BenchmarkTreeNode SelectedItem => BenchmarkTree.SelectedItem;
    }
}