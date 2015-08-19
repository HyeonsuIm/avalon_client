using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron
{
    class UserInfo
    {
        string id, nick;
        int win, lose, draw;
        int Index;
        bool isHost;
        Avalron.CharacterCard.Card Card;

        public UserInfo(string id, string nick)
        {
            this.id = id;
            this.nick = nick;
        }

        public Avalron.CharacterCard.Card card
        {
            get
            {
                return Card;
            }
        }

        public int index
        {
            get
            {
                return Index;
            }
        }
        public bool checkHost()
        {
            return isHost;
        }

        public void getScore()
        {
            //Program.tcp.DataSend(0, "");
            
        }

        public string getNick()
        {
            return nick;
        }
    }
}
