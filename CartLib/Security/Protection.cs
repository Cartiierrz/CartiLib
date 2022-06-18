using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace CartiLib.Security
{
    public class Protection
    {
        private static readonly HashSet<string> Debuggertitles = new HashSet<string>();
        private static readonly HashSet<string> Debuggertitles2 = new HashSet<string>();
        private static readonly byte[] CryptKey =
        {
            59, 38, 75, 70, 33, 77, 33, 104, 56, 94, 105, 84, 58, 60, 41, 97, 63, 126, 109, 88, 101, 78, 42, 126, 111,
            63, 103, 78, 91, 118, 64, 114, 81, 61, 66
        };
        private static readonly byte[] SaltBytes =
        {
            102, 51, 111, 51, 75, 45, 49, 49, 61, 71, 45, 78, 55, 86, 74, 116, 111, 122, 79, 87, 82, 114, 61, 40, 116,
            78, 90, 66, 102, 75, 43, 98, 83, 55, 70, 121
        };

        public static void CheckTitle() // checks the console title from any sus names that reversers would use 
        {
            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var checkerFolder = Path.Combine(appDataFolder, $"./{AsciiVariables.AppName}");
            switch (Directory.Exists(checkerFolder)) //Checks if the appdata folder exists if not creates
            {
                case false:
                    Directory.CreateDirectory(checkerFolder);
                    break;
            }
            switch (File.Exists(checkerFolder + $"./v05m15034mfqw.{AsciiVariables.AppName}")) //checks if the trigger file exists if it does exits the program and deletes itself
            {
                case true:
                    var exepath = Assembly.GetEntryAssembly()?.Location;
                    var info = new ProcessStartInfo("cmd.exe", "/C ping 1.1.1.1 -n 1 -w 3000 > Nul & Del \"" + exepath + "\"")
                    {
                        WindowStyle = ProcessWindowStyle.Hidden
                    };
                    Process.Start(info)?.Dispose();
                    Environment.Exit(0);
                    break;
            }
            switch (Console.Title.ToLower().Contains("reversed") || Console.Title.ToLower().Contains("bypassed") || Console.Title.ToLower().Contains("patched") || Console.Title.ToLower().Contains("patched.to") || Console.Title.ToLower().Contains("reversed by patched.to") || Console.Title.ToLower().Contains("reversed by p.to") || Console.Title.ToLower().Contains("pto") || Console.Title.ToLower().Contains("p.to") || Console.Title.ToLower().Contains("cracked.io") || Console.Title.ToLower().Contains("cracked.to") || Console.Title.ToLower().Contains("cracked") || Console.Title.ToLower().Contains("reversed by"))
            {
                case true:
                    File.Create(checkerFolder + $"./v05m15034mfqw.{AsciiVariables.AppName}");
                    var exepath = Assembly.GetEntryAssembly()?.Location;
                    var info = new ProcessStartInfo("cmd.exe", "/C ping 1.1.1.1 -n 1 -w 3000 > Nul & Del \"" + exepath + "\"")
                    {
                        WindowStyle = ProcessWindowStyle.Hidden
                    };
                    Process.Start(info)?.Dispose();
                    Environment.Exit(0);
                    break;
            }
        }
        public static string Decrypt(byte[] bytesToBeDecrypted) // this is used to decrypt the api to grab a users IP on debugger detected
        {
            byte[] decryptedBytes;
            using (var ms = new MemoryStream())
            {
                using (var aes = new RijndaelManaged())
                {
                    aes.KeySize = 256;
                    aes.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(CryptKey, SaltBytes, 1000);
                    aes.Key = key.GetBytes(aes.KeySize / 8);
                    aes.IV = key.GetBytes(aes.BlockSize / 8);
                    aes.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }

            return Encoding.UTF8.GetString(decryptedBytes);
        }
        public static string GetPublicIp()
        {
            var externalip = new WebClient().DownloadString(Decrypt(new byte[] { 23, 61, 220, 157, 111, 249, 43, 180, 122, 28, 107, 102, 60, 187, 44, 39, 44, 238, 221, 5, 238, 56, 3, 133, 224, 68, 195, 226, 41, 226, 22, 191 })).Replace("\n", "");
            return externalip;
        }
        public static void DebuggerAlert(string procname)
        {
            WebRequest wr = (HttpWebRequest)WebRequest.Create($"{ProtectionVariables.SecurityWebhook}");
            wr.ContentType = "application/json";
            wr.Method = "POST";
            using (var sw = new StreamWriter(wr.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(new
                {

                    username = $"{AsciiVariables.AppName} Security",
                    embeds = new[]
                    {
                        new
                        {
                            description = $"{AsciiVariables.AppName} has detected the use of a debugger, the exe has been removed from the users PC" +
                                          $"\n -> Debugger: {procname}" +
                                          $"\n -> User Was Logged In: {LocalVariables.IsLoggedIn}" +
                                          $"\n -> Program Version: {LocalVariables.Version}" +
                                          $"\n -> Computer Username: {WindowsIdentity.GetCurrent().Name}" +
                                          $"\n -> {AsciiVariables.AppName} License or Username: {LocalVariables.Username}" +
                                          $"\n -> Users IP Address: ||{GetPublicIp()}||" +
                                          $"\n -> Users HWID: ||{WindowsIdentity.GetCurrent().User?.Value}||",
                            title = $"Debugger Detected",
                            color = "00000",

                        }
                    }
                }); ;
                sw.Write(json);
            }
            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var checkerFolder = Path.Combine(appDataFolder, $"./{AsciiVariables.AppName}");
            switch (Directory.Exists(checkerFolder))
            {
                case false:
                    Directory.CreateDirectory(checkerFolder);
                    break;
            }
            switch (File.Exists(checkerFolder + $"./v05m15034mfqw.{AsciiVariables.AppName}"))
            {
                case true:
                    var exepath = Assembly.GetEntryAssembly()?.Location;
                    var info = new ProcessStartInfo("cmd.exe", "/C ping 1.1.1.1 -n 1 -w 3000 > Nul & Del \"" + exepath + "\"")
                    {
                        WindowStyle = ProcessWindowStyle.Hidden
                    };
                    Process.Start(info)?.Dispose();
                    Environment.Exit(0);
                    break;
                case false:
                    File.Create(checkerFolder + $"./v05m15034mfqw.{AsciiVariables.AppName}"); var exepath1 = Assembly.GetEntryAssembly()?.Location;
                    var info1 = new ProcessStartInfo("cmd.exe", "/C ping 1.1.1.1 -n 1 -w 3000 > Nul & Del \"" + exepath1 + "\"")
                    {
                        WindowStyle = ProcessWindowStyle.Hidden
                    };
                    Process.Start(info1)?.Dispose();
                    Environment.Exit(0);
                    break;
            }
        }
        public static void DebuggerCheck()
        {
            for (; ; )
            {
                switch (Debuggertitles.Count)
                {
                    case 0 when Debuggertitles2.Count == 0:
                        DebuggersList();
                        break;
                }
                var processes = Process.GetProcesses();
                foreach (var process in processes)
                {
                    switch (Debuggertitles.Contains(process.ProcessName))
                    {
                        case false when !Debuggertitles2.Contains(process.MainWindowTitle):
                            continue;
                        default:
                            try
                            {
                                DebuggerAlert(process.ProcessName);
                                var exepath = Assembly.GetEntryAssembly()?.Location;
                                var info = new ProcessStartInfo("cmd.exe", "/C ping 1.1.1.1 -n 1 -w 3000 > Nul & Del \"" + exepath + "\"")
                                {
                                    WindowStyle = ProcessWindowStyle.Hidden
                                };
                                Process.Start(info)?.Dispose();
                                Environment.Exit(0);
                            }
                            catch
                            {
                                DebuggerAlert(process.ProcessName);
                                var exepath = Assembly.GetEntryAssembly()?.Location;
                                var info = new ProcessStartInfo("cmd.exe", "/C ping 1.1.1.1 -n 1 -w 3000 > Nul & Del \"" + exepath + "\"")
                                {
                                    WindowStyle = ProcessWindowStyle.Hidden
                                };
                                Process.Start(info)?.Dispose();
                                Environment.Exit(0);
                            }
                            break;
                    }
                }
            }
        }
        private static int DebuggersList()
        {
            int result;
            switch (Debuggertitles.Count > 0)
            {
                case true when Debuggertitles2.Count > 0:
                    result = 1;
                    break;
                default:
                    Debuggertitles.Add("ollydbg");
                    Debuggertitles.Add("ida");
                    Debuggertitles.Add("ida64");
                    Debuggertitles.Add("idag");
                    Debuggertitles.Add("idag64");
                    Debuggertitles.Add("idaw");
                    Debuggertitles.Add("idaw64");
                    Debuggertitles.Add("idaq");
                    Debuggertitles.Add("idaq64");
                    Debuggertitles.Add("idau");
                    Debuggertitles.Add("idau64");
                    Debuggertitles.Add("scylla");
                    Debuggertitles.Add("scylla_x64");
                    Debuggertitles.Add("scylla_x86");
                    Debuggertitles.Add("protection_id");
                    Debuggertitles.Add("x64dbg");
                    Debuggertitles.Add("x32dbg");
                    Debuggertitles.Add("windbg");
                    Debuggertitles.Add("reshacker");
                    Debuggertitles.Add("ImportREC");
                    Debuggertitles.Add("IMMUNITYDEBUGGER");
                    Debuggertitles.Add("MegaDumper");
                    Debuggertitles.Add("dnSpy");
                    Debuggertitles.Add("Charles");
                    Debuggertitles.Add("HTTP Toolkit");
                    Debuggertitles.Add("HTTP Debugger");
                    Debuggertitles.Add("Fiddler");
                    Debuggertitles.Add("ILSpy");
                    Debuggertitles.Add("0harmony");
                    Debuggertitles.Add("0Harmony");
                    Debuggertitles.Add("x32dbg");
                    Debuggertitles.Add("sharpod");
                    Debuggertitles.Add("xd");
                    Debuggertitles.Add("extremedumper");
                    Debuggertitles.Add("dojandqwklndoqwd");
                    Debuggertitles.Add("dojandqwklndoqwd-x86");
                    Debuggertitles.Add("ksdumper");
                    Debuggertitles.Add("KsDumper");
                    Debuggertitles.Add("KsDumper v1.1 - By EquiFox");
                    Debuggertitles.Add("ksdumper v1.1 - by equifox");
                    Debuggertitles.Add("x64dbg");
                    Debuggertitles.Add("x32_dbg");
                    Debuggertitles.Add("x64_dbg");
                    Debuggertitles.Add("strongod");
                    Debuggertitles.Add("PhantOm");
                    Debuggertitles.Add("titanHide");
                    Debuggertitles.Add("scyllaHide");
                    Debuggertitles.Add("ilspy");
                    Debuggertitles.Add("graywolf");
                    Debuggertitles.Add("simpleassemblyexplorer");
                    Debuggertitles.Add("MegaDumper");
                    Debuggertitles.Add("megadumper");
                    Debuggertitles.Add("X64NetDumper");
                    Debuggertitles.Add("x64netdumper");
                    Debuggertitles.Add("process hacker 2");
                    Debuggertitles.Add("ollydbg");
                    Debuggertitles.Add("x32dbg");
                    Debuggertitles.Add("x64dbg");
                    Debuggertitles.Add("ida -");
                    Debuggertitles.Add("charles");
                    Debuggertitles.Add("dnspy");
                    Debuggertitles.Add("httpanalyzer");
                    Debuggertitles.Add("httpdebug");
                    Debuggertitles.Add("fiddler");
                    Debuggertitles.Add("wireshark");
                    Debuggertitles.Add("proxifier");
                    Debuggertitles.Add("mitmproxy");
                    Debuggertitles.Add("process hacker");
                    Debuggertitles.Add("process monitor");
                    Debuggertitles.Add("process hacker 2");
                    Debuggertitles.Add("system explorer");
                    Debuggertitles.Add("systemexplorer");
                    Debuggertitles.Add("systemexplorerservice");
                    Debuggertitles.Add("WPE PRO");
                    Debuggertitles.Add("ghidra");
                    Debuggertitles.Add("x32dbg");
                    Debuggertitles.Add("x64dbg");
                    Debuggertitles.Add("ollydbg");
                    Debuggertitles.Add("ida -");
                    Debuggertitles.Add("charles");
                    Debuggertitles.Add("dnspy");
                    Debuggertitles.Add("httpanalyzer");
                    Debuggertitles.Add("httpdebug");
                    Debuggertitles.Add("harmony");
                    Debuggertitles.Add("http debugger");
                    Debuggertitles.Add("fiddler");
                    Debuggertitles.Add("wireshark");
                    Debuggertitles.Add("dbx");
                    Debuggertitles.Add("mdbg");
                    Debuggertitles.Add("gdb");
                    Debuggertitles.Add("windbg");
                    Debuggertitles.Add("dbgclr");
                    Debuggertitles.Add("kdb");
                    Debuggertitles.Add("httpdebugger");
                    Debuggertitles.Add("kgdb");
                    Debuggertitles.Add("mdb");
                    Debuggertitles.Add("folderchangesview");
                    Debuggertitles2.Add("ILSpy");
                    Debuggertitles2.Add("0harmony");
                    Debuggertitles2.Add("0Harmony");
                    Debuggertitles2.Add("x32dbg");
                    Debuggertitles2.Add("sharpod");
                    Debuggertitles2.Add("xd");
                    Debuggertitles2.Add("extremedumper");
                    Debuggertitles2.Add("dojandqwklndoqwd");
                    Debuggertitles2.Add("dojandqwklndoqwd-x86");
                    Debuggertitles2.Add("IDA");
                    Debuggertitles2.Add("Renamed by LockT (x86)");
                    Debuggertitles2.Add("LockT");
                    Debuggertitles2.Add("by LockT");
                    Debuggertitles2.Add("Renamed by");
                    Debuggertitles2.Add("Astro Renamer");
                    Debuggertitles2.Add("-astro");
                    Debuggertitles2.Add("ksdumper");
                    Debuggertitles2.Add("KsDumper");
                    Debuggertitles2.Add("KsDumper v1.1 - By EquiFox");
                    Debuggertitles2.Add("ksdumper v1.1 - by equifox");
                    Debuggertitles2.Add("x64dbg");
                    Debuggertitles2.Add("x32_dbg");
                    Debuggertitles2.Add("x64_dbg");
                    Debuggertitles2.Add("strongod");
                    Debuggertitles2.Add("PhantOm");
                    Debuggertitles2.Add("titanHide");
                    Debuggertitles2.Add("scyllaHide");
                    Debuggertitles2.Add("ilspy");
                    Debuggertitles2.Add("graywolf");
                    Debuggertitles2.Add("simpleassemblyexplorer");
                    Debuggertitles2.Add("MegaDumper");
                    Debuggertitles2.Add("megadumper");
                    Debuggertitles2.Add("X64NetDumper");
                    Debuggertitles2.Add("x64netdumper");
                    Debuggertitles2.Add("process hacker 2");
                    Debuggertitles2.Add("ollydbg");
                    Debuggertitles2.Add("x32dbg");
                    Debuggertitles2.Add("x64dbg");
                    Debuggertitles2.Add("ida -");
                    Debuggertitles2.Add("charles");
                    Debuggertitles2.Add("dnspy");
                    Debuggertitles2.Add("httpanalyzer");
                    Debuggertitles2.Add("httpdebug");
                    Debuggertitles2.Add("fiddler");
                    Debuggertitles2.Add("wireshark");
                    Debuggertitles2.Add("proxifier");
                    Debuggertitles2.Add("mitmproxy");
                    Debuggertitles2.Add("process hacker");
                    Debuggertitles2.Add("process monitor");
                    Debuggertitles2.Add("process hacker 2");
                    Debuggertitles2.Add("system explorer");
                    Debuggertitles2.Add("systemexplorer");
                    Debuggertitles2.Add("systemexplorerservice");
                    Debuggertitles2.Add("WPE PRO");
                    Debuggertitles2.Add("ghidra");
                    Debuggertitles2.Add("x32dbg");
                    Debuggertitles2.Add("x64dbg");
                    Debuggertitles2.Add("ollydbg");
                    Debuggertitles2.Add("ida -");
                    Debuggertitles2.Add("charles");
                    Debuggertitles2.Add("dnspy");
                    Debuggertitles2.Add("httpanalyzer");
                    Debuggertitles2.Add("httpdebug");
                    Debuggertitles2.Add("harmony");
                    Debuggertitles2.Add("http debugger");
                    Debuggertitles2.Add("fiddler");
                    Debuggertitles2.Add("wireshark");
                    Debuggertitles2.Add("dbx");
                    Debuggertitles2.Add("mdbg");
                    Debuggertitles2.Add("gdb");
                    Debuggertitles2.Add("windbg");
                    Debuggertitles2.Add("dbgclr");
                    Debuggertitles2.Add("kdb");
                    Debuggertitles2.Add("httpdebugger");
                    Debuggertitles2.Add("kgdb");
                    Debuggertitles2.Add("mdb");
                    Debuggertitles2.Add("folderchangesview");
                    Debuggertitles2.Add("OLLYDBG");
                    Debuggertitles2.Add("ida");
                    Debuggertitles2.Add("disassembly");
                    Debuggertitles2.Add("scylla");
                    Debuggertitles2.Add("Debug");
                    Debuggertitles2.Add("[CPU");
                    Debuggertitles2.Add("Immunity");
                    Debuggertitles2.Add("WinDbg");
                    Debuggertitles2.Add("x32dbg");
                    Debuggertitles2.Add("x64dbg");
                    Debuggertitles2.Add("Import reconstructor");
                    Debuggertitles2.Add("MegaDumper");
                    Debuggertitles2.Add("MegaDumper 1.0 by CodeCracker / SnD");
                    Debuggertitles2.Add("HTTP Debugger");
                    Debuggertitles2.Add("Fiddler");
                    Debuggertitles2.Add("HTTP Toolkit");
                    Debuggertitles2.Add("Charles");
                    result = 0;
                    break;
            }
            return result;
        }
    }
}
