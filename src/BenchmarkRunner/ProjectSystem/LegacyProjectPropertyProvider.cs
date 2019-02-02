using BenchmarkRunner.Runner;
using EnvDTE;
using System.IO;
using System.Threading.Tasks;

namespace BenchmarkRunner.ProjectSystem
{
    public class LegacyProjectPropertyProvider : IProjectPropertyProvider
    {
        private readonly Project _project;
        private readonly Configuration _activeConfiguration;

        public LegacyProjectPropertyProvider(Project project)
        {
            _project = project;
            _activeConfiguration = project.ConfigurationManager.ActiveConfiguration;
        }

        public Task LoadPropertiesAsync()
        {
            return Task.CompletedTask;
        }

        public bool IsOptimized => (bool)_activeConfiguration.Properties.Item("Optimize").Value;
        public bool Prefer32Bit => (bool)_activeConfiguration.Properties.Item("Prefer32bit").Value;
        public string ProjectPath => _project.Properties.Item("FullPath").Value.ToString();
        public string OutputPath => Path.Combine(ProjectPath, _activeConfiguration.Properties.Item("OutputPath").Value.ToString());
        public TargetRuntime TargetRuntime => TargetRuntime.NetFramework;

        public string WorkingDirectory => OutputPath;

        public string GetOutputFilename()
        {
            string outputFileName = _project.Properties.Item("OutputFileName").Value.ToString();
            return Path.Combine(OutputPath, outputFileName);
        }
    }
}
