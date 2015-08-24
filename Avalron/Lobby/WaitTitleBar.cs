using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron
{
    class WaitTitleBar : TitleBar
    {
        public WaitTitleBar(Form form) : base(form)
        {
            parent = false;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
        }

        new protected void Exit_Click(object sender, EventArgs e)
        {
            Program.tcp.DataSend((int)TCPClient.RoomOpCode.DisConnect, Program.userInfo.index.ToString());
            Avalron.Chatting.closing = true;
            form.Close();
        }
    }
}
