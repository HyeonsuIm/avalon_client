using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron
{
    public class UserInfo
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

        public string GetID()
        {
            return id;
        }

        public string GetNick()
        {
            return nick;
        }

        public string GetIp()
        {
            return ip;
        }

        public void getScore()
        {

        }

        public void setScore(int win, int lose, int draw)
        {
            this.win = win;
            this.lose = lose;
            this.draw = draw;
        }
    }
}
