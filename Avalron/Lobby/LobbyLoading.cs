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
        public LobbyLoading(UserInfo userinfo)
        {
            Program.tcpAllocation();
            InitializeComponent();
            Program.lobby = new Lobby(userinfo);
            Shown += new EventHandler(LobbyLoading_Shown);
        }

        private void LobbyLoading_Shown(Object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Close();
        }
    }
}
