using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace BenchmarkRunner.Controls
{
    public partial class OptionsControl : UserControl
    {
        public OptionsControl()
        {
            InitializeComponent();

            toolTip.SetToolTip(chkMarkdownDefault, "Must be enabled for BenchmarkRunner result view.");
        }

        public OptionsPage OptionsPage { get; set; }

        public void Initialize()
        {
            chkMemoryDiagnoser.Checked = OptionsPage.IsMemoryDiagnoserEnabled;
            chkDisassembly.Checked = OptionsPage.IsDisassemblyDiagnoserEnabled;
            chkEtwProfiler.Checked = OptionsPage.IsEtwProfilerEnabled;

            chkClr.Checked = OptionsPage.RuntimeClrEnabled;
            chkCore.Checked = OptionsPage.RuntimeCoreEnabled;
            chkMono.Checked = OptionsPage.RuntimeMonoEnabled;
            chkCoreRT.Checked = OptionsPage.RuntimeCoreRTEnabled;

            chkCsv.Checked = OptionsPage.ExporterCsvEnabled;
            chkCsvMeasurements.Checked = OptionsPage.ExporterCsvMeasurementsEnabled;
            chkHtml.Checked = OptionsPage.ExporterHtmlEnabled;
            //chkMarkdownDefault.Checked = OptionsPage.ExporterMarkdownDefaultEnabled;
            chkMarkdownAtlassian.Checked = OptionsPage.ExporterMarkdownAtlassianEnabled;
            chkMarkdownStackOverflow.Checked = OptionsPage.ExporterMarkdownStackOverflowEnabled;
            chkMarkdownGitHub.Checked = OptionsPage.ExporterMarkdownGitHubEnabled;
            chkPlain.Checked = OptionsPage.ExporterPlainEnabled;
            chkRPlot.Checked = OptionsPage.ExporterRPlotEnabled;
            chkJsonDefault.Checked = OptionsPage.ExporterJsonDefaultEnabled;
            chkJsonBrief.Checked = OptionsPage.ExporterJsonBriefEnabled;
            chkJsonFull.Checked = OptionsPage.ExporterJsonFullEnabled;
            chkAsciiDoc.Checked = OptionsPage.ExporterAsciiDocEnabled;
            chkXmlDefault.Checked = OptionsPage.ExporterXmlDefaultEnabled;
            chkXmlBrief.Checked = OptionsPage.ExporterXmlBriefEnabled;
            chkXmlFull.Checked = OptionsPage.ExporterXmlFullEnabled;
            txtCommandLine.Text = OptionsPage.CommandlineParameters;
        }

        private void OptionsControl_Changed(object sender, EventArgs e)
        {
            OptionsPage.IsMemoryDiagnoserEnabled = chkMemoryDiagnoser.Checked;
            OptionsPage.IsDisassemblyDiagnoserEnabled = chkDisassembly.Checked;
            OptionsPage.IsEtwProfilerEnabled = chkEtwProfiler.Checked;

            OptionsPage.RuntimeClrEnabled = chkClr.Checked;
            OptionsPage.RuntimeCoreEnabled = chkCore.Checked;
            OptionsPage.RuntimeMonoEnabled = chkMono.Checked;
            OptionsPage.RuntimeCoreRTEnabled = chkCoreRT.Checked;

            OptionsPage.ExporterCsvEnabled = chkCsv.Checked;
            OptionsPage.ExporterCsvMeasurementsEnabled = chkCsvMeasurements.Checked;
            OptionsPage.ExporterHtmlEnabled = chkHtml.Checked;
            OptionsPage.ExporterMarkdownAtlassianEnabled = chkMarkdownAtlassian.Checked;
            OptionsPage.ExporterMarkdownStackOverflowEnabled = chkMarkdownStackOverflow.Checked;
            OptionsPage.ExporterMarkdownGitHubEnabled = chkMarkdownGitHub.Checked;
            OptionsPage.ExporterPlainEnabled = chkPlain.Checked;
            OptionsPage.ExporterRPlotEnabled = chkRPlot.Checked;
            OptionsPage.ExporterJsonDefaultEnabled = chkJsonDefault.Checked;
            OptionsPage.ExporterJsonBriefEnabled = chkJsonBrief.Checked;
            OptionsPage.ExporterJsonFullEnabled = chkJsonFull.Checked;
            OptionsPage.ExporterAsciiDocEnabled = chkAsciiDoc.Checked;
            OptionsPage.ExporterXmlDefaultEnabled = chkXmlDefault.Checked;
            OptionsPage.ExporterXmlBriefEnabled = chkXmlBrief.Checked;
            OptionsPage.ExporterXmlFullEnabled = chkXmlFull.Checked;
            OptionsPage.CommandlineParameters = txtCommandLine.Text;
        }

        private void lnkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("https://benchmarkdotnet.org/articles/guides/console-args.html");
            }
            catch(Exception)
            {
                // ignore errors
            }
        }
    }
}
