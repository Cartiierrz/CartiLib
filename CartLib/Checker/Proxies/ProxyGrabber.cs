using System;

namespace CartiLib.Checker.Proxies
{
    public class ProxyGrabber
    {
        public static string Init() //Returns a random proxy
        {
            return CheckerVariables.Proxies[new Random().Next(CheckerVariables.Proxies.Count)];
        }
    }
}
