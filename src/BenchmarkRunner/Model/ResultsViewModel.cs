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
                        Log = File.ReadAllText(logfile);
                        Summary = BenchmarkResultCollection.ReadSummary(logfile);
                        return;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            
            Log = null;
            Summary = null;
        }

        public string Summary { get => _summary; set { _summary = value; OnPropertyChanged(); } }
        public string Log { get => _logFileContent; set { _logFileContent = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
