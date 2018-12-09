using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkRunner.Model
{
    public class Benchmark
    {
        public string Project { get; set; }
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public List<string> Categories { get; set; }
    }
}
