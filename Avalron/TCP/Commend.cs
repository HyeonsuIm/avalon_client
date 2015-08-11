using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron
{
    class Commend
    {
        public char delimiter = '\u0001'; // 구분자

        // opcode 정의
        const string chat = "10001";
        const string nomalExit = "90000";
        const string wisper = "10102";
        const string roomRefresh = "10200";
        const string userRefresh = "10300";
        const string roomMake = "10404";

        public string order(string opcode)
        {
            switch (opcode)
            {
                case "chat":
                    opcode = chat;
                    break;
                case "wisper":
                    opcode = wisper;
                    break;
                case "roomRefresh":
                    opcode = roomRefresh;
                    break;
                case "userRefresh":
                    opcode = userRefresh;
                    break;
                case "roomMake":
                    opcode = roomMake;
                    break;
                case "nomalExit":
                    opcode = nomalExit;
                    break;
            }
            return opcode;
        }
    }
}
