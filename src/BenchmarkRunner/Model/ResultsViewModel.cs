using BenchmarkRunner.Controls.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkRunner.Model
{
    public class ResultsViewModel : INotifyPropertyChanged
    {
        private Benchmark _benchmark;
        private string _logFileContent;

        public void SetSelectedBenchmark(Benchmark benchmark)
        {
            _benchmark = benchmark;

            if (benchmark?.LastResult?.LogFileFullPath != null)
                _logFileContent = File.ReadAllText(benchmark.LastResult.LogFileFullPath);
            else
                _logFileContent = null;

            OnPropertyChanged(nameof(Summary));
            OnPropertyChanged(nameof(Log));
        }

        public string Summary => _benchmark?.LastResult?.Summary;
        public string Log => _logFileContent;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
