## XmlDocument vs XDocument: Benchmark
Looking for a suitable solution for the project I was involved in, I built this benchmark in an attempt to determine what performs better - the old and dirty `XmlDocument` or maybe a slightly newer, much more readable and practical solution that uses XML to Linq with `XDocument` and `XElement`.

Benchmark using: [![BenchmarkDotNet](https://img.shields.io/badge/BenchmarkDotNet-0.12.1-orange)](https://github.com/dotnet/BenchmarkDotNet)


## Results
#### Benchmark #1 - Loading the XML to an .NET object.
|                       Method |            XmlFile |         Mean |      Error |     StdDev | Ratio | RatioSD |
|----------------------------- |------------------- |-------------:|-----------:|-----------:|------:|--------:|
|  LoadXml_XmlDocument_LoadXml |          books.xml |     31.40 μs |   0.198 μs |   0.185 μs |  1.00 |    0.00 |
|     LoadXml_XmlDocument_Load |          books.xml |     32.90 μs |   0.644 μs |   0.767 μs |  1.05 |    0.03 |
| LoadXml_XmlDocument_FromFile |          books.xml |    192.49 μs |   3.845 μs |   5.872 μs |  6.22 |    0.20 |
|      LoadXml_XDocument_Parse |          books.xml |     29.12 μs |   0.540 μs |   0.505 μs |  0.93 |    0.02 |
|       **LoadXml_XDocument_Load** |          books.xml |     **28.51 μs** |   0.099 μs |   0.087 μs |  **0.91** |    0.00 |
|   LoadXml_XDocument_FromFile |          books.xml |    187.23 μs |   2.491 μs |   2.330 μs |  5.96 |    0.08 |
|                              |                    |              |            |            |       |         |
|  LoadXml_XmlDocument_LoadXml | mondial-europe.xml | 23,702.41 μs | 338.244 μs | 316.394 μs |  1.00 |    0.00 |
|     LoadXml_XmlDocument_Load | mondial-europe.xml | 22,808.81 μs | 413.449 μs | 459.547 μs |  0.96 |    0.03 |
| LoadXml_XmlDocument_FromFile | mondial-europe.xml | 24,457.32 μs | 477.049 μs | 567.892 μs |  1.04 |    0.03 |
|      LoadXml_XDocument_Parse | mondial-europe.xml | 18,465.56 μs | 369.142 μs | 674.997 μs |  0.78 |    0.03 |
|       **LoadXml_XDocument_Load **| mondial-europe.xml | **15,389.74 μs** | 298.979 μs | 419.127 μs |  **0.65** |    0.02 |
|   LoadXml_XDocument_FromFile | mondial-europe.xml | 16,232.86 μs | 285.339 μs | 266.906 μs |  0.68 |    0.01 |


#### Benchmark #2 - Selecting and modifying the XML element.

|                                 Method |     Mean |   Error |   StdDev |   Median |
|--------------------------------------- |---------:|--------:|---------:|---------:|
|     SelectAndModifyElement_XmlDocument | 399.1 μs | 2.58 μs |  2.29 μs | 398.8 μs |
| SelectAndModifyElement_XDocument_XPath | 498.8 μs | 9.79 μs | 16.36 μs | 488.8 μs |
|  **SelectAndModifyElement_XDocument_Linq** | **157.0 μs** | **1.65 μs** |  **1.54 μs** | **157.3 μs** |

#### Benchmark #3 - Appending elements to the XML tree.

|                      Method |  N  |     Mean |    Error |    StdDev |   Median |
|---------------------------- |---- |---------:|---------:|----------:|---------:|
| AppendNElements_XmlDocument | 100 | 33.85 μs | 9.582 μs | 11.035 μs | 27.19 μs |
|   AppendNElements_XDocument | 100 | **16.56 μs** |** 6.509 μs** |  **7.496 μs** | **13.02 μs** |


------------

###References
- For more information on XML to Linq check: https://docs.microsoft.com/en-us/dotnet/standard/linq/linq-xml-overview
- XmlDocument Class: https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmldocument?view=net-5.0
- BenchmarkDotNet: https://github.com/dotnet/BenchmarkDotNet


##### XML files used in the benchmark:
books.xml - https://docs.microsoft.com/en-us/previous-versions/windows/desktop/ms762271(v=vs.85)
mondial-europe.xml - Institute for Informatics Georg-August-Universität Göttingen
https://www.dbis.informatik.uni-goettingen.de/Mondial/