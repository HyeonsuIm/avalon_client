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
        public bool result;

        public Vote()
        {
            InitializeComponent();
        }

        private void approve_Click(object sender, EventArgs e)
        {
            result = true;
            Close();
        }

        private void reject_Click(object sender, EventArgs e)
        {
            result = false;
            Close();
        }
    }
}
