
# XDoc.PDF Library - Create, read, edit, convert PDFs in C#.NET projects

XDoc.PDF is a C# PDF library developed by RasterEdge which helps C# developers to easily create, read, edit, process, convert PDF documents in .NET projects.

### Using XDoc.PDF to:
- Generate PDFs from MS Word, Excel, PowerPoint, TIFF, JPG, PNG and many image formats
- Convert PDF to Word, multi-page TIFF, SVG, JPG, PNG and other image formats
- Read, extract text, image, font data, AcroForm data from PDFs
- Edit, modify existing PDF text, image, bookmark, metadata contents
- Annotate, markup PDF content with highligh, comments, drawings
- Redact, remove sensitive information from PDF documents
- Protect PDFs with password protection
- Add, remove digit signature to PDFs
- Add, generate, read QR Code and barcode on PDF
- Convert scanned PDF to editable PDF using OCR


### Compatible with
- .NET Standard 2.0
- .NET 8, .NET 7, .NET 6, .NET 5, .NET Core 3.x & 2.x
- .NET Framework 4.x
- Windows, MacOS, Linux, Docker, Azure


## Get to Start

Once installed the PDF library package, you can use the following C# code to convert PDF to Word document.

```csharp
// file path to file path	
String inputPath = @"C:\demo.pdf";
String outputPath = @"C:\output.docx";
PDFDocument doc = new PDFDocument(inputPath);
doc.ConvertToDocument(DocumentType.DOCX, outputPath);


// stream to stream
String inputPath = @"";
byte[] arr = File.ReadAllBytes(inputPath);
Stream inputStream = new MemoryStream(arr);
PDFDocument doc = new PDFDocument(inputStream);
Stream outputStream = new MemoryStream();
doc.ConvertToDocument(DocumentType.DOCX, outputStream);

```


## Support & Documents

- C# How to Guide : https://www.rasteredge.com/how-to/csharp-imaging/pdf-overview/
- How-Tos 
     * Convert PDF : https://www.rasteredge.com/how-to/csharp-imaging/pdf-converting/
     * Edit PDF : https://www.rasteredge.com/how-to/csharp-imaging/pdf-page-modify/
     * Create PDF : https://www.rasteredge.com/how-to/csharp-imaging/pdf-creating/
     * Read PDF : https://www.rasteredge.com/how-to/csharp-imaging/pdf-reading/
     * Preview PDF : https://www.rasteredge.com/kb/pdf-csharp/file-preview/
     * Compress PDF : https://www.rasteredge.com/how-to/csharp-imaging/pdf-net-wpf/
- Licenses : https://www.rasteredge.com/xdoc/pdf/pricing/
- Email : support@rasteredge.com




