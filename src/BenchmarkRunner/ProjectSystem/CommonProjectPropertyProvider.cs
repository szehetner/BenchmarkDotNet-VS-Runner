using BenchmarkRunner.Runner;
using EnvDTE;
using Microsoft.VisualStudio.ProjectSystem.Properties;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BenchmarkRunner.ProjectSystem
{
    public class CommonProjectPropertyProvider : IProjectPropertyProvider
    {
        private readonly Project _project;
        private string _outputPath;
        private string _assemblyName;
        private string _targetFramework;

        public CommonProjectPropertyProvider(Project project)
        {
            _project = project;
        }

        public async Task LoadPropertiesAsync()
        {
            var context = (IVsBrowseObjectContext)_project;
            var unconfiguredProject = context.UnconfiguredProject;
            var configuredProject = await unconfiguredProject.GetSuggestedConfiguredProjectAsync();
            var properties = configuredProject.Services.ProjectPropertiesProvider.GetCommonProperties();

            IsOptimized = bool.Parse(await properties.GetEvaluatedPropertyValueAsync("Optimize"));
            Prefer32Bit = bool.Parse(await properties.GetEvaluatedPropertyValueAsync("Prefer32bit"));
            PlatformTarget = await properties.GetEvaluatedPropertyValueAsync("PlatformTarget");

            _outputPath = await properties.GetEvaluatedPropertyValueAsync("OutputPath");
            _assemblyName = await properties.GetEvaluatedPropertyValueAsync("AssemblyName");

            string targetFramework = await properties.GetEvaluatedPropertyValueAsync("TargetFramework");
            string targetFrameworks = await properties.GetEvaluatedPropertyValueAsync("TargetFrameworks");

            List<string> frameworks = string.IsNullOrEmpty(targetFrameworks) ? new List<string> { targetFramework } : targetFrameworks.Split(';').ToList();
            _targetFramework = frameworks.FirstOrDefault(f => f.StartsWith("netcore"));
        }
        
        public bool IsOptimized { get; private set; }
        public bool Prefer32Bit { get; private set; }
        public string PlatformTarget { get; private set; }
        public string ProjectPath => _project.Properties.Item("FullPath").Value.ToString();
        public string OutputPath => Path.Combine(ProjectPath, _outputPath);

        public string GetOutputFilename()
        {
            string assemblyName = _assemblyName + ".dll";
            
            if (OutputPath.Trim('\\').EndsWith(_targetFramework))
                return Path.Combine(OutputPath, assemblyName);

            return Path.Combine(OutputPath, "..", _targetFramework, assemblyName);
        }

        // TODO: test new projectsystem with NetFramework
        public TargetRuntime TargetRuntime
        {
            get
            {
                if (_targetFramework?.StartsWith("netcore") == true)
                    return TargetRuntime.NetCore;

                return TargetRuntime.NetFramework;
            }
        }
    }
}
