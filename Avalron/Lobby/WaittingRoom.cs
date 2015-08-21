using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron
{
    public partial class WaittingRoom : Form
    {
        WaittingRoomProfile[] wattingRoomProfile = new WaittingRoomProfile[10];
        Avalron.Chatting chatting;
        LobbyRoomMake RoomSetting;

        public WaittingRoom()
        {
            InitializeComponent();

            for(int i =0; i < wattingRoomProfile.Length; i++)
            {
                wattingRoomProfile[i] = new WaittingRoomProfile(Controls, i);
            }
            chatting = new Avalron.Chatting(Controls);
        }

        private void RoomSetting_Click(object sender, EventArgs e)
        {
            RoomSetting = new LobbyRoomMake(Program.tcp);
            RoomSetting.ShowDialog(this);
        }

        private void Go_Click(object sender, EventArgs e)
        {
            MessageBoxEx.Show("go");
        }
    }
}
