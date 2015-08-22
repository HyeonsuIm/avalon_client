﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron
{
    class WaitingRoomChatting : Avalron.Chatting
    {
        public WaitingRoomChatting(Control.ControlCollection Controls) : base(Controls)
        {
            formNum = (int)TCPClient.FormNum.ROOM;
        }

        public void RunGetChat()
        {
            string getString = "";
            while(IsClosing() == false)
            {
                try
                {
                    getString = Program.tcp.ReciveData() + "\n";
                }
                catch(System.Net.Sockets.SocketException e)
                {
                    MessageBoxEx.Show(e.Message);
                }

                if (getString == "")
                    continue;

                Spriter spriter = new Spriter(getString);
                int OpCode = spriter.getOpCode();
                string line;

                switch(OpCode)
                {
                    case (int)TCPClient.RoomOpCode.Chat:
                        line = spriter.split[0] + " : " + spriter.split[1] ;
                        addText(line);
                        break;
                    case (int)TCPClient.RoomOpCode.Wisper:
                        if (spriter.split[0] == Program.userInfo.index.ToString())
                            line = spriter.split[0];
                        line = spriter.split[0] + "님으로 부터 : " + spriter.split[1];
                        addText(line);
                        break;
                    case (int)TCPClient.RoomOpCode.Connect:

                        break;
                    case (int)TCPClient.RoomOpCode.DisConnect:
                        break;
                    case (int)TCPClient.RoomOpCode.SeatClose:
                        break;
                    case (int)TCPClient.RoomOpCode.Modify:
                        break;
                    case (int)TCPClient.RoomOpCode.Delete:
                        break;
                    case (int)TCPClient.RoomOpCode.Start:
                        break;
                    default:
                        break;

                }
                addText(getString);

                getString = "";

                //채팅 금지시
                if (false)
                {
                    ChatBoxEnabled = false;
                }

                // 채팅 금지 해지시
                if(false)
                {
                    ChatBoxEnabled = true;
                }
            }
        }
    }
}

