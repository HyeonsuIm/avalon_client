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
    public partial class LobbyRoomPassword : Form
    {
        public string pass
        {
            get; set;
        }
        string roomNum;

        public LobbyRoomPassword(string RoomNumber)
        {
            InitializeComponent();
            roomNum = RoomNumber;
        }

        private void LobbyRoomPassword_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LobbyRoomPassword_Comein_Click(object sender, EventArgs e)
        {
            if (LobbyRoomPassword_Passbox.Text == "") { LobbyRoomPassword_Passbox.Focus(); return; }
            pass = LobbyRoomPassword_Passbox.Text;

            Program.tcp.DataSend((int)Lobby.LobbyOpcode.ROOM_JOIN, Program.userInfo.index + TCPClient.delimiter + roomNum + TCPClient.delimiter + pass);
            Close();
        }

        private void LobbyRoomPassword_Passbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // 입장 버튼누르기
                LobbyRoomPassword_Comein_Click(sender, e);

                // 엔터키 소리제거
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
