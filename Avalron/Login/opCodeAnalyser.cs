using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron.Login
{
    class opCodeAnalyser
    {
        Spliter Spliter;

        string[] analyse(byte[] data)
        {
            string line = Encoding.UTF8.GetString(data);
            Spliter = new Spliter(line);

            int opCode = Spliter.getJustOpCode();

            switch(opCode)
            {
                case 1:
                    int b = 0;
                    break;
            }

            return null;
        }
    }
}
