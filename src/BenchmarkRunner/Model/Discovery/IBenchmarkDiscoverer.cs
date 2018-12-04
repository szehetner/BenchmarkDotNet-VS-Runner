using System.Collections.Generic;
using System.Threading.Tasks;

namespace BenchmarkRunner.Model
{
    public interface IBenchmarkDiscoverer
    {
        Task InitializeAsync();
        IEnumerable<Benchmark> FindBenchmarks();
    }
}
