﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron.Avalron
{
    public class AvalronClient : TCPClient
    {
        enum Game {Avalron};
        enum OpCode { CHATSEND };

        public AvalronClient() : base()
        {

        }

        public AvalronClient(string address) : base(address)
        {

        }

        public void ChatSend(string nick, string line)
        {
            //Send((int)FormNum.GAME + (int)OpCode.CHATSEND + "02" + nick + delimiter +  line);
            DataSend(Convert.ToInt32((int)FormNum.LOBBY + "0" +(int)OpCode.CHATSEND + "02"), nick + delimiter + line);
        }

        public void WisperSend(string nick, string ToNick, string line)
        {
            DataSend(Convert.ToInt32((int)FormNum.LOBBY + "01" + "03"), nick + delimiter + ToNick + delimiter + line);
        }

   }
}
