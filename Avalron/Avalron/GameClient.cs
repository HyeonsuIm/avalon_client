using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron.Avalron
{
    public class GameClient : TCPClient
    {
        enum Game {AVALON};
        enum OpCode { CHATSEND };

        public GameClient(string address) : base(address)
        {

        }

        public void ChatSend(string nick, string line)
        {
            //Send((int)FormNum.GAME + (int)OpCode.CHATSEND + "02" + nick + delimiter +  line);
            DataSend(Convert.ToInt32((int)FormNum.LOBBY + "0" +(int)OpCode.CHATSEND + "02"), nick + delimiter + line);
        }

        public bool IsClosing()
        {
            //if (base.recv == -1 || base.recv == 0)
            if(Avalron.closing)
                return true;
            return false;
        }
    }
}
