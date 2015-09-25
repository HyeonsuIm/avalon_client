using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron.TCP
{
    class ParameterCounter
    {
        public static int pc(string line)
        {
            int count = 1;
            if (line.Equals("")) { count = 0; }
            // 
            foreach (char c in line)
                if (c.Equals(TCPClient.delimiter[0])) count++;
            
            return count;
        }
    }
}
