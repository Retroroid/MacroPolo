using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Tesseract;

namespace MacroPolo {
    public class Glass {
        // ---------------------------------------------------------------- //
        // class variables
        public Bitmap bit;
        public string path;
        // ---------------------------------------------------------------- //
        // constructor
        public Glass(string path) {
            this.path = path;
            this.bit = new Bitmap(path);
        }
        // ---------------------------------------------------------------- //
        // class methods
        public string ReadPicture() { // Attempt to interpret the bitmap image as text. Not 100% accurate.
            var ocrtext = string.Empty;
            using (var engine = new TesseractEngine(@"C:\\Program Files\\Tesseract-OCR\\tessdata", "eng", EngineMode.Default)) {
                using (var img = PixConverter.ToPix(this.bit)) {
                    using (var page = engine.Process(img)) { ocrtext = page.GetText(); }
                }
            }
            return ocrtext;
        }
        // ---------------------------------------------------------------- //
    }
}