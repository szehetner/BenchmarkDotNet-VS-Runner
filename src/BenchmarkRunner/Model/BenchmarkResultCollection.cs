using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkRunner.Model
{
    public class BenchmarkResultCollection
    {
        public const string ARTIFACTS_FOLDER = "BenchmarkDotNet.Artifacts";
        public const string RESULTS_FOLDER = "results";

        public string ArtifactsFolder { get; set; }

        private Dictionary<string, BenchmarkResult> _results = new Dictionary<string, BenchmarkResult>();

        public static async Task<BenchmarkResultCollection> CreateAsync(string projectName)
        {
            var propertyProvider = await CommandHandler.CreateProjectPropertyProviderAsync(projectName);
            
            return await Task.Run(() => new BenchmarkResultCollection(propertyProvider.OutputPath));
        }

        public static async Task<string> GetReportFolderAsync(string projectName)
        {
            var propertyProvider = await CommandHandler.CreateProjectPropertyProviderAsync(projectName);
            return Path.Combine(propertyProvider.OutputPath, ARTIFACTS_FOLDER);
        }

        public BenchmarkResultCollection(string outputDirectory)
        {
            ArtifactsFolder = Path.Combine(outputDirectory, ARTIFACTS_FOLDER);
            if (!Directory.Exists(ArtifactsFolder))
                return;

            IEnumerable<string> logFiles = Directory.EnumerateFiles(ArtifactsFolder, "*.log");
            ProcessLogfiles(logFiles);

            string resultsFolder = Path.Combine(ArtifactsFolder, RESULTS_FOLDER);
            var reportFiles = Directory.EnumerateFiles(resultsFolder);
            ProcessReportFiles(reportFiles);
        }

        private void ProcessReportFiles(IEnumerable<string> reportFiles)
        {
            foreach (var reportFile in reportFiles)
            {
                string benchmarkName = ExtractBenchmarkName(reportFile);
                if (!_results.TryGetValue(benchmarkName, out var result))
                    continue;

                result.Reports.Add(new Report
                {
                    FullPath = reportFile,
                    TypeName = ExtractTypeName(reportFile)
                });
            }
        }
        
        private string ExtractBenchmarkName(string reportFile)
        {
            string filename = Path.GetFileNameWithoutExtension(reportFile);
            int reportIndex = filename.IndexOf("-report");
            if (reportIndex == -1)
                return filename;

            return filename.Substring(0, reportIndex);
        }

        private string ExtractTypeName(string reportFile)
        {
            string filename = Path.GetFileName(reportFile);
            int reportIndex = filename.IndexOf("-report");
            if (reportIndex == -1)
                return "<unknown>";

            int reportEndIndex = reportIndex + "-report".Length;
            return filename.Substring(reportEndIndex, filename.Length - reportEndIndex).TrimStart('-');
        }

        private void ProcessLogfiles(IEnumerable<string> logFiles)
        {
            foreach (var logFile in logFiles)
            {
                string benchmarkName = Path.GetFileNameWithoutExtension(logFile);
                BenchmarkResult result = new BenchmarkResult
                {
                    LogFileFullPath = logFile,
                    //Summary = ReadSummary(logFile)
                };
                _results[benchmarkName] = result;
            }
        }

        public static string ReadSummary(string logFile)
        {
            var lines = File.ReadLines(logFile);
            StringBuilder stringBuilder = new StringBuilder();
            bool isInSummary = false;
            foreach (var line in lines)
            {
                if (isInSummary && line.StartsWith("// *"))
                {
                    break;
                }

                if (isInSummary)
                {
                    stringBuilder.AppendLine(line);
                }

                if (line.StartsWith("// * Summary *"))
                {
                    isInSummary = true;
                }
            }
            return stringBuilder.ToString();
        }

        internal BenchmarkResult GetResult(string benchmarkName)
        {
            _results.TryGetValue(benchmarkName, out var result);
            return result;
        }
    }

    public class BenchmarkResult
    {
        //public string Summary { get; set; }
        public string LogFileFullPath { get; set; }
        public List<Report> Reports { get; set; } = new List<Report>();
    }

    public class Report
    {
        public string FullPath { get; set; }
        public string TypeName { get; set; }
        //public ImageSource Icon { get; set; }
    }
}