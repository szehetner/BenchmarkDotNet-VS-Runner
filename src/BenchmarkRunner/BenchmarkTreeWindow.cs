using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using System.ComponentModel.Design;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell.Interop;
using BenchmarkRunner.Model;

namespace BenchmarkRunner
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("0642046d-b3a7-45cd-a1f6-9c2c3c29a45c")]
    public class BenchmarkTreeWindow : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkTreeWindow"/> class.
        /// </summary>
        public BenchmarkTreeWindow() : base(null)
        {
            this.Caption = "Benchmarks";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new BenchmarkTreeWindowControl();

            this.ToolBar = new CommandID(new Guid(BenchmarkTreeWindowCommand.guidBenchmarkTreeWindowPackageCmdSet), BenchmarkTreeWindowCommand.ToolbarID);
            this.ToolBarLocation = (int)VSTWT_LOCATION.VSTWT_TOP;
        }

        private BenchmarkTreeWindowControl TreeWindowControl => (BenchmarkTreeWindowControl)Content;
        
        public BenchmarkTreeNode SelectedItem => TreeWindowControl.SelectedItem;

        internal BenchmarkTreeViewModel GetViewModel()
        {
            return TreeWindowControl.GetViewModel();
        }
    }
}
