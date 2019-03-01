using BenchmarkRunner.Model;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenchmarkRunner.Controls
{
    public class OptionsPage : DialogPage, IOptionsProvider
    {
        public string CommandlineParameters { get; set; }

        public bool IsMemoryDiagnoserEnabled { get; set; }
        public bool IsDisassemblyDiagnoserEnabled { get; set; }
        public bool IsEtwProfilerEnabled { get; set; }

        public bool RuntimeClrEnabled { get; set; }
        public bool RuntimeCoreEnabled { get; set; }
        public bool RuntimeMonoEnabled { get; set; }
        public bool RuntimeCoreRTEnabled { get; set; }

        public bool ExporterCsvEnabled { get; set; }
        public bool ExporterCsvMeasurementsEnabled { get; set; }
        public bool ExporterHtmlEnabled { get; set; }
        public bool ExporterMarkdownAtlassianEnabled { get; set; }
        public bool ExporterMarkdownStackOverflowEnabled { get; set; }
        public bool ExporterMarkdownGitHubEnabled { get; set; }
        public bool ExporterPlainEnabled { get; set; }
        public bool ExporterRPlotEnabled { get; set; }
        public bool ExporterJsonDefaultEnabled { get; set; }
        public bool ExporterJsonBriefEnabled { get; set; }
        public bool ExporterJsonFullEnabled { get; set; }
        public bool ExporterAsciiDocEnabled { get; set; }
        public bool ExporterXmlDefaultEnabled { get; set; }
        public bool ExporterXmlBriefEnabled { get; set; }
        public bool ExporterXmlFullEnabled { get; set; }

        protected override IWin32Window Window
        {
            get
            {
                OptionsControl control = new OptionsControl();
                control.OptionsPage = this;
                control.Initialize();
                return control;
            }
        }
    }
}
