# XImage.Barcode Reader is a C# barcode scanning library to read, scan QR Code, Code 128 and many 2d and 1d barcode formats in .NET projects.


### Barcode Reader from RasterEdge is an advanced barcode scanner library :
- Read, scan QR Code, Data Matrix, PDF-417, Code 39 / 128, EAN/UPC, GS1-128, and other 1d barcode formats
- Recognise multiple barcodes from a single file with fast reading speed and high recognition rate
- Support reading barcodes from PDF, multi-page TIFF, GIF, BMP, JPEG, TIFF, PNG image files, and stream objects


### Compatible with
- .NET Standard 2.0
- .NET 8, .NET 7, .NET 6, .NET 5, .NET Core 3.x & 2.x
- .NET Framework 4.x
- Windows, Mac, Linux, Docker, Azure


## Get to Start

Once installed the package, you can use the following C# code to scan, recogize barcodes from images in C# application

```csharp
// load PDF document
PDFDocument doc = new PDFDocument(inputDirectory + "Sample_Barcode.pdf");

// get the page you want to scan
BasePage page = doc.GetPage(0);

// set reader setting
ReaderSettings setting = new ReaderSettings();

// set type to read
setting.AddTypesToRead(BarcodeType.EAN13);

// read barcode from PDF page
Barcode[] barcodes = BarcodeReader.ReadBarcodes(setting, page);

foreach (Barcode barcode in barcodes)
{
	// print the loaction of barcode on image
                Console.WriteLine(barcode.BoundingRectangle.X + "  " + barcode.BoundingRectangle.Y);

                // output barcode data onto screen 
                Console.WriteLine(barcode.DataString);
}

```

## Support & Documents

- C# How to Guide : https://www.rasteredge.com/how-to/csharp-imaging/read-barcode-csharp/
- Email : support@rasteredge.com




