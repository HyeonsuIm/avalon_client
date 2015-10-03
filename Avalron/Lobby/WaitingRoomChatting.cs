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
            while((Program.state%10) == 2)
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
                        if(Program.userInfo.index != Convert.ToInt32(spriter.split[0]))
                        {
                            Program.room.PeopleLeave(Convert.ToInt32(spriter.split[0]));
                            Program.room.UserRefresh();
                            break;
                        }
                        closing = true;
                        Program.state = 21;
                        Program.room.RoomClose();
                        break;
                    case (int)TCPClient.RoomOpCode.SeatClose:
                        break;
                    case (int)TCPClient.RoomOpCode.Modify:
                        MessageBox.Show("방이 수정되었습니다.");
                        break;
                    case (int)TCPClient.RoomOpCode.Delete:
                        MessageBox.Show("방이 삭제되었습니다.");
                        break;
                    case (int)TCPClient.RoomOpCode.Start:
                        //MessageBox.Show("게임을 시작합니다.");
                        string ip = spriter.split[0];
                        Program.avalron = new Avalron.Avalron(7);
                        closing = true;
                        Program.state = 23;
                        Program.room.RoomClose();
                        break;
                    case (int)TCPClient.RoomOpCode.Ready:
                        bool readyBool = false;
                        if (0 == Convert.ToInt32(spriter.split[0]))
                            readyBool = false;
                        else if (1 == Convert.ToInt32(spriter.split[0]))
                            readyBool = true;
                        else
                            MessageBox.Show("네트워크 : RoomReady + " + readyBool.ToString());
                    
                        Program.room.roomInfo.ready(Convert.ToInt32(spriter.split[1]), readyBool);
                        break;
                    case (int)TCPClient.LobbyOpcode.USER_REFRESH: // 103코드 넘어옴 방지
                        break;
                    case 901:
                        Program.state = 0;
                        Program.room.RoomClose();
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

