using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Slides;

public class PptTextExtractor {
    public void ExtractAllTextFromPpt(string filePath) {
        using (Presentation pres = new Presentation(filePath)) {
            foreach (ISlide slide in pres.Slides) {
                foreach (IShape shape in slide.Shapes) {
                    if (shape is IAutoShape autoShape && autoShape.TextFrame != null) {
                        Console.WriteLine(autoShape.TextFrame.Text);
                    }
                }
            }
        }
    }
}