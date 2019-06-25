using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// ---------------------------------------------------------------- //
namespace MacroPolo {
    class Program {
        static void Main(string[] args) {
            // string m = @"C:\\Users\\lucask\\testmat\\";
            //string path = @"C:\\Windows\\System32\\mspaint.exe";
            //Ignition.Ignite(path);
            // ---------------------------------------------------------------- //
            Thread.Sleep(5000);
            VirtualBoard.Send(VirtualBoard.ScanCodeShort.F5);
        }
    }
}