# XDoc.PowerPoint is a FREE C# Office PowerPoint library to read, convert, edit, create PPTX documents in .NET projects.


### XDoc.PowerPoint from RasterEdge is an advanced Microsoft PowerPoint library :
- It is FREE
- Generate PPTX from scratch
- Convert PPTX to PDF, SVG, HTML, multi-page TIFF, JPG, PNG and many raster image formats
- Edit, process, merge PowerPoint document

### Compatible with
- .NET Standard 2.0
- .NET 8, .NET 7, .NET 6, .NET 5, .NET Core 3.x & 2.x
- .NET Framework 4.x
- Windows, Mac, Linux, Docker, Azure


## Get to Start

Once installed the package, you can use the following C# code to combine multiple PPTX files into one.

```csharp
String inputFilePath1 = Program.RootPath + "\\" + "1.pptx";
String inputFilePath2 = Program.RootPath + "\\" + "2.pptx";
String outputFilePath = Program.RootPath + "\\" + "Output.pptx";
String[] inputFilePaths = new String[2] { inputFilePath1, inputFilePath2 };

// Combine two PowerPoint files.
PPTXDocument.CombineDocument(inputFilePaths, outputFilePath);

```

## Support & Documents

- C# How to Guide : https://www.rasteredge.com/how-to/csharp-imaging/powerpoint-reading/
- Email : support@rasteredge.com




