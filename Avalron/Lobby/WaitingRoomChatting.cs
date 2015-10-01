using System;
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

        // room에 종속적입니다.
        new public void RunGetChat()
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
                int OpCode = spriter.getJustOpCode();
                string line;

                switch(OpCode)
                {
                    case (int)TCPClient.RoomOpCode.Chat:
                        line = spriter.split[0] + " : " + spriter.split[1] ;
                        addText(line);
                        break;
                    case (int)TCPClient.RoomOpCode.Wisper:
                        if (spriter.split[0] == Program.userInfo.index.ToString())
                            line = spriter.split[1] + " 님에게 : " + spriter.split[2];
                        else
                            line = spriter.split[0] + " 님으로 부터 : " + spriter.split[1];
                        addText(line);
                        break;
                    case (int)TCPClient.RoomOpCode.Connect:
                        Program.room.PeopleEnter(Convert.ToInt32(spriter.split[0]), spriter.split[1]); 
                        break;
                    case (int)TCPClient.RoomOpCode.DisConnect:
                        Program.room.PeopleLeave(Convert.ToInt32(spriter.split[0]));
                        break;
                    case (int)TCPClient.RoomOpCode.SeatClose:
                        break;
                    case (int)TCPClient.RoomOpCode.Modify:
                        MessageBoxEx.Show(Program.room, "방이 수정되었습니다.");
                        break;
                    case (int)TCPClient.RoomOpCode.Delete:
                        MessageBoxEx.Show(Program.room, "방이 삭제되었습니다.");
                        break;
                    case (int)TCPClient.RoomOpCode.Start:
                        MessageBoxEx.Show(Program.room, "게임을 시작합니다.");
                        Program.room.RoomClose();
                        Program.avalron = new Avalron.Avalron(7);
                        break;
                    case 902:
                        break;
                    default:
                        MessageBoxEx.Show("처리되지 않은 OpCode : " + OpCode);
                        break;

                }
                getString = "";
            }
        }
    }
}

