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
        }

        public BenchmarkTreeViewModel InitializeViewModel(CommandHandler commandHandler)
        {
            _viewModel = new BenchmarkTreeViewModel(commandHandler);
            BenchmarkTree.DataContext = _viewModel;
            return _viewModel;
        }

        private BenchmarkTreeViewModel _viewModel;

        public BenchmarkTreeNode SelectedItem => BenchmarkTree.SelectedItem;
    }
}