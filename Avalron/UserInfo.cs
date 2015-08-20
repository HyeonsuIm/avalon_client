using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron
{
    public class UserInfo
    {
        public string id
        {
            get; set;
        }

        public string nick
        {
            get; set;
        }

        public int win
        {
            get; set;
        }

        public int lose
        {
            get; set;
        }

        public int draw
        {
            get; set;
        }

        public int index
        {
            get; set;
        }

        public bool isHost
        {
            get; set;
        }

        public UserInfo(string nick, int index)
        {
            this.nick = nick;
            this.index = index;
        }

        public void getScore()
        {
            //Program.tcp.DataSend(0, "");
            
        }

        public void setScore(int win, int lose, int draw)
        {
            this.win = win;
            this.lose = lose;
            this.draw = draw;
        }
    }
}
