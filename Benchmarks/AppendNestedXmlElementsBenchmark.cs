using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using BenchmarkDotNet.Attributes;

namespace XmlFrameworksBenchmark.Benchmarks
{
    [MarkdownExporterAttribute.GitHub]
    [HtmlExporter]
    [SimpleJob(launchCount: 1, warmupCount: 5, targetCount: 20)]
    public class AppendNestedXmlElementsBenchmark
    {
        [Params(100)]
        public int N { get; set; }
        private const string XmlFile = @"books.xml";
        private XmlDocument _xmlDoc;
        private XDocument _xDoc;

        [GlobalSetup]
        public void Setup()
        {
            //TODO: Load xml to buffer once
            var projectDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            byte[] _buffer = File.ReadAllBytes(Path.Combine(projectDir, "xmls", XmlFile));
            string xml = Encoding.UTF8.GetString(_buffer);

            // Load XmlDocument
            _xDoc = XDocument.Parse(xml);

            // Load XmlDocument
            _xmlDoc = new XmlDocument();
            _xmlDoc.LoadXml(xml);
        }

        [Benchmark]
        public XmlNode AppendNElements_XmlDocument()
        {
            var parent = _xmlDoc.SelectSingleNode("/catalog");
            for (int i = 0; i < N; i++)
            {
                var element = _xmlDoc.CreateElement("book");
                element.InnerXml = $"<title>Benchmark{i}</title>";
                var attr = _xmlDoc.CreateAttribute("id");
                attr.Value = "bk1010" + i;
                element.Attributes.Append(attr);
                parent.AppendChild(element);
            }

#if DEBUG
            var check = _xmlDoc.SelectSingleNode("/catalog/book[@id='bk10101']"); // not null
#endif

            return parent;
        }

        [Benchmark]
        public XElement AppendNElements_XDocument()
        {
            var parent = _xDoc.Element("catalog");
            for (int i = 0; i < N; i++)
            {
                var element = new XElement("book", new XAttribute("id", "bk1010" + i), new XElement("title", $"Benchmark{i}"));
                parent.Add(element);
            }
            
#if DEBUG
            var check = _xDoc.Descendants("book").First(x => x.Attribute("id").Value == "bk10101"); // not null
#endif
            return parent;
        }
    }
}