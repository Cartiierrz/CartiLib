using CartiLib.Checker.Module_Tools;
using CartiLib.Interface;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace CartiLib.Loaders.Proxies
{
    public class ProxyLoader
    {
        public static string EscapedDirectory;

        public static void Init()
        {
            var thread = new Thread(delegate ()
            {
            RestartSelection:
                InterfaceMaker.ResetOptionsCount();
                AsciiMaker.Init("Big.flf");
                InterfaceMaker.CreateOptions("Http");
                InterfaceMaker.CreateOptions("Socks4");
                InterfaceMaker.CreateOptions("Socks5");
                InterfaceMaker.CreateOptions("Proxyless");
                InterfaceMaker.CreateInput(true);
                var pType = Console.ReadLine();
                switch (pType)
                {
                    case "1":
                        CheckerVariables.ProxyProtocol = 1;
                        CheckerVariables.Proxytype = Leaf.xNet.ProxyType.HTTP;
                        goto StartLoad;
                    case "2":
                        CheckerVariables.ProxyProtocol = 2;
                        CheckerVariables.Proxytype = Leaf.xNet.ProxyType.Socks4;
                        goto StartLoad;
                    case "3":
                        CheckerVariables.ProxyProtocol = 3;
                        CheckerVariables.Proxytype = Leaf.xNet.ProxyType.Socks5;
                        goto StartLoad;
                    case "4":
                        CheckerVariables.ProxyProtocol = 4;
                        goto StartLoad;
                    default:
                        goto RestartSelection;
                }
            StartLoad:
                AsciiMaker.Init("Big.flf");
                InterfaceMaker.CreateNotification("Drag & Drop your proxylist");
                InterfaceMaker.CreateInput(false);
                var proxyDirectory = Console.ReadLine();
                switch (proxyDirectory.StartsWith("\"")) // if the directory starts with a " it will auto remove the " to prevent errors in the loading 
                {
                    case true:
                        EscapedDirectory = Functions.Parse(proxyDirectory, "\"", "\"");
                        var fileEscaped = File.ReadAllLines(EscapedDirectory);
                        File.WriteAllLines(EscapedDirectory, fileEscaped.Distinct().ToArray()); // removes exact proxy duplicates
                        foreach (var proxies in File.ReadAllLines(EscapedDirectory))
                        {
                            CheckerVariables.Proxies.Add(proxies); // Adds the proxies to the list for proxies
                        }
                        break;
                    case false:
                        var file = File.ReadAllLines(proxyDirectory);
                        File.WriteAllLines(proxyDirectory, file.Distinct().ToArray());
                        foreach (var proxies in File.ReadAllLines(proxyDirectory))
                        {
                            CheckerVariables.Proxies.Add(proxies);
                        }
                        break;
                }
                AsciiMaker.Init("Big.flf");
                InterfaceMaker.CreateNotification($"Loaded {CheckerVariables.Proxies.Count} total proxies");
                Thread.Sleep(1000);
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }
    }
}
