using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkRunner.Model
{
    public class ResultsViewModel : INotifyPropertyChanged
    {
        private string _previewContent;

        public string PreviewContent { get => _previewContent; set { _previewContent = value; OnPropertyChanged(); } }

        public void SetSelectedBenchmark(Benchmark benchmark)
        {
            if (benchmark != null)
                PreviewContent = benchmark.ClassName;
            else
                PreviewContent = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
