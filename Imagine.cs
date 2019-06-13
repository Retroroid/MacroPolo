using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
// ---------------------------------------------------------------- //
namespace MacroPolo {
    class Imagine {
        // ---------------------------------------------------------------- //
        public static bool VarMatch(Bitmap a, Bitmap b, double grain, float tol) {
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
        private static int[,] GetVariableHash(Bitmap bmp, double grain) { // bmp the source image, grain the grain size (0-100 I think)
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
        // Stuff for the screenshot features
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        // ---------------------------------------------------------------- //
        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hWnd);
        private const int SW_RESTORE = 9;
        [DllImport("user32.dll")]
        private static extern IntPtr ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
        // ---------------------------------------------------------------- //
        public static Bitmap CaptureApplication(string procName) {
            Process proc;
            // Cater for cases when the process can't be located.
            try {
                proc = Process.GetProcessesByName(procName)[0];
            }
            catch (IndexOutOfRangeException e) {
                Console.WriteLine(e.Message);
                return null;
            }
            // You need to focus on the application
            SetForegroundWindow(proc.MainWindowHandle);
            ShowWindow(proc.MainWindowHandle, SW_RESTORE);
            Thread.Sleep(500);
            Rect rect = new Rect();
            IntPtr error = GetWindowRect(proc.MainWindowHandle, ref rect);

            // sometimes it gives error.
            while (error == (IntPtr)0) {
                error = GetWindowRect(proc.MainWindowHandle, ref rect);
            }

            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics.FromImage(bmp).CopyFromScreen(rect.left,
                                                   rect.top,
                                                   0,
                                                   0,
                                                   new Size(width, height),
                                                   CopyPixelOperation.SourceCopy);
            return bmp;
        }
        // ---------------------------------------------------------------- //
        public static Bitmap CaptureScreen() {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height)) {
                using (Graphics g = Graphics.FromImage(bitmap)) {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                return bitmap;
            }
        }
        // ---------------------------------------------------------------- //
    }
}
