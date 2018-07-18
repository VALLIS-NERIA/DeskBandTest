using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskBandRegister.Properties;
using CommandLine;

namespace DeskBandRegister {
    using System.Diagnostics;
    using System.Threading;

    class Options {
        [Option("r", DefaultValue = false)]
        public bool Register { get; set; }

        [Option("u", DefaultValue = false)]
        public bool UnRegister { get; set; }
    }

    class Program {
        static string ExtractTool() {
            var dir = Path.GetTempPath();
            var exePath = dir + "regasm.exe";
            var cfgPath = dir + "regasm.exe.config";
            var batPath = dir + "restart_explorer.bat";
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

            if (!File.Exists(batPath)) {
                using (var s = new FileStream(batPath, FileMode.Create)) {
                    s.Write(Resources.restart_explorer, 0, Resources.restart_explorer.Length);
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
                    Arguments = "/c \"taskkill /f /im explorer.exe& timeout 1&explorer\"",
                    UseShellExecute = false,
                    FileName = "cmd"
                }
            };
            process.Start();
            process.Dispose();
        }
        static void Main(string[] args) {
            if (args.Length < 1) return;
            var options = new Options();

            Parser.Default.ParseArguments(args, options) ;
            ;
            CleanTemp();
            string dll = args.Last();
            var exe = ExtractTool();
            UnRegister(exe, dll);
            KillExplorer();
            //Thread.Sleep(5000);
            //Process.Start("explorer");
            //Register(exe, dll);
            try {
                CleanTemp();
            }
            catch(UnauthorizedAccessException) { }

            Process.Start(new ProcessStartInfo
            {
                Arguments = "/c \" timeout -1\"",
                UseShellExecute = false,
                FileName = "cmd"
            });
        }
    }
}
