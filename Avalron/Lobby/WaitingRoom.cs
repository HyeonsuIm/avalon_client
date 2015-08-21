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
        WaitingRoomProfile[] waitingRoomProfile = new WaitingRoomProfile[10];
        Avalron.Chatting chatting;
        LobbyRoomMake RoomSetting;
        Thread TCPReceiveThread;

        public WaitingRoom()
        {
            InitializeComponent();

            for(int i =0; i < waitingRoomProfile.Length; i++)
            {
                waitingRoomProfile[i] = new WaitingRoomProfile(Controls, i);
            }
            chatting = new Avalron.Chatting(Controls);

            TCPReceiveThread = new Thread(new ThreadStart(chatting.RunGetChat));
            TCPReceiveThread.Start();
        }

        private void RoomSetting_Click(object sender, EventArgs e)
        {
            RoomSetting = new LobbyRoomMake(Program.tcp);
            RoomSetting.ShowDialog(this);
        }

        private void Go_Click(object sender, EventArgs e)
        {
            Application.Exit();
            //Program.tcp.
            MessageBoxEx.Show("go");
        }
    }
}
