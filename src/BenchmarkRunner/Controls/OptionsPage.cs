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
        [Category("Benchmarks")]
        [DisplayName("Command Line Parameters")]
        [Description("Additional command line parameters to pass to BenchmarkDotnet.")]
        public string CommandlineParameters { get; set; }
    }
}
