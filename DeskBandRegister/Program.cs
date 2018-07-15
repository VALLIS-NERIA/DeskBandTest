using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskBandRegister.Properties;

namespace DeskBandRegister {
    class Program {
        static void ExtractTool() {
            var dir = Path.GetTempPath();
            var exePath = dir + "regasm.exe";
            var cfgPath = dir + "regasm.exe.config";
            if(!File.Exists(exePath)) {
                using (var s = new FileStream(exePath,FileMode.Create)) {
                    s.Write(Resources.RegAsm, 0, Resources.RegAsm.Length);
                }
            }

            if (!File.Exists(cfgPath)) {
                using (var s = new FileStream(cfgPath, FileMode.Create)) {
                    s.Write(Resources.regasm_exe, 0, Resources.regasm_exe.Length);
                }
            }
        }

        static void Main(string[] args) {
            ExtractTool();

        }
    }
}
