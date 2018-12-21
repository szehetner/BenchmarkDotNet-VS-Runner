using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BenchmarkRunner.Model
{
    public class ToolWindowViewModel : INotifyPropertyChanged
    {
        private int _horizontalColumnSpan = 1;
        private int _verticalRowSpan = 3;
        private int _treeRow;
        private int _treeColumn;
        private int _treeRowSpan;
        private int _treeColumnSpan;
        private int _resultsRow = 0;
        private int _resultsColumn = 2;
        private int _resultsRowSpan = 2;
        private int _resultsColumnSpan = 2;
        private Visibility _horizontalSplitterVisibility = Visibility.Collapsed;
        private Visibility _verticalSplitterVisibility = Visibility.Visible;
        private Visibility _resultVisibility = Visibility.Collapsed;
        private BenchmarkTreeViewModel _treeViewModel;

        public BenchmarkTreeViewModel TreeViewModel { get => _treeViewModel; set { _treeViewModel = value; OnPropertyChanged(); } }

        public Visibility HorizontalSplitterVisibility { get => _horizontalSplitterVisibility; set { _horizontalSplitterVisibility = value; OnPropertyChanged(); } }
        public Visibility VerticalSplitterVisibility { get => _verticalSplitterVisibility; set { _verticalSplitterVisibility = value; OnPropertyChanged(); } }
        public Visibility ResultVisibility { get => _resultVisibility; set { _resultVisibility = value; OnPropertyChanged(); } }

        public int HorizontalColumnSpan { get => _horizontalColumnSpan; set { _horizontalColumnSpan = value; OnPropertyChanged(); } }
        public int VerticalRowSpan { get => _verticalRowSpan; set { _verticalRowSpan = value; OnPropertyChanged(); } }

        public int TreeRow { get => _treeRow; set { _treeRow = value; OnPropertyChanged(); } }
        public int TreeColumn { get => _treeColumn; set { _treeColumn = value; OnPropertyChanged(); } }
        public int TreeRowSpan { get => _treeRowSpan; set { _treeRowSpan = value; OnPropertyChanged(); } }
        public int TreeColumnSpan { get => _treeColumnSpan; set { _treeColumnSpan = value; OnPropertyChanged(); } }

        public int ResultsRow { get => _resultsRow; set { _resultsRow = value; OnPropertyChanged(); } }
        public int ResultsColumn { get => _resultsColumn; set { _resultsColumn = value; OnPropertyChanged(); } }
        public int ResultsRowSpan { get => _resultsRowSpan; set { _resultsRowSpan = value; OnPropertyChanged(); } }
        public int ResultsColumnSpan { get => _resultsColumnSpan; set { _resultsColumnSpan = value; OnPropertyChanged(); } }


        public ToolWindowViewModel()
        {
            SetResultOrientation(ResultOrientation.None);
        }

        public void SetResultOrientation(ResultOrientation orientation)
        {
            if (orientation == ResultOrientation.Bottom)
            {
                TreeRow = 0;
                TreeColumn = 0;
                TreeColumnSpan = 3;
                TreeRowSpan = 1;
                ResultsRow = 2;
                ResultsColumn = 0;
                ResultsColumnSpan = 3;
                ResultsRowSpan = 1;
                HorizontalSplitterVisibility = Visibility.Visible;
                VerticalSplitterVisibility = Visibility.Collapsed;
                HorizontalColumnSpan = 3;
                VerticalRowSpan = 1;
                ResultVisibility = Visibility.Visible;
            }
            else if (orientation == ResultOrientation.Right)
            {
                TreeRow = 0;
                TreeColumn = 0;
                TreeColumnSpan = 1;
                TreeRowSpan = 3;
                ResultsRow = 0;
                ResultsColumn = 2;
                ResultsColumnSpan = 1;
                ResultsRowSpan = 3;
                HorizontalSplitterVisibility = Visibility.Collapsed;
                VerticalSplitterVisibility = Visibility.Visible;
                HorizontalColumnSpan = 1;
                VerticalRowSpan = 3;
                ResultVisibility = Visibility.Visible;
            }
            else
            {
                TreeRow = 0;
                TreeColumn = 0;
                TreeColumnSpan = 1;
                TreeRowSpan = 1;
                ResultsRow = 0;
                ResultsColumn = 2;
                ResultsColumnSpan = 1;
                ResultsRowSpan = 1;
                HorizontalSplitterVisibility = Visibility.Collapsed;
                VerticalSplitterVisibility = Visibility.Collapsed;
                HorizontalColumnSpan = 1;
                VerticalRowSpan = 1;
                ResultVisibility = Visibility.Collapsed;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum ResultOrientation
    {
        None,
        Right,
        Bottom
    }
}
