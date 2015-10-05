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
        int card;
        ToolTip tooltip = new ToolTip();

        public QuestSelect(int card)
        {
            this.card = card;
            InitializeComponent();
        }

        public bool getResult()
        {
            return result;
        }

        private void approve_Click(object sender, EventArgs e)
        {
            Program.avalron.questSelect = true;
            result = true;
            Close();
        }

        private void reject_Click(object sender, EventArgs e)
        {
            if (1 == card / (int)CharacterCard.Card.separatrix)
            {
                reject.Enabled = false;
                tooltip.SetToolTip(reject, "선의 세력은 찬성만 선택가능합니다." + '\n');
                return;
            }
            Program.avalron.questSelect = false;
            result = false;
            Close();
        }
    }
}
