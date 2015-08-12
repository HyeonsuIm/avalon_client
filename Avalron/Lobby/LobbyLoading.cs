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
        Task waiting;
        private delegate void closing();

        public LobbyLoading()
        {
            InitializeComponent();

            waiting = new Task(wait);

            waiting.Start();
        }

        void wait()
        {
            Thread.Sleep(2000);
            if (InvokeRequired)
            {
                closing closing = new closing(wait);
                Invoke(closing);
            }
            else
            {
                Close();
            }
        }
    }
}
