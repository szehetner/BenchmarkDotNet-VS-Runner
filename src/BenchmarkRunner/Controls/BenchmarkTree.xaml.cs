using BenchmarkRunner.Model;
using System;
using System.Collections.Generic;
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

namespace BenchmarkRunner.Controls
{
    /// <summary>
    /// Interaction logic for BenchmarkTree.xaml
    /// </summary>
    public partial class BenchmarkTree : UserControl
    {
        public BenchmarkTree()
        {
            InitializeComponent();
        }

        public BenchmarkTreeNode SelectedItem
        {
            get { return (BenchmarkTreeNode)trvBenchmarks.SelectedItem; }
        }
    }
}
