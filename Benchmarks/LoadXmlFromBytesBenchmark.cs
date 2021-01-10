using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using BenchmarkDotNet.Attributes;

namespace XmlFrameworksBenchmark.Benchmarks
{
    [MarkdownExporterAttribute.GitHub]
    [HtmlExporter]
    public class LoadXmlBenchmark
    {
        private const string SMALL_XML = @"books.xml";
        private const string MEDIUM_XML = @"mondial-europe.xml";
        private string _filePath;
        private byte[] _buffer;

        [ParamsSource(nameof(XmlFilesConsts))]
        public string XmlFile { get; set; }
        public IEnumerable<string> XmlFilesConsts => new[] { SMALL_XML, MEDIUM_XML };

        [GlobalSetup]
        public void Setup()
        {
            var projectDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            _filePath = Path.Combine(projectDir, "xmls", XmlFile);

            _buffer = File.ReadAllBytes(_filePath);
        }

        [Benchmark(Baseline = true)]
        public XmlDocument LoadXml_XmlDocument_LoadXml()
        {
            XmlDocument doc = new XmlDocument();
            string xml = Encoding.UTF8.GetString(_buffer);
            doc.LoadXml(xml);

            return doc;
        }

        [Benchmark]
        public XmlDocument LoadXml_XmlDocument_Load()
        {
            XmlDocument doc = new XmlDocument();
            using MemoryStream ms = new MemoryStream(_buffer);
            doc.Load(ms);

            return doc;
        }

        [Benchmark]
        public XmlDocument LoadXml_XmlDocument_FromFile()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(_filePath);

            return doc;
        }

        [Benchmark]
        public XDocument LoadXml_XDocument_Parse()
        {
            string xml = Encoding.UTF8.GetString(_buffer);
            XDocument doc = XDocument.Parse(xml);

            return doc;
        }

        [Benchmark]
        public XDocument LoadXml_XDocument_Load()
        {
            using MemoryStream ms = new MemoryStream(_buffer);
            XDocument doc = XDocument.Load(ms);

            return doc;
        }

        [Benchmark]
        public XDocument LoadXml_XDocument_FromFile()
        {
            XDocument doc = XDocument.Load(_filePath);

            return doc;
        }
    }
}