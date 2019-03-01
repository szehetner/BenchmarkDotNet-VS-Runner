using BenchmarkRunner.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BenchmarkRunner.Runner
{
    public class BenchmarkRunController
    {
        private readonly RunParameters _parameters;
        private readonly IOptionsProvider _optionsProvider;

        public BenchmarkRunController(RunParameters parameters, IOptionsProvider optionsProvider)
        {
            _parameters = parameters;
            _optionsProvider = optionsProvider;
        }

        public Task RunAsync()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = BuildCmdArguments(),
                CreateNoWindow = false,
                WindowStyle = ProcessWindowStyle.Normal,
                WorkingDirectory = GetWorkingDirectory()
            };
            var process = Process.Start(startInfo);
            return Task.Run(() => process.WaitForExit());
        }

        public void CopyCommandlineToClipboard()
        {
            string commandline = BuildArguments();
            Clipboard.SetText(commandline);
        }

        private string GetWorkingDirectory()
        {
            if (_parameters.Runtime == TargetRuntime.NetFramework)
                return _parameters.OutputPath;

            return _parameters.ProjectPath;
        }

        private string GetExecutable()
        {
            if (_parameters.Runtime == TargetRuntime.NetFramework)
                return _parameters.AssemblyPath;

            return "dotnet run -c Release";
        }

        private string BuildCmdArguments()
        {
            return "/k " + BuildArguments();
        }

        public string BuildArguments()
        {
            StringBuilder arguments = new StringBuilder();
            arguments.Append(GetExecutable());

            if (_parameters.IsDryRun)
                arguments.Append(" -j Dry");

            if (IsMultiTargetProject())
            {
                arguments.Append(" --framework ");
                arguments.Append(GetProjectTargetFramework());
            }

            arguments.Append(" " + _parameters.BuildFilter());

            AddRuntimeArguments(arguments);
            AddDiagnoserArguments(arguments);
            AddExporterArguments(arguments);

            if (!string.IsNullOrWhiteSpace(_optionsProvider.CommandlineParameters))
            {
                arguments.Append(" ");
                arguments.Append(_optionsProvider.CommandlineParameters);
            }

            return arguments.ToString();
        }

        private void AddRuntimeArguments(StringBuilder builder)
        {
            if (!_optionsProvider.RuntimeClrEnabled
                && !_optionsProvider.RuntimeCoreEnabled
                && !_optionsProvider.RuntimeMonoEnabled
                && !_optionsProvider.RuntimeCoreRTEnabled)
                return;

            if (_optionsProvider.RuntimeClrEnabled)
                builder.Append(" clr");

            if (_optionsProvider.RuntimeCoreEnabled)
                builder.Append(" core");

            if (_optionsProvider.RuntimeMonoEnabled)
                builder.Append(" mono");

            if (_optionsProvider.RuntimeCoreRTEnabled)
                builder.Append(" corert");
        }

        private void AddDiagnoserArguments(StringBuilder builder)
        {
            if (_optionsProvider.IsMemoryDiagnoserEnabled)
                builder.Append(" -m");

            if (_optionsProvider.IsDisassemblyDiagnoserEnabled)
                builder.Append(" -d");

            if (_optionsProvider.IsEtwProfilerEnabled)
                builder.Append(" -p ETW");
        }

        private void AddExporterArguments(StringBuilder builder)
        {
            builder.Append(" -e markdown");

            if (_optionsProvider.ExporterCsvEnabled)
                builder.Append(" csv");

            if (_optionsProvider.ExporterCsvMeasurementsEnabled)
                builder.Append(" csvmeasurements");

            if (_optionsProvider.ExporterHtmlEnabled)
                builder.Append(" html");

            if (_optionsProvider.ExporterMarkdownAtlassianEnabled)
                builder.Append(" atlassian");

            if (_optionsProvider.ExporterMarkdownStackOverflowEnabled)
                builder.Append(" stackoverflow");

            if (_optionsProvider.ExporterMarkdownGitHubEnabled)
                builder.Append(" github");

            if (_optionsProvider.ExporterPlainEnabled)
                builder.Append(" plain");

            if (_optionsProvider.ExporterRPlotEnabled)
                builder.Append(" rplot");

            if (_optionsProvider.ExporterJsonDefaultEnabled)
                builder.Append(" json");

            if (_optionsProvider.ExporterJsonBriefEnabled)
                builder.Append(" briefjson");

            if (_optionsProvider.ExporterJsonFullEnabled)
                builder.Append(" fulljson");

            if (_optionsProvider.ExporterAsciiDocEnabled)
                builder.Append(" asciidoc");

            if (_optionsProvider.ExporterXmlDefaultEnabled)
                builder.Append(" xml");

            if (_optionsProvider.ExporterXmlBriefEnabled)
                builder.Append(" briefxml");

            if (_optionsProvider.ExporterXmlFullEnabled)
                builder.Append(" fullxml");
        }

        private bool IsMultiTargetProject()
        {
            return _parameters.SelectedNode.ProjectName.IndexOf('(') != -1;
        }

        private string GetProjectTargetFramework()
        {
            string projectName = _parameters.SelectedNode.ProjectName;
            int startIndex = projectName.IndexOf('(');
            return projectName.Substring(startIndex + 1, projectName.Length - startIndex - 2);
        }

    }
}
