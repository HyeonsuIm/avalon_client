using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron.Avalron
{
    class GameClient : TCPClient
    {
        enum Game {AVALON};
        enum OpCode { CHATSEND };

        public GameClient(string address) : base(address)
        {

        }

        public void ChatSend(string nick, string line)
        {
            Send((int)FormNum.GAME + (int)OpCode.CHATSEND + "02" + nick + delimiter +  line);
        }
    }
}
