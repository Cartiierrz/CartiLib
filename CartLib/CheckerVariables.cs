using Leaf.xNet;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartiLib
{
    public class CheckerVariables
    {
        // Proxy Variables
        public static int ProxyProtocol = 0;
        public static ProxyType Proxytype = ProxyType.HTTP;
        // Checker Variables
        public static int Cpm = 0;
        public static int CpmI = 0;
        public static int[] CpmList = new int[60];
        public static int PreviousCheckeds = 0;
        public static int TotalCombo = 0;
        public static int Hits = 0;
        public static int Bads = 0;
        public static int Frees = 0;
        public static int Expired = 0;
        public static int Twofa = 0;
        public static int Customs = 0;
        public static int Retries = 0;
        public static int Errors = 0;
        public static int Checked = 0;
        public static List<Task> Tasks = new List<Task>();
        public static ConcurrentQueue<string> Combo = new ConcurrentQueue<string>();
        public static List<string> Proxies = new List<string>();
    }
}
