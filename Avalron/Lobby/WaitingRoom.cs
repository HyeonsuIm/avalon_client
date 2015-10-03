using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron
{
    public partial class WaitingRoom : Form
    {
        public enum OpCode { RoomChat = 201, RoomWisper, RoomConnect = 210, RoomDisConnect, RoomSeatClose, RoomModify, RoomDelete, RoomStart}
        WaitingRoomProfile[] waitingRoomProfile = new WaitingRoomProfile[10];
        WaitingRoomChatting chatting;
        LobbyRoomMake RoomSetting;
        //Thread TCPReceiveThread;
        Task TCPReceiveThread;
        public AvalonServer.RoomInfo roomInfo;
        delegate void RoomClosing(); // 룸 종료 크로스스레드
        delegate void UserRefreshCallback(); // 유저 종료 크로스스레드

        // 현재 방인원 0부터 시작
        public int MemberCnt
        {
            get; set;
        }

        // 준비 버튼의 눌렀는지의 상태값
        public bool Ready
        {
            set
            {
                RoomGoButton.Checked = value;
            }

            get
            {
                return RoomGoButton.Checked;
            }
        }

        public WaitingRoom(Room room)
        {
            roomInfo = new AvalonServer.RoomInfo();
            
            //이창한 봐라 바꼈다
            AvalonServer.TcpUserInfo peopleInfo = new AvalonServer.TcpUserInfo();
            peopleInfo.userNick = Program.userInfo.nick;
            peopleInfo.userIndex = Program.userInfo.index;

            //이창한 봐라
            //roomInfo.createRoom(room.RoomName, Convert.ToInt32(room.RoomType), room.RoomPassword, Program.userInfo.index, Program.userInfo.nick, Convert.ToInt32(room.RoomMaxMember), -1);
            roomInfo.createRoom(room.RoomName, Convert.ToInt32(room.RoomType), room.RoomPassword, Convert.ToInt32(room.RoomMaxMember), Convert.ToInt32(room.RoomNumber), peopleInfo);
             init();
        }

        public WaitingRoom(AvalonServer.RoomInfo roomInfo)
        {
            this.roomInfo = roomInfo;
            //string[] infoStr = roomInfo.getRoomInfo();

            init();
        }

        private void init()
        {
            InitializeComponent();

            TitleBar titleBar = new TitleBar(this);

            for (int i = 0; i < waitingRoomProfile.Length; i++)
            {
                waitingRoomProfile[i] = new WaitingRoomProfile(Controls, i);
            }

            waitingRoomProfile[0].SetHost();
            chatting = new WaitingRoomChatting(Controls);

            // 방장이면 시작버튼
            SetHost();

            string[] infoStr = roomInfo.getRoomInfo();

            //이창한 봐라
            //int[] indexList = roomInfo.getMemberIndexList();
            //string[] nickList = roomInfo.getMemberNickList();
            AvalonServer.TcpUserInfo[] UserList = roomInfo.memberInfo;

            for (int i = 0; i < Convert.ToInt32(infoStr[3]); i++)
            {
                waitingRoomProfile[i].SetInform(UserList[i].userNick , UserList[i].userIndex, null);
            }
            
            RoomName.Text = infoStr[0];
            RoomType.Text = infoStr[1];
            RoomMaxNumber.Text = infoStr[4];

            //TCPReceiveThread = new Thread(new ThreadStart(chatting.RunGetChat));
            TCPReceiveThread = new Task(chatting.RunGetChat);
            TCPReceiveThread.Start();
        }

        private void RoomSetting_Click(object sender, EventArgs e)
        {
            RoomSetting = new LobbyRoomMake(Program.tcp);
            RoomSetting.Modify(roomInfo);
            RoomSetting.ShowDialog(this);
        }

        // 유저 나가기 크로스 스레드
        public void UserRefresh()
        {
            if (InvokeRequired)
            {
                UserRefreshCallback userRefreshCallback = new UserRefreshCallback(UserRefresh);
                Invoke(userRefreshCallback);
            }
            else
            {
                int i;
                AvalonServer.TcpUserInfo[] UserList = roomInfo.memberInfo;
                string[] infoStr = roomInfo.getRoomInfo();

                for (i = 0; i < Convert.ToInt32(infoStr[3]); i++)
                {
                    waitingRoomProfile[i].SetInform(UserList[i].userNick, UserList[i].userIndex, null);
                }
                waitingRoomProfile[i].SeatOpen();
                // do
            }
        }

        private void Go_Click(object sender, EventArgs e)
        {
            if(SetHost())
            {
                // 방장일시.
                Program.tcp.DataSend((int)TCPClient.RoomOpCode.Ready, "1");
                if(false)
                //if(false == checkMemberCnt())
                {
                    MessageBoxEx.Show(this, "최소 인원에 도달하지 못했습니다.");
                    return;
                }
                if(false == checkReay())
                {
                    MessageBoxEx.Show(this, "준비가 완료 되지 않았습니다.");
                    Ready = false;
                    RoomGoButton.Enabled = true;
                    return;
                }
                Program.tcp.DataSend((int)TCPClient.RoomOpCode.Start, Program.userInfo.index.ToString() + TCPClient.delimiter + roomInfo.getNumber().ToString());
                //Program.state = 23;
                Program.lobby = null;
                return;
            }

            // 방장이 아니면 준비되었다고 신호를 보냅니다.
            string ReadyStr = "-1";
            if (Ready)
            {
                RoomGoButton.Text = "준비완료";
                ReadyStr = "1";
            }
            else
            {
                RoomGoButton.Text = "준비";
                ReadyStr = "0";
            }
            
            Program.tcp.DataSend((int)TCPClient.RoomOpCode.Ready, ReadyStr); 
        }

        public bool PeopleEnter(int index, string nick)
        {
            if (MemberCnt > Convert.ToInt32(RoomMaxNumber.Text))
                return false;

            string[] str = roomInfo.getRoomInfo();

            AvalonServer.TcpUserInfo peopleInfo = new AvalonServer.TcpUserInfo();
            peopleInfo.userIndex = index;
            peopleInfo.userNick = nick;

            //이창한 봐라
            //roomInfo.addUser(index, nick, str[2]); 에서 변경
            roomInfo.addUser(peopleInfo, str[2]);

            foreach (WaitingRoomProfile i in waitingRoomProfile)
            {
                if(-1 == i.index) 
                {
                    i.SetInform(nick, index, null);
                    break;
                }
            }

            return true;
        }

        public void PeopleLeave(int UserInfoindex)
        {
            int cnt = 0;
            foreach(WaitingRoomProfile index in waitingRoomProfile)
            {
                if (index.index == UserInfoindex)
                {
                    waitingRoomProfile[cnt].UserLeave();
                    roomInfo.removeUser(UserInfoindex);
                    break;
                }
                cnt++;
            }
            for(; cnt < (waitingRoomProfile.Length - 1) ; cnt++)
            {
                waitingRoomProfile[cnt] = waitingRoomProfile[cnt++];
            }

            //SetHost();
        } 

        // Room을 종료시키는 크로스스레드 함수
        public void RoomClose()
        {
            if(InvokeRequired)
            {
                RoomClosing RoomClosing = new RoomClosing(RoomClose);
                Invoke(RoomClosing);

                //Invoke((MethodInvoker)delegate ()
                //{
                //    Close();
                //});
            }
            else
            {
                if (Program.state != 0) { Program.lobby = new Lobby(Program.userInfo); }
                Program.room.Dispose();
                Program.room.Close();
            }
        }

        private void WaitingRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TCPReceiveThread.Wait();
            //Program.lobby = new Lobby(Program.userInfo);
            //Program.lobby.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.tcp.DataSend(105, "27" + TCPClient.delimiter + "0" + TCPClient.delimiter);
        }

        // 방장이면 되야하는 기능들. 및 방장시 true 반환
        private bool SetHost()
        {
            if (Program.userInfo.index == waitingRoomProfile[0].index)
            {
                RoomGoButton.Text = "시작";
                RoomGoButton.Enabled = false;        // 기본값은 false로 수정할것.

                return true;
            }

            return false;
        }

        // 나가기 버튼
        private void RoomOut_Click(object sender, EventArgs e)
        {
            Program.tcp.DataSend((int)TCPClient.RoomOpCode.DisConnect, Program.userInfo.index.ToString());
            RoomOut.Enabled = false;
        }

        // 모든 사람이 레디 되었는지 확인합니다.
        public bool checkReay()
        {
            for(int i =0; i < MemberCnt; i++)
            {
                if (false == roomInfo.readyState[i])
                    return false;
            }
            return true;
        }

        // 최소 인원수가 도달했는지 검사합니다.
        public bool checkMemberCnt()
        {
            if (roomInfo.getMemberCount() < roomInfo.minPerson)
                return false;

            return true;
        }
    }
}
