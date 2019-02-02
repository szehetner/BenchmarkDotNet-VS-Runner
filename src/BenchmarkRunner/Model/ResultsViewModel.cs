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
        private string _summary;

        public void SetSelectedBenchmark(Benchmark benchmark)
        {
            _benchmark = benchmark;

            if (benchmark?.ArtifactsFolder != null)
            {
                string logfile = benchmark.GetLogFilename();
                if (File.Exists(logfile))
                {
                    try
                    {
                        _logFileContent = File.ReadAllText(logfile);
                        _summary = BenchmarkResultCollection.ReadSummary(logfile);
                    }
                    catch (Exception)
                    {
                        _logFileContent = null;
                        _summary = null;
                    }
                }
            }
            else
            {
                _logFileContent = null;
                _summary = null;
            }

            OnPropertyChanged(nameof(Summary));
            OnPropertyChanged(nameof(Log));
        }
        
        public string Summary => _summary;
        public string Log => _logFileContent;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
