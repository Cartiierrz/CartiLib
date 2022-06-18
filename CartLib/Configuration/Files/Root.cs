using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartiLib.Configuration.Files
{
    public class Root
    {
        public Settings Settings { get; set; }
        public Proxies Proxies { get; set; }
        public Checker Checker { get; set; }
    }
}
