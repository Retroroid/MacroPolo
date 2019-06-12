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

namespace MacroPolo {
    class Screencap {
        // ---------------------------------------------------------------- //
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