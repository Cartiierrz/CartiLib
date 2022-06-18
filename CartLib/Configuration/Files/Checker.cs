using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartiLib.Configuration.Files
{
    public class Checker
    {
        public int Threads { get; set; }
        public int Retries { get; set; }
        public bool UseGui { get; set; }
    }
}
