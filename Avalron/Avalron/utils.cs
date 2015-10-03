using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron.Avalron
{
    class IpSplit 
    {
        public string host
        {
            get;
        }
        public int port
        {
            get;
        }

        public IpSplit(string str)
        {
            int colonIndex = str.IndexOf(':');
            if (colonIndex == -1)
            {
                // Or whatever
                throw new ArgumentException("Invalid host:port format");
            }
            host = str.Substring(0, colonIndex);
            string portStr = str.Substring(colonIndex + 1);
            port = Convert.ToInt32(portStr);
        }
    }
}
