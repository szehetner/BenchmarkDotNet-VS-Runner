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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = treeView.SelectedItem;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BenchmarkTreeViewModel viewModel = new BenchmarkTreeViewModel();
            viewModel.Nodes = new ObservableCollection<BenchmarkTreeNode>
            {
                new ProjectBenchmarkTreeNode("BenchmarkProject1")
                {
                    Nodes = new ObservableCollection<BenchmarkTreeNode>()
                    {
                        new ClassBenchmarkTreeNode("BenchmarkProject1", "BenchmarkClass1")
                        {
                            Nodes = new ObservableCollection<BenchmarkTreeNode>
                            {
                                new MethodBenchmarkTreeNode("BenchmarkProject1", "Method1"),
                                new MethodBenchmarkTreeNode("BenchmarkProject1", "Method2"),
                            }
                        }
                    }
                },
                new ProjectBenchmarkTreeNode("BenchmarkProject2")
            };

            treeView.DataContext = viewModel;
        }
    }
}
