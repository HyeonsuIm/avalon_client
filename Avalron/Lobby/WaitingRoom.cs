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
        Room room;
        public int MemberCnt 
        {
            get; set;
        }

        public WaitingRoom(Room room)
        {
            InitializeComponent();

            TitleBar titleBar = new TitleBar(this);

            for(int i =0; i < waitingRoomProfile.Length; i++)
            {
                waitingRoomProfile[i] = new WaitingRoomProfile(Controls, i);
            }
            chatting = new WaitingRoomChatting(Controls);

            this.room = room;
            RoomName.Text = room.RoomName;
            RoomType.Text = room.RoomType;
            RoomMaxNumber.Text = room.RoomMaxMember;

            //TCPReceiveThread = new Thread(new ThreadStart(chatting.RunGetChat));
            TCPReceiveThread = new Task(chatting.RunGetChat);
            TCPReceiveThread.Start();
        }

        private void RoomSetting_Click(object sender, EventArgs e)
        {
            RoomSetting = new LobbyRoomMake(Program.tcp);
            RoomSetting.Modify(room);
            RoomSetting.ShowDialog(this);
        }

        private void Go_Click(object sender, EventArgs e)
        {
            Program.tcp.DataSend((int)TCPClient.RoomOpCode.Start, Program.userInfo.index.ToString());
            Program.avalron = new Avalron.Avalron(MemberCnt);
            Close();
            MessageBoxEx.Show("go");
        }

        public bool PeopleEnter(string nick, int index)
        {
            if (MemberCnt > Convert.ToInt32(room.RoomMaxMember))
                return false;

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
                }
                cnt++;
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
