using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartiLib.Configuration.Files
{
    public class Proxies
    {
        public int ProxyTimeout { get; set; }
        public bool UseProxyAuth { get; set; }
        public string ProxyAuthType { get; set; }
    }
}
