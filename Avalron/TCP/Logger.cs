using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron.TCP
{
    class Logger
    {
        StreamWriter fileStream ;

        public Logger()
        {
            string time = System.DateTime.Now.ToString("MM_dd_hh_mm_ss");
            fileStream = new StreamWriter(time + ".log");
        }

        ~Logger()
        {
            fileStream.Close();
        }

        public void save(string line)
        {
            string time = System.DateTime.Now.ToString("MM/dd hh:mm:ss  ");
            fileStream.WriteLine(time + line);
            fileStream.Flush();
        }
    }
}
