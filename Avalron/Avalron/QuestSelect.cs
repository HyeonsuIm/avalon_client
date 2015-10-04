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
    public partial class QuestSelect : Form
    {
        bool result;

        public QuestSelect()
        {
            InitializeComponent();
        }

        public QuestSelect(string title)
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
