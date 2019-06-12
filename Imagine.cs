using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ---------------------------------------------------------------- //
namespace MacroPolo {
    class Imagine {
        // ---------------------------------------------------------------- //
        public static bool VarMatch(Bitmap a, Bitmap b, double grain, float tol) {
            GraphicsUnit u = GraphicsUnit.Pixel;
            int equalElements = 0;
            int[,] alist = GetVariableHash(a, grain);
            int[,] blist = GetVariableHash(b, grain);
            for (int i = 0; i < a.Width; i++) {
                for (int j = 0; j < a.Height; j++) {
                    if (alist[i, j] == blist[i, j]) equalElements++;
                }
            }
            return equalElements > alist.Length * tol;
        }
        // ---------------------------------------------------------------- //
        public static int[,] GetVariableHash(Bitmap bmp, double grain) { // bmp the source image, grain the grain size (0-100 I think)
            // Count the number of intervals based on the grain, aka the number of shades to be used to re-draw the bmp. Then, get the bounds.
            int count = Convert.ToInt32(Math.Ceiling(100 / grain));
            GraphicsUnit unit = GraphicsUnit.Pixel;
            RectangleF rectf = bmp.GetBounds(ref unit);
            Bitmap bmpte = new Bitmap(bmp, new Size((int)rectf.Right, (int)rectf.Bottom));
            // Initialize and then fill the array with the shade of gray corresponding to each pixel's brightness.
            int[,] grayscaled = new int[bmpte.Size.Width, bmpte.Size.Height];
            for (int j = 0; j < bmpte.Height; j++) {
                for (int i = 0; i < bmpte.Width; i++) {
                    // Get each pixel's brightness
                    float bright = bmpte.GetPixel(i, j).GetBrightness();
                    // Set the color flag based on how big your grains are. 
                    // Brightness / grain size = a number between the integer multiples of grain, truncate to the int. The math works.
                    grayscaled[i, j] = (int)(bright / grain);
                }
            }
            return grayscaled;
        }
        // ---------------------------------------------------------------- //
    }
}
