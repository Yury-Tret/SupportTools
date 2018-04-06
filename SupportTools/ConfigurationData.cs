using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTools
{
    public class ConfigurationData
    {
        public string username { get; set; }
        public string password { get; set; }
        public string key { get; set; }
        public string startPage { get; set; }
        public bool enableLogging;
        public string logPath { get; set; }

    }
}
