# XImage.Raster is a FREE C# imaging library to create, convert, process, preview, edit raster image file in .NET projects.


### XImage.Raster from RasterEdge is an advanced raster image processing library :
- It is FREE
- Generate image file from PDF, Word, Excel, PowerPoint, and other JPG, PNG, BITMAP image formats
- Convert image file to PDF, multi-page TIFF documents
- Read, save, compress, process, preview image files
- Read QR Code, barcode, text content from image file

### Compatible with
- .NET Standard 2.0
- .NET 8, .NET 7, .NET 6, .NET 5, .NET Core 3.x & 2.x
- .NET Framework 4.x
- Windows, Mac, Linux, Docker, Azure


## Get to Start

Once installed the package, you can use the following C# code to convert bmp to png file.

```csharp
String inputFilePath = Program.RootPath + "\\"  +  "input.bmp";
String outputFilePath = Program.RootPath + "\\"  +  "output.png";

RasterImage img = new RasterImage(inputFilePath);
//Convert format and save.
img.Save(outputFilePath);

```

## Support & Documents

- C# How to Guide : https://www.rasteredge.com/how-to/csharp-imaging/
- Email : support@rasteredge.com




