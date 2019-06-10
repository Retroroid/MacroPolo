using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ---------------------------------------------------------------- //
namespace MacroPolo {
    class Program {
        static void Main(string[] args) {
            string m = @"C:\\Users\\lucask\\testmat\\";
            // ---------------------------------------------------------------- //
            screencap.ScreencapS(m + "testest.bmp");

            VirtualMouse.MoveTo(10, 860);
        }
    }
}
