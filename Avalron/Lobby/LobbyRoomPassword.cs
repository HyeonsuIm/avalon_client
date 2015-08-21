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
        string pass;

        public LobbyRoomPassword(string password)
        {
            pass = password;
            InitializeComponent();

        }

        private void LobbyRoomPassword_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LobbyRoomPassword_Comein_Click(object sender, EventArgs e)
        {
            if (LobbyRoomPassword_Passbox.Text == "") { LobbyRoomPassword_Passbox.Focus(); return; }
            string key = LobbyRoomPassword_Passbox.Text;
            if (key.Equals(pass))
            {
                MessageBox.Show("입장합니다.");
                Close();
            }
            else
            {
                MessageBox.Show("비밀번호가 틀렸습니다.");
                LobbyRoomPassword_Passbox.Text = "";
            }
            LobbyRoomPassword_Passbox.Focus();
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
