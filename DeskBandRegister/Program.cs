using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskBandRegister.Properties;

namespace DeskBandRegister {
    using System.Diagnostics;

    class Program {
        static string ExtractTool() {
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

            return exePath;
        }

        static void Main(string[] args) {
            if (args.Length < 1) return;
            var exe = ExtractTool();
            string dll = args[0];
            var process = new Process();
            process.StartInfo.Arguments = $"/codebase {dll}";
            process.StartInfo.FileName = exe;
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.Verb = "runas";
            process.Start();
        }
    }
}
