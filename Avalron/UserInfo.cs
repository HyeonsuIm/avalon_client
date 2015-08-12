using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron
{
    public class UserInfo
    {
        string index, nick;
        int win, lose, draw;
        bool isHost;

        public UserInfo(string nick, string index)
        {
            this.index = index;
            this.nick = nick;
        }

        public bool checkHost()
        {
            return isHost;
        }

        public string GetIndex()
        {
            return index;
        }

        public string GetNick()
        {
            return nick;
        }

        public int getWin()
        {
            return win;
        }

        public int getLose()
        {
            return lose;
        }

        public int getDraw()
        {
            return draw;
        }

        public void setScore(int win, int lose, int draw)
        {
            this.win = win;
            this.lose = lose;
            this.draw = draw;
        }
    }
}
