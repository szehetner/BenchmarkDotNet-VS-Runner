using BenchmarkRunner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ControlTest
{
    public class StubDiscoverer : IBenchmarkDiscoverer
    {
        public IEnumerable<Benchmark> FindBenchmarks()
        {
            yield return new Benchmark
            {
                ProjectName = "Project1",
                Namespace = "Namespace.Sub",
                ClassName = "Class1",
                MethodName = "Method1"
            };
            Thread.Sleep(2000);
            yield return new Benchmark
            {
                ProjectName = "Project1",
                Namespace = "Namespace.Sub",
                ClassName = "Class1",
                MethodName = "Method2"
            };
            Thread.Sleep(2000);
            yield return new Benchmark
            {
                ProjectName = "Project1",
                Namespace = "Namespace.Sub",
                ClassName = "Class2",
                MethodName = "MethodA"
            };
            Thread.Sleep(2000);
            yield return new Benchmark
            {
                ProjectName = "Project1",
                Namespace = "Namespace.Sub2",
                ClassName = "Class3",
                MethodName = "MethodC"
            };
            Thread.Sleep(2000);
            yield return new Benchmark
            {
                ProjectName = "Project1",
                Namespace = "Namespace.Sub2",
                ClassName = "Class4",
                MethodName = "MethodD"
            };
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
