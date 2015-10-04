using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron.Avalron
{
    public class AvalronClient : TCPClient
    {
        enum ChattingOpCode { CHATSEND = 800 };

        public AvalronClient() : base()
        {

        }

        public AvalronClient(string address, int port) : base(address, port)
        {

        }

        public void ChatSend(string nick, string line)
        {
            //Send((int)FormNum.GAME + (int)OpCode.CHATSEND + "02" + nick + delimiter +  line);
            DataSend(Convert.ToInt32((int)FormNum.LOBBY + "0" +(int)ChattingOpCode.CHATSEND), nick + delimiter + line);
        }

        public void WisperSend(string nick, string ToNick, string line)
        {
            DataSend(Convert.ToInt32((int)FormNum.LOBBY + "01"), nick + delimiter + ToNick + delimiter + line);
        }

        public void avalronRecve()
        {
            string getString = "";
            while(Program.state % 10 == 3)
            {
                try {
                    getString = Program.tcp.ReciveData() + "\n";
                }
                catch(System.Net.Sockets.SocketException e)
                {
                    MessageBoxEx.Show(e.Message);
                }
                 
                if (getString == "")
                    continue;

                Spriter spriter = new Spriter(getString);
                int opCode = spriter.getJustOpCode();

                switch (opCode)
                {
                    case (int)ChattingOpCode.CHATSEND:
                        Program.avalron.chatting.addText(getString);
                        break;
                    default:
                        break;
                }
                
                // 채팅 금지시
                if(false)
                {
                    Program.avalron.chatting.ChatBoxEnabled = false;
                }

                // 채팅 금지 해지시
                if(false)
                {
                    Program.avalron.chatting.ChatBoxEnabled = true;
                }
            }
        }
   }
}
