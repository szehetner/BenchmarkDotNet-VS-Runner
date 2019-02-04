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
        private readonly ResultWatcher _resultWatcher;

        private Benchmark _benchmark;
        private string _logFileContent;
        private string _summary;

        public ResultsViewModel()
        {
            _resultWatcher = new ResultWatcher(this);
        }

        public void SetSelectedBenchmark(Benchmark benchmark)
        {
            _benchmark = benchmark;

            RefreshResults();
        }

        public void RefreshResults()
        {
            if (_benchmark?.ArtifactsFolder != null)
            {
                string logfile = _benchmark.GetLogFilename();
                _resultWatcher.StartWatching(logfile);
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

        internal void Reset()
        {
            Log = null;
            Summary = null;
            _resultWatcher.Reset();
        }
    }
}
