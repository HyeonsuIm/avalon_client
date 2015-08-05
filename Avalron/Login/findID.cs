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
    public partial class findID : Form
    {
        Label warning;
        public findID()
        {
            InitializeComponent();
        }

        private void find_Click(object sender, EventArgs e)
        {
            // 이메일주소가 서버에 있는지 검사
            if (printWarning() == false)
                return;

            TCPClient tcp = new TCPClient();
            string ID = tcp.FindID(EmailBox.Text);
            if (ID == "")
            {
                MessageBox.Show("이메일 주소에 해당하는 아이디가 없습니다.", "이메일 불일치", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("당신의 아이디는 " + ID + "입니다.");
                Close();
            }
        }

        private void EmailBox_Leave(object sender, EventArgs e)
        {
            printWarning();
        }

        private bool printWarning()
        {
            if (login.IsValidEmail(EmailBox.Text) == false)
            {
                warning = new Label();
                warning.Text = "이메일 형식이 맞지 않습니다.";
                warning.ForeColor = System.Drawing.Color.Red;
                warning.AutoSize = true;
                warning.Location = new Point(20, 130);
                EmailBox.BackColor = Color.Red;
                this.Controls.Add(warning);

                return false;
            }
            else
            {
                EmailBox.BackColor = Color.White;
                if (null != warning)
                {
                    warning.Dispose();
                }

                return true;
            }
        }
    }
}
