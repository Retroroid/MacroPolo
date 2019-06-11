using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;

namespace MacroPolo {
    class Ignition {
        public static void Ignite(string path) { // Start a program from the specified filepath, path (.exe, or similar)
            try {
                using (Process pro = new Process()) {
                    pro.StartInfo.UseShellExecute = false;
                    pro.StartInfo.FileName = path;
                    pro.Start();
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
