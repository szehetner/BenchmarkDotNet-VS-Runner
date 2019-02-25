using System;
using System.Collections.Generic;
using System.IO;

namespace BenchmarkRunner.Model
{
    public class ResultWatcher
    {
        private readonly ResultsViewModel _parent;
        private Dictionary<string, FileSystemWatcher> _watchers = new Dictionary<string, FileSystemWatcher>();
        private string _currentLogfile;

        public ResultWatcher(ResultsViewModel parent)
        {
            _parent = parent;
        }

        public void StartWatching(string logfile)
        {
            _currentLogfile = logfile;

            string logPath = Path.GetDirectoryName(logfile);
            TryCreateWatcher(logPath);
        }

        public void TryCreateWatcher(string logPath)
        {
            if (_watchers.ContainsKey(logPath) || !Directory.Exists(logPath))
                return;

            FileSystemWatcher watcher = null;
            try
            {
                watcher = new FileSystemWatcher(logPath);
                watcher.Changed += Watcher_Changed;
                watcher.Created += Watcher_Changed;
                watcher.EnableRaisingEvents = true;

                _watchers.Add(logPath, watcher);
            }
            catch(Exception)
            {
                if (watcher != null)
                    watcher.Dispose();
            }
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (_currentLogfile == e.FullPath)
                _parent.RefreshResults();
        }

        internal void Reset()
        {
            foreach (var watcher in _watchers)
            {
                watcher.Value.Dispose();
            }

            _watchers.Clear();
        }
    }
}
