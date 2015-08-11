using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;       // 텍스트 정규식을 위한 헤더 파일
using System.Security.Policy;               // 이벤트 핸들러 등록을 위한 헤더 파일

namespace Avalron
{
    public partial class Register : Form
    {
        Label warning;
        int[] IsChecked = new int[5];        // 각 항목의 검사결과가 참인지에 대한 값.
                                             // 확인안함 0, 검사했지만 거짓 1, 특수문자검사만 2, 최종검사(중복) 3

        TCPClient tcp = new TCPClient();
        public Register()
        {
            InitializeComponent();
            for (int i =0; i < IsChecked.Length; i++)
            {
                IsChecked[i] = 0;
            }
        }

        private void DoRegister_Click(object sender, EventArgs e)
        {
            //tcp = new TCPClient();

            if (0 == IDBox.TextLength || 0 == PWBox.TextLength || 0 == PWConformBox.TextLength ||
                0 == EmailBox.TextLength || 0 == NickNameBox.TextLength)
            {
                MessageBoxEx.Show(this, "필수항목이 비어있습니다.", "회원가입 에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (PWBox.Text != PWConformBox.Text)
            {
                MessageBoxEx.Show(this,"비밀번호와 비밀번호 확인이 맞지 않습니다.", "회원가입 에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 이메일은 서버의 데이터에서 중복될 수 없습니다.
            if (tcp.EMailCheck(EmailBox.Text))
            {
                MessageBoxEx.Show(this,"이미 등록된 이메일 주소입니다.", "이메일 중복", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for(int i =0; i < 5; i++)
            {
                if (2 != IsChecked[i])
                {
                    switch(i)
                    {
                        case 0:
                            printWarning("아이디 중복검사를 해주세요.", IDBox);
                            break;
                        case 1:
                        case 2:
                            printWarning("", PWBox);
                            printWarning("비밀번호를 확인해주세요", PWConformBox);
                            break;
                        case 3:
                            printWarning("닉네임 중복을 해주세요", NickNameBox);
                            break;
                        case 4:
                            printWarning("이메일 검사를 해주세요", EmailBox);
                            break;
                    }
                    
                    return;
                }
            }

            tcp.Register(IDBox.Text, PWBox.Text, login.Encryption(NickNameBox.Text), EmailBox.Text);

            Close();
        }

        private void IDCheck_Click(object sender, EventArgs e)
        {
            if (IDBox.Text == "")
            {
                printWarning("아이디가 비어있습니다ㅣ.", IDBox);
                return;
            }

            if (login.IsValidStr(IDBox.Text) == false)
            {
                MessageBoxEx.Show(this,"아이디에는 특수문자가 포함될 수 없습니다.", "아이디 중복", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 입력된 아이디가 서버와 중복됬는지 검사.
            bool check = tcp.IDCheck(IDBox.Text);
            if(check)
            {
                printWarning("아이디가 중복됩니다.", IDBox);
            }
            else
            {
                IsChecked[0] = 2;   // 검사 완료를 뜻함.
                IDBox.BackColor = Color.White;

                freeWarning();
            }
        }

        private void NickNameCheck_Click(object sender, EventArgs e)
        {
            if(NickNameBox.Text == "")
            {
                printWarning("닉네임을 입력해 주세요", NickNameBox);
                return;
            }
            if(login.IsValidStr(NickNameBox.Text) == false)
            {
                MessageBoxEx.Show(this,"닉네임에는 특수문자가 포함될 수 없습니다.", "회원가입 에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 입력된 닉네임이 서버와 중복되있는지 검사
            bool check = tcp.NickCheck(NickNameBox.Text);
            if(check)
            {
                printWarning("닉네임이 중복됩니다.", NickNameBox);
            }
            else
            {
                IsChecked[3] = 2;   // 검사 완료를 뜻함.
                NickNameBox.BackColor = Color.White;

                freeWarning();
            }
        }

       

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void IDBox_Leave(object sender, EventArgs e)
        {
            if (0 == IDBox.TextLength)
                printWarning("ID가 비어있습니다.", IDBox);
            else if (login.IsValidStr(IDBox.Text) == false)
                printWarning("특수문자가 포함될 수 없습니다.", IDBox);
            else if (2 != IsChecked[0])
                printWarning("ID중복검사를 하십시오.", IDBox);
            else if (2 == IsChecked[0])
            {
                IDBox.BackColor = Color.White;
                freeWarning();
                return;
            }
            IsChecked[0] = 1;
        }

        private void PWBox_Leave(object sender, EventArgs e)
        {
            IsChecked[1] = 1;
            if (0 == PWBox.TextLength)
                printWarning("PW가 비어있습니다.", PWBox);
            else if(IsChecked[2] == 1 && PWConformBox.Text == PWBox.Text)
            {
                PWBox.BackColor = Color.White;
                PWConformBox.BackColor = Color.White;
                IsChecked[1] = 2; IsChecked[2] = 2;
                freeWarning();
            }
        }

        private void PWConformBox_Leave(object sender, EventArgs e)
        {
            IsChecked[2] = 1;
            if (0 == PWConformBox.TextLength)
                printWarning("PW확인란이 비어있습니다.", PWConformBox);
            else if (PWConformBox.Text != PWBox.Text)
            {
                printWarning("PW와 PW확인이 같지 않습니다.", PWBox);
                printWarning("PW와 PW확인이 같지 않습니다.", PWConformBox);
            }
            else
            {
                PWBox.BackColor = Color.White;
                PWConformBox.BackColor = Color.White;
                IsChecked[1] = 2;   IsChecked[2] = 2;
                freeWarning();
            }
        }

        private void NickNameBox_Leave(object sender, EventArgs e)
        {
            if (0 == NickNameBox.TextLength)
                printWarning("닉네임란이 비어있습니다.", NickNameBox);
            else if (login.IsValidStr(NickNameBox.Text) == false)
                printWarning("닉네임란에는 특수문자가 올 수 없습니다.", NickNameBox);
            else if (2 != IsChecked[3])
                printWarning("닉네임 중복검사를 하십시오.", NickNameBox);
            else if(2 == IsChecked[3])
            {
                IsChecked[3] = 2;
                NickNameBox.BackColor = Color.White;
                freeWarning();
            }
        }

        private void EmailBox_Leave(object sender, EventArgs e)
        {
            if (0 == EmailBox.TextLength)
                printWarning("이메일란이 비어있습니다.", EmailBox);
            else
            {
                if (login.IsValidEmail(EmailBox.Text) == false)
                    printWarning("이메일 형식을 올바르게 입력하십시오.", EmailBox);
                else
                {
                    IsChecked[4] = 2;
                    EmailBox.BackColor = Color.White;
                    freeWarning();
                }
                //printWarning(IsValidEmail(EmailBox.Text).ToString(), EmailBox);
            }
        }
        private void printWarning(string str, TextBox pBox)
        {
            freeWarning();
            warning = new Label();
            warning.Text = str;
            warning.ForeColor = System.Drawing.Color.Red;
            warning.AutoSize = true;
            warning.Location = new Point(30, 190);
            pBox.BackColor = System.Drawing.Color.Red;
            this.Controls.Add(warning);
        }

        //static public void printWarning(string str, TextBox pBox, Label warning, Point point, ControlCollection Controls)
        //{
        //    if (warning != null)
        //    {
        //        warning.Dispose();
        //    }
        //    warning = new Label();
        //    warning.Text = str;
        //    warning.ForeColor = System.Drawing.Color.Red;
        //    warning.AutoSize = true;
        //    warning.Location = point;
        //    pBox.BackColor = System.Drawing.Color.Red;
        //    Controls.Add(warning);
        //}

        private void freeWarning()
        {
            if (warning != null)
                warning.Dispose();
        }

        

        private void IDBox_TextChanged(object sender, EventArgs e)
        {
            if(2 == IsChecked[0])
                IsChecked[0] = 1;       // 중복검사 안함으로 변경
        }

        private void NickNameBox_TextChanged(object sender, EventArgs e)
        {
            if (2 == IsChecked[3])
                IsChecked[3] = 1;       // 중복검사 안함으로 변경
        }

        private void EmailConfirm_Click(object sender, EventArgs e)
        {
            if (2 != IsChecked[4])
            {
                EmailConfirm.Text = "인증됨";
                return;
            }

            using (Confirm confirm = new Confirm(EmailBox.Text))
            {
                confirm.ShowDialog();

                if (true == confirm.IsAuthorized())
                    IsChecked[4] = 2;
            }
        }
    }
}

