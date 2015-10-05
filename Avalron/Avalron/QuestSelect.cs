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

            TitleBar title = new TitleBar(this, false);
            this.card = card;
            InitializeComponent();
            if (card < (int)CharacterCard.Card.separatrix)
            {
                Notice.Text = "선의 세력은 성공만 선택할 수 있습니다.";
                reject.Visible = false;
            }
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
            Program.avalron.questSelect = false;
            result = false;
            Close();
        }
    }
}
