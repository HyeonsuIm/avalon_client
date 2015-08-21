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
        Avalron.Chatting chatting;
        LobbyRoomMake RoomSetting;
        //Thread TCPReceiveThread;
        Task TCPReceiveThread;
        Room room;

        public WaitingRoom(Room room)
        {
            InitializeComponent();

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
            Close();
            Program.tcp.Close();
            //Program.tcp.
            MessageBoxEx.Show("go");
        }
    }
}
