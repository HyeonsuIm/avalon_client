using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron
{
    class UserInfo
    {
        string id, nick, ip;
        int win, lose, draw;
        bool isHost;

        public UserInfo(string id, string nick, string ip)
        {
            this.id = id;
            this.nick = nick;
            this.ip = ip;
        }

        public bool checkHost()
        {
            return isHost;
        }

        public void getScore()
        {
            Program.tcp.DataSend("", "");
            
        }
    }
}
