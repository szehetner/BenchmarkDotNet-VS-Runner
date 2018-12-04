using System;
using System.ComponentModel.Design;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using BenchmarkRunner.Model;
using System.Runtime.InteropServices;
using BenchmarkRunner.ProjectSystem;
using BenchmarkRunner.Runner;

namespace BenchmarkRunner
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class BenchmarkTreeWindowCommand
    {
        public const int CommandId = 0x0100;

        public const string guidBenchmarkTreeWindowPackageCmdSet = "322958f6-2403-49a7-ab0f-943c0e2011e5"; 
        public const uint cmdIdToolbarCommands = 0x100;
        public const int cmdIdRun = 0x135;
        public const int cmdIdRefresh = 0x131;
        public const int cmdIdGroupBy = 0x132;
        public const int cmdIdGroupByList = 0x137;
        public const int ToolbarID = 0x1000;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid(guidBenchmarkTreeWindowPackageCmdSet);

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        private IServiceProvider _serviceProvider;

        [Import]
        internal VisualStudioWorkspace Workspace;

        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkTreeWindowCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private BenchmarkTreeWindowCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            var dte2 = (DTE2)Package.GetGlobalService(typeof(SDTE));
            _serviceProvider = new ServiceProvider(dte2 as Microsoft.VisualStudio.OLE.Interop.IServiceProvider);

            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);


            commandService.AddCommand(new OleMenuCommand(GroupByHandler, new CommandID(CommandSet, cmdIdGroupBy)));
            commandService.AddCommand(new OleMenuCommand(GroupByListHandler, new CommandID(CommandSet, cmdIdGroupByList)));

            commandService.AddCommand(new MenuCommand(new EventHandler(RunBenchmarks), new CommandID(CommandSet, cmdIdRun)));
            commandService.AddCommand(new MenuCommand(new EventHandler(Refresh), new CommandID(CommandSet, cmdIdRefresh)));
            
        }

        private string selectedGrouping = "Namespace, Class";

        private void GroupByHandler(object sender, EventArgs eventArgs)
        {
            if (eventArgs == EventArgs.Empty)
                return;

            var args = eventArgs as OleMenuCmdEventArgs;
            if (args == null)
                return;

            string newChoice = args.InValue as string;
            IntPtr vOut = args.OutValue;

            if (vOut != IntPtr.Zero && newChoice != null)
                throw new ArgumentException("Both in and out parameters can't be null!");

            if (vOut != IntPtr.Zero)
            {
                // Set combo value.
                Marshal.GetNativeVariantForObject(selectedGrouping, vOut);
            }
            else if (newChoice != null && newChoice != selectedGrouping)
            {
                // TODO: change grouping
            }
        }

        private void GroupByListHandler(object sender, EventArgs eventArgs)
        {
            var args = eventArgs as OleMenuCmdEventArgs;
            if (args == null)
                return;

            string[] groupByOptions = new string[]
            {
                "Class",
                "Namespace, Class",
                "Category, Class",
            };

            Marshal.GetNativeVariantForObject(groupByOptions, args.OutValue);
        }

        private async void Refresh(object sender, EventArgs arguments)
        {
            var toolWindow = (BenchmarkTreeWindow)this.package.FindToolWindow(typeof(BenchmarkTreeWindow), 0, false);

            var discoverer = new WorkspaceBenchmarkDiscoverer(Workspace);
            await discoverer.InitializeAsync();

            toolWindow.Refresh(discoverer);
        }

        private EnvDTE.Project GetProject(DTE2 dte2, string name)
        {
            foreach (EnvDTE.Project project in dte2.Solution.Projects)
            {
                if (project.Name == name)
                    return project;
            }
            throw new Exception("Unexpected Project: " + name);
        }

        private async void RunBenchmarks(object sender, EventArgs arguments)
        {
            try
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

                var toolWindow = (BenchmarkTreeWindow)this.package.FindToolWindow(typeof(BenchmarkTreeWindow), 0, false);

                BenchmarkTreeNode selectedNode = toolWindow.SelectedItem;

                var dte2 = (DTE2)Package.GetGlobalService(typeof(SDTE));
                EnvDTE.Project project = GetProject(dte2, selectedNode.ProjectName);
                if (project == null)
                    return;

                var propertyProvider = ProjectPropertyProviderFactory.Create(project);
                await propertyProvider.LoadPropertiesAsync();

                try
                {
                    if (!propertyProvider.IsOptimized)
                    {
                        await ShowError(
                            "The current build configuration does not have the \"Optimize code\" flag set and is therefore not suitable for running Benchmarks.\r\n\r\nPlease enable the the \"Optimize code\" flag (under Project Properties -> Build) or switch to a non-debug configuration (e.g. 'Release') before running a Benchmark.");
                        return;
                    }
                }
                catch (Exception)
                {
                }

                string configurationName = project.ConfigurationManager.ActiveConfiguration.ConfigurationName;
                dte2.Solution.SolutionBuild.BuildProject(configurationName, project.UniqueName, true);
                if (dte2.Solution.SolutionBuild.LastBuildInfo != 0)
                {
                    return;
                }

                var runParameters = new RunParameters
                {
                    OutputPath = propertyProvider.OutputPath,
                    Runtime = propertyProvider.TargetRuntime,
                    AssemblyPath = propertyProvider.GetOutputFilename()

                };
                BenchmarkRunController runController = new BenchmarkRunController(runParameters);
                await runController.RunAsync();
            }
            catch(Exception ex)
            {
                await ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static BenchmarkTreeWindowCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in BenchmarkTreeWindowCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new BenchmarkTreeWindowCommand(package, commandService);

            var container = await package.GetServiceAsync(typeof(Microsoft.VisualStudio.ComponentModelHost.SComponentModel)) as Microsoft.VisualStudio.ComponentModelHost.IComponentModel;
            container.DefaultCompositionService.SatisfyImportsOnce(Instance);
        }

        /// <summary>
        /// Shows the tool window when the menu item is clicked.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            // Get the instance number 0 of this tool window. This window is single instance so this instance
            // is actually the only one.
            // The last flag is set to true so that if the tool window does not exists it will be created.
            ToolWindowPane window = this.package.FindToolWindow(typeof(BenchmarkTreeWindow), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException("Cannot create tool window");
            }

            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }

        private async Task ShowError(string message)
        {
            string title = "Benchmark Runner";

            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            VsShellUtilities.ShowMessageBox(
                _serviceProvider,
                message,
                title,
                OLEMSGICON.OLEMSGICON_CRITICAL,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}
