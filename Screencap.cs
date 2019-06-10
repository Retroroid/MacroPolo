using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MacroPolo {
    class screencap : Form {
        public static void ScreencapS(string savePath) {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height)) {
                using (Graphics g = Graphics.FromImage(bitmap)) {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                bitmap.Save(savePath, ImageFormat.Jpeg);
            }
        }
        public static void ScreencapW(string savePath) {
            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                                   Screen.PrimaryScreen.Bounds.Height,
                                                   PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                0,
                                0,
                                Screen.PrimaryScreen.Bounds.Size,
                                CopyPixelOperation.SourceCopy);

            // Save the screenshot to the specified path that the user has chosen.
            bmpScreenshot.Save(savePath, ImageFormat.Png);
        }

    }
}
