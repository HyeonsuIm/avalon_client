using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron.Avalron
{
    public partial class Vote : Form
    {
        bool result;

        public Vote()
        {
            InitializeComponent();

            TitleBar title = new TitleBar(this, false);
        }

        public Vote(string title)
        {
            this.Text = title;
            InitializeComponent();
        }

        public bool getResult()
        {
            return result;
        }

        private void approve_Click(object sender, EventArgs e)
        {
            Program.avalron.vote = true;
            result = true;
            Close();
        }

        private void reject_Click(object sender, EventArgs e)
        {
            Program.avalron.vote = false;
            result = false;
            Close();
        }
    }
}
