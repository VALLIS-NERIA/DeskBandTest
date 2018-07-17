using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskBandRegister.Properties;

namespace DeskBandRegister {
    using System.Diagnostics;
    using System.Threading;

    class Program {
        static string ExtractTool() {
            var dir = Path.GetTempPath();
            var exePath = dir + "regasm.exe";
            var cfgPath = dir + "regasm.exe.config";
            if (!File.Exists(exePath)) {
                using (var s = new FileStream(exePath, FileMode.Create)) {
                    if (Environment.Is64BitOperatingSystem) {
                        s.Write(Resources.RegAsm_64, 0, Resources.RegAsm_64.Length);
                    }
                    else {
                        s.Write(Resources.RegAsm_32, 0, Resources.RegAsm_32.Length);
                    }
                }
            }

            if (!File.Exists(cfgPath)) {
                using (var s = new FileStream(cfgPath, FileMode.Create)) {
                    s.Write(Resources.regasm_exe, 0, Resources.regasm_exe.Length);
                }
            }

            return exePath;
        }

        static void CleanTemp() {
            var dir = Path.GetTempPath();
            var exePath = dir + "regasm.exe";
            var cfgPath = dir + "regasm.exe.config";
            if (File.Exists(exePath)) {
                File.Delete(exePath);
            }

            if (File.Exists(cfgPath)) {
                File.Delete(cfgPath);
            }
        }

        static void Register(string exe, string dll) {
            var process = new Process
            {
                StartInfo =
                {
                    Arguments = $"/codebase /tlb {dll}",
                    UseShellExecute = false,
                    FileName = exe
                }
            };
            process.Start();
            process.Dispose();
        }

        static void UnRegister(string exe, string dll) {
            var process = new Process
            {
                StartInfo =
                {
                    Arguments = $"/u {dll}",
                    UseShellExecute = false,
                    FileName = exe
                }
            };
            process.Start();
            process.Dispose();
        }

        static void KillExplorer() {
            var process = new Process
            {
                StartInfo =
                {
                    Arguments = $"/f /im explorer.exe",
                    UseShellExecute = false,
                    FileName = "taskkill"
                }
            };
            process.Start();
            process.Dispose();
        }
        static void Main(string[] args) {
            if (args.Length < 1) return;
            CleanTemp();
            string dll = args[0];
            var exe = ExtractTool();
            //UnRegister(exe, dll);
            //KillExplorer();
            //Thread.Sleep(5000);
            //Process.Start("explorer");
            Register(exe, dll);
            try {
                CleanTemp();
            }
            catch(UnauthorizedAccessException) { }
        }
    }
}
