using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using BenchmarkDotNet.Attributes;

namespace XmlFrameworksBenchmark.Benchmarks
{
    [MarkdownExporterAttribute.GitHub]
    [HtmlExporter]
    public class SelectAndModifyNestedXmlElementBenchmark
    {
        private const string XmlFile = @"mondial-europe.xml";
        private XmlDocument _xmlDoc;
        private XDocument _xDoc;

        [GlobalSetup]
        public void Setup()
        {
            var projectDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var _buffer = File.ReadAllBytes(Path.Combine(projectDir, "xmls", XmlFile));
            string xml = Encoding.UTF8.GetString(_buffer);

            // Load XmlDocument
            _xDoc = XDocument.Parse(xml);

            // Load XmlDocument
            _xmlDoc = new XmlDocument();
            _xmlDoc.LoadXml(xml);
        }

        [Benchmark]
        public XmlElement SelectAndModifyElement_XmlDocument()
        {
            // Select
            var node = (XmlElement)_xmlDoc.SelectSingleNode("/mondial/country/province/city[@id='cty-Sweden-Falun']/population");
            var value = node.InnerText; // value = '51900
            var attr = node.Attributes["year"].Value; // value = '1987'

            // Modify attribute
            node.SetAttribute("year", "2021");

#if DEBUG
            var check = node.Attributes["year"].Value; // value = '2021'
#endif

            return node;
        }

        [Benchmark]
        public XElement SelectAndModifyElement_XDocument_XPath()
        {
            // Select
            var element = _xDoc.XPathSelectElement("/mondial/country/province/city[@id='cty-Sweden-Falun']/population");
            var value = element.Value; // value = '51900'
            var attr = element.Attribute("year").Value; // value = '1987'

            // Modify attribute
            element.Attribute("year").SetValue("2021");

#if DEBUG
            var check = element.Attribute("year").Value; // value = '2021'
#endif

            return element;
        }

        [Benchmark]
        public XElement SelectAndModifyElement_XDocument_Linq()
        {
            // Select
            var element = _xDoc.Descendants("city").First(x => x.Attribute("id").Value == "cty-Sweden-Falun").Element("population");
            var value = element.Value; // value = '51900
            var attr = element.Attribute("year").Value; // value = '1987'

            // Modify attribute
            element.Attribute("year").SetValue("2021");

#if DEBUG
            var check = element.Attribute("year").Value; // value = '2021'
#endif

            return element;
        }
    }
}