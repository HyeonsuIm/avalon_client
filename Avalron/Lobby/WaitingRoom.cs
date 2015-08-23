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
        AvalonServer.RoomInfo roomInfo;

        public int MemberCnt 
        {
            get; set;
        }

        public bool Ready
        {
            set
            {
                RoomGoButton.Enabled = value;
            }
        }

        public WaitingRoom(Room room)
        {
            roomInfo = new AvalonServer.RoomInfo();
            roomInfo.createRoom(room.RoomName, Convert.ToInt32(room.RoomType), room.RoomPassword, Program.userInfo.index, Program.userInfo.nick, Convert.ToInt32(room.RoomMaxMember), -1);

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
            chatting = new WaitingRoomChatting(Controls);

            // 방장이면 시작버튼
            if (true)
            {
                RoomGoButton.Text = "시작";
                RoomGoButton.Enabled = true;        // 기본값은 false로 수정할것.
            }

            string[] infoStr = roomInfo.getRoomInfo();
            int[] indexList = roomInfo.getMemberIndexList();
            string[] nickList = roomInfo.getMemberNickList();

            for (int i = 0; i < Convert.ToInt32(infoStr[3]); i++)
            {
                waitingRoomProfile[i].SetInform(nickList[i], indexList[i], null);
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

        private void Go_Click(object sender, EventArgs e)
        {
            // 방장이 아니면 준비되었다고 신호를 보냅니다.
            if(false)
            {
                Program.tcp.DataSend((int)TCPClient.RoomOpCode.Ready, null);
                RoomGoButton.Text = "준비완료";
                return;
            }
            // 방장일시.
            Program.tcp.DataSend((int)TCPClient.RoomOpCode.Start, Program.userInfo.index.ToString());
            Program.avalron = new Avalron.Avalron(MemberCnt);
            Close();
        }

        public bool PeopleEnter(string nick, int index)
        {
            if (MemberCnt > Convert.ToInt32(RoomMaxNumber.Text))
                return false;

            string[] str = roomInfo.getRoomInfo();
            roomInfo.addUser(index, nick, str[2]);

            foreach(WaitingRoomProfile i in waitingRoomProfile)
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
                    waitingRoomProfile[cnt] = null;
                    roomInfo.removeUser(UserInfoindex);
                    break;
                }
                cnt++;
            }
            for(; null != waitingRoomProfile && cnt < (waitingRoomProfile.Length - 1) ; cnt++)
            {
                waitingRoomProfile[cnt] = waitingRoomProfile[cnt++];
            }
        } 

        public void RoomClose()
        {
            if(InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {
                    Close();
                });
            }
            else
            {
                Close();
            }
        }

        private void WaitingRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            TCPReceiveThread.Wait();
            Program.lobby = new Lobby(Program.userInfo);
            Program.lobby.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.tcp.DataSend(105, "27" + TCPClient.delimiter + "0" + TCPClient.delimiter);
        }
    }
}
