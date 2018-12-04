using BenchmarkRunner.Runner;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkRunner.ProjectSystem
{
    public interface IProjectPropertyProvider
    {
        Task LoadPropertiesAsync();
        bool IsOptimized { get; }
        string ProjectPath { get; }
        string OutputPath { get; }
        string GetOutputFilename();
        TargetRuntime TargetRuntime { get; }
    }
}
