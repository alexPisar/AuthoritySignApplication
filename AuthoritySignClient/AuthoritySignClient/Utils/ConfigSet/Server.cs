using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthoritySignClient.Utils.ConfigSet
{
    public class Server
    {
        public string Name { get; set; }
        public string SID { get; set; }
        public string IP { get; set; }

        public string FullName => $"{IP}/{SID}";
    }
}
