using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkRunner.Model
{
    public interface IOptionsProvider
    {
        string CommandlineParameters { get; }
        bool IsMemoryDiagnoserEnabled { get; }
        bool IsDisassemblyDiagnoserEnabled { get;  }
        bool IsEtwProfilerEnabled { get; }

        bool RuntimeClrEnabled { get; }
        bool RuntimeCoreEnabled { get; }
        bool RuntimeMonoEnabled { get; }
        bool RuntimeCoreRTEnabled { get; }

        bool ExporterCsvEnabled { get; }
        bool ExporterCsvMeasurementsEnabled { get; }
        bool ExporterHtmlEnabled { get; }
        bool ExporterMarkdownAtlassianEnabled { get; }
        bool ExporterMarkdownStackOverflowEnabled { get; }
        bool ExporterMarkdownGitHubEnabled { get; }
        bool ExporterPlainEnabled { get; }
        bool ExporterRPlotEnabled { get; }
        bool ExporterJsonDefaultEnabled { get; }
        bool ExporterJsonBriefEnabled { get; }
        bool ExporterJsonFullEnabled { get; }
        bool ExporterAsciiDocEnabled { get; }
        bool ExporterXmlDefaultEnabled { get; }
        bool ExporterXmlBriefEnabled { get; }
        bool ExporterXmlFullEnabled { get; }
    }
}
