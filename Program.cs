using System;
using System.Xml.Linq;
using BenchmarkDotNet.Running;
using XmlFrameworksBenchmark.Benchmarks;

namespace XmlFrameworksBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {

            // var x = new AppendNestedXmlElementsBenchmark();
            // x.Setup();
            // x.N = 3;
            // x.AddManyElements_XDocument();
            // x.AddManyElements_XmlDocument();

            Console.WriteLine("Benchmark #1 - load XML to .net object.");
            BenchmarkRunner.Run<LoadXmlBenchmark>();

            Console.WriteLine("Benchmark #2 - select XML element and modify it.");
            BenchmarkRunner.Run<SelectAndModifyNestedXmlElementBenchmark>();

            // Console.WriteLine("Benchmark #3 - append element to XML tree.");
            // BenchmarkRunner.Run<AppendNestedXmlElementsBenchmark>();
        }
    }
}
