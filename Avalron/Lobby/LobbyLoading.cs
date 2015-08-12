using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Avalron
{
    public partial class LobbyLoading : Form
    {
        enum GlobalOpcode { Nomal_EXIT = 900, Keep_Alive }
        private delegate void closing();

        public LobbyLoading(UserInfo userInfo)
        {
            InitializeComponent();

            Program.userInfo = userInfo;
            
            Shown += new EventHandler(LobbyLoading_Shown);
            FormClosing += new FormClosingEventHandler(closed);
        }

        private void LobbyLoading_Shown(Object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Program.tcp = new TCPClient();

            Program.tcp.DataSend((int)GlobalOpcode.Keep_Alive, "");

            int opcode = Convert.ToInt16(Program.tcp.ReciveData().Substring(0, 3));
            if (opcode == (int)GlobalOpcode.Keep_Alive)
            {
                Close();
            }
            else
            {
                MessageBox.Show("접속에 실패하였습니다.");
                Application.Exit();
            }
        }

        private void closed(Object sender, EventArgs e)
        {
            Program.lobby = new Lobby(Program.userInfo);
        }
    }
}
