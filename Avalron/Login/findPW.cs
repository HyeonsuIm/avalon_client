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
    public partial class findPW : Form
    {
        Label warning;
        int[] IsChecked = new int[2];       // 0 : 아이디, 1 : 이메일 의 검사가 정상적으로 되었는지 나타내는 변수.
        public findPW()
        {
            InitializeComponent();
        }

        private void find_Click(object sender, EventArgs e)
        {
            // 비밀번호 찾기
            // 입력된 정보들에 대해 맞는지 검사
            for (int i = 0; i < IsChecked.Length; i++)
            {
                if (0 == IsChecked[i]) return;
            }

            Login.LoginClient tcp = new Login.LoginClient();
            if(tcp.FindPW(IDBox.Text, EmailBox.Text))
            {
                MessageBoxEx.Show(this, "이메일로 임시 비밀번호가 전송되었습니다.");
                Close();
            }
            else
            {
                MessageBoxEx.Show(this,"입력하신 정보가 맞지 않습니다.");
            }
            tcp.Close();
        }

        private void IDBox_TextChanged(object sender, EventArgs e)
        {
            if(login.IsValidStr(IDBox.Text) == false)
            {
                printWarning("아이디에 특수문자가 올수 없습니다.", IDBox);
                return;
            }

            if (warning != null)
                warning.Dispose();
            IDBox.BackColor = Color.White;
            IsChecked[0] = 1;
        }

        private void EmailBox_Leave(object sender, EventArgs e)
        {
            if(false == login.IsValidEmail(EmailBox.Text))
            {
                printWarning("이메일 형식이 맞지 않습니다.", EmailBox);
                return;
            }

            if (null != warning)
                warning.Dispose();
            EmailBox.BackColor = Color.White;
            IsChecked[1] = 1;
        }

        private void printWarning(string text, TextBox pBox)
        {
            if (null != warning)
                warning.Dispose();

            warning = new Label();
            warning.Text = text;
            warning.ForeColor = System.Drawing.Color.Red;
            warning.AutoSize = true;
            warning.Location = new Point(30, 140);
            pBox.BackColor = System.Drawing.Color.Red;
            this.Controls.Add(warning);
        }
    }
}
