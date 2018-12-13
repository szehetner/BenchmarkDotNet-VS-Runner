using BenchmarkRunner.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private BenchmarkTreeViewModel _viewModel;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = treeView.SelectedItem;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = new BenchmarkTreeViewModel(null);
            //viewModel.Nodes = new ObservableCollection<BenchmarkTreeNode>
            //{
            //    new ProjectBenchmarkTreeNode("BenchmarkProject1")
            //    {
            //        Nodes = new ObservableCollection<BenchmarkTreeNode>()
            //        {
            //            new NamespaceBenchmarkTreeNode(null, "Namespace1.Namespace2")
            //            {
            //                Nodes = new ObservableCollection<BenchmarkTreeNode>
            //                {
            //                    new ClassBenchmarkTreeNode(null, null, "BenchmarkClass1")
            //                    {
            //                        Nodes = new ObservableCollection<BenchmarkTreeNode>
            //                        {
            //                            new MethodBenchmarkTreeNode(null, null, "Method1"),
            //                            new MethodBenchmarkTreeNode(null, null, "Method2"),
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    },
            //    new ProjectBenchmarkTreeNode("BenchmarkProject2")
            //};

            treeView.DataContext = _viewModel;
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            var discoverer = new StubDiscoverer();
            await _viewModel.RefreshAsync(discoverer, Grouping.ProjectNamespaceClass);
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ExpandAll_Click(object sender, RoutedEventArgs e)
        {
            ((BenchmarkTreeViewModel)treeView.DataContext).ExpandAll();
        }

        private void CollapseAll_Click(object sender, RoutedEventArgs e)
        {
            ((BenchmarkTreeViewModel)treeView.DataContext).CollapseAll();
        }
    }
}
