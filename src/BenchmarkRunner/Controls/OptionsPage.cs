using BenchmarkRunner.Model;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkRunner.Controls
{
    public class OptionsPage : DialogPage, IOptionsProvider
    {
        [Category("General")]
        [DisplayName("Command Line Parameters")]
        [Description("Additional command line parameters to pass to BenchmarkDotnet.")]
        public string CommandlineParameters { get; set; }

        [Category("Diagnosers")]
        [DisplayName("Enable Memory Diagnoser")]
        [Description("Enables the MemoryDiagnoser and prints memory statistics.")]
        public bool IsMemoryDiagnoserEnabled { get; set; }

        [Category("Diagnosers")]
        [DisplayName("Enable Disassembly Diagnoser")]
        [Description("Enables the DisassemblyDiagnoser and exports disassembly of benchmarked code.")]
        public bool IsDisassemblyDiagnoserEnabled { get; set; }

        [Category("Profiler")]
        [DisplayName("Enable ETW Profiler")]
        [Description("Enables the DisassemblyDiagnoser and exports disassembly of benchmarked code.")]
        public bool IsEtwProfilerEnabled { get; set; }
    }
}
