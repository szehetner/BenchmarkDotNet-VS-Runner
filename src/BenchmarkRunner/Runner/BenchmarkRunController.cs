using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkRunner.Runner
{
    public class BenchmarkRunController
    {
        private readonly RunParameters _parameters;

        public BenchmarkRunController(RunParameters parameters)
        {
            _parameters = parameters;
        }

        public Task RunAsync()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = BuildArguments(),
                CreateNoWindow = false,
                WindowStyle = ProcessWindowStyle.Normal,
                WorkingDirectory = _parameters.OutputPath
            };
            var process = Process.Start(startInfo);
            return Task.Run(() => process.WaitForExit());
        }

        private string BuildArguments()
        {
            return "/k " + _parameters.AssemblyPath + " -j Dry --filter *";
        }
    }
    
    public enum TargetRuntime
    {
        NetCore,
        NetFramework
    }
    
    public class RunParameters
    {
        public TargetRuntime Runtime { get; set; }
        public string OutputPath { get; set; }
        public string AssemblyPath { get; set; }
        public string SelectedBenchmark { get; set; }
    }
}
