using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.IO;

namespace BenchmarkRunner.Model
{
    public class Benchmark
    {
        public string ProjectName { get; set; }
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public List<string> Categories { get; set; }

        public ISymbol MethodSymbol { get; set; }
        public ISymbol ClassSymbol { get; set; }
        public Project Project { get; set; }

        public BenchmarkResult LastResult { get; set; }
        public string ArtifactsFolder { get; set; }

        public string GetLogFilename()
        {
            string filename = Namespace + "." + ClassName + ".log";
            return Path.Combine(ArtifactsFolder, filename);
        }

        public string GetSummaryFilename()
        {
            string filename = Namespace + "." + ClassName + "-report-default.md";
            return Path.Combine(ArtifactsFolder, BenchmarkResultCollection.RESULTS_FOLDER, filename);
        }
    }
}
