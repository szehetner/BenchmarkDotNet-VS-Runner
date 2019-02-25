using System;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;
using BenchmarkRunner.Model;
using BenchmarkRunner.ProjectSystem;
using BenchmarkRunner.Runner;
using Microsoft.VisualStudio.LanguageServices;
using System.Threading;
using Microsoft.CodeAnalysis;
using System.Threading.Tasks;
using System.Diagnostics;
using BenchmarkRunner.Controls;
using System.IO;

namespace BenchmarkRunner
{
    public class CommandHandler
    {
        private AsyncPackage _package;
        private readonly VisualStudioWorkspace _workspace;

        public CommandHandler(BenchmarkTreeWindowCommand commands)
        {
            _package = commands.ParentPackage;
            _workspace = commands.Workspace;
        }

        public async Task RunAsync(bool isDryRun)
        {
            try
            {
                BenchmarkRunController runController = await CreateBenchmarkRunControllerAsync(isDryRun);
                if (runController == null)
                    return;

                await runController.RunAsync();
            }
            catch (Exception ex)
            {
                await UIHelper.ShowErrorAsync(_package, ex.Message);
            }
        }

        public async Task CopyCommandlineToClipboardAsync()
        {
            try
            {
                BenchmarkRunController runController = await CreateBenchmarkRunControllerAsync(false);
                if (runController == null)
                    return;

                runController.CopyCommandlineToClipboard();
            }
            catch (Exception ex)
            {
                await UIHelper.ShowErrorAsync(_package, ex.Message);
            }
        }

        private async Task<BenchmarkRunController> CreateBenchmarkRunControllerAsync(bool isDryRun)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(_package.DisposalToken);

            var toolWindow = (BenchmarkTreeWindow)_package.FindToolWindow(typeof(BenchmarkTreeWindow), 0, false);
            BenchmarkTreeNode selectedNode = toolWindow.SelectedItem;

            var dte2 = (DTE2)Package.GetGlobalService(typeof(SDTE));
            EnvDTE.Project project = GetProject(dte2, selectedNode.ProjectName);
            if (project == null)
                return null;

            var propertyProvider = await CreateProjectPropertyProviderAsync(project.Name);

            try
            {
                if (!propertyProvider.IsOptimized)
                {
                    await UIHelper.ShowErrorAsync(_package,
                        "The current build configuration does not have the \"Optimize code\" flag set and is therefore not suitable for running Benchmarks.\r\n\r\nPlease enable the \"Optimize code\" flag (under Project Properties -> Build) or switch to a non-debug configuration (e.g. 'Release') before running a Benchmark.");
                    return null;
                }
            }
            catch (Exception)
            {
            }

            string configurationName = project.ConfigurationManager.ActiveConfiguration.ConfigurationName;
            dte2.Solution.SolutionBuild.BuildProject(configurationName, project.UniqueName, true);
            if (dte2.Solution.SolutionBuild.LastBuildInfo != 0)
            {
                return null;
            }

            var runParameters = new RunParameters
            {
                OutputPath = propertyProvider.OutputPath,
                ProjectPath = propertyProvider.ProjectPath,
                Runtime = propertyProvider.TargetRuntime,
                AssemblyPath = propertyProvider.GetOutputFilename(),
                IsDryRun = isDryRun,
                SelectedNode = selectedNode
            };
            return new BenchmarkRunController(runParameters, GetOptions());
        }

        private IOptionsProvider GetOptions()
        {
            return (OptionsPage)_package.GetDialogPage(typeof(OptionsPage));
        }

        internal async Task GoToCodeAsync(Project project, ISymbol targetSymbol)
        {
            try
            {
                _workspace.TryGoToDefinition(targetSymbol, project, CancellationToken.None);
            }
            catch (Exception ex)
            {
                await UIHelper.ShowErrorAsync(_package, ex.Message);
            }
        }

        public async Task OpenReportFolderAsync()
        {
            try
            {
                var toolWindow = (BenchmarkTreeWindow)_package.FindToolWindow(typeof(BenchmarkTreeWindow), 0, false);
                BenchmarkTreeNode selectedNode = toolWindow.SelectedItem;
                if (selectedNode == null)
                    return;
            
                string reportFolder = await BenchmarkResultCollection.GetReportFolderAsync(selectedNode.ProjectName);
                if (!Directory.Exists(reportFolder))
                    return;

                Process.Start(reportFolder);
            }
            catch (Exception ex)
            {
                await UIHelper.ShowErrorAsync(_package, ex.Message);
            }
        }
        
        public static async Task<IProjectPropertyProvider> CreateProjectPropertyProviderAsync(string projectName)
        {
            var dte2 = (DTE2)Package.GetGlobalService(typeof(SDTE));
            EnvDTE.Project project = GetProject(dte2, projectName);
            if (project == null)
                return null;

            var propertyProvider = ProjectPropertyProviderFactory.Create(project);
            if (propertyProvider == null)
                return null;

            await propertyProvider.LoadPropertiesAsync();
            return propertyProvider;
        }

        private static EnvDTE.Project GetProject(DTE2 dte2, string name)
        {
            string baseName = name;
            var parenthesisIndex = name.IndexOf('(');
            if (parenthesisIndex != -1)
                baseName = name.Substring(0, parenthesisIndex);

            foreach (EnvDTE.Project project in dte2.Solution.Projects)
            {
                if (project.Name == baseName)
                    return project;
            }
            return null;
        }
    }
}
