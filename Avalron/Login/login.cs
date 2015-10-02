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
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Net;

namespace Avalron
{
    public partial class login : Form
    {
        public Loading FrmLoading;
        private Music music = new Music();
        private Image login_bg;
        private Label Warning;
        bool tcpstop = false;

        ZBobb.AlphaBlendTextBox IDBox = new ZBobb.AlphaBlendTextBox();
        ZBobb.AlphaBlendTextBox PWBox = new ZBobb.AlphaBlendTextBox();
        ZBobb.AlphaBlendTextBox Tee = new ZBobb.AlphaBlendTextBox();

        public login()
        {
            InitializeComponent();

            //music.Play();
            pictureBox1.Visible = false;

            TitleBar titlebar = new TitleBar(this);


            Tee.Location = new Point(0, 0);
            Tee.Size = new Size(100, 100);
            // 
            // IDBox
            // 
            this.IDBox.Font = new System.Drawing.Font("굴림", 15F);
            this.IDBox.Location = new System.Drawing.Point(87, 179);
            this.IDBox.Name = "IDBox";
            this.IDBox.Size = new System.Drawing.Size(246, 30);
            this.IDBox.TabIndex = 0;
            this.IDBox.TextChanged += new System.EventHandler(this.IDBox_TextChanged);
            IDBox.Font = new System.Drawing.Font("HYPMokGak", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            IDBox.BorderStyle = BorderStyle.None;
            //IDBox.BackAlpha = 0; // Totally transparent
            // 
            // PWBox
            // 
            PWBox.Font = new System.Drawing.Font("굴림", 15F);
            PWBox.Location = new System.Drawing.Point(87, 281);
            PWBox.Name = "PWBox";
            PWBox.PasswordChar = '●';
            PWBox.Size = new System.Drawing.Size(246, 30);
            PWBox.TabIndex = 1;
            PWBox.BorderStyle = BorderStyle.None;
            //PWBox.Font = new System.Drawing.Font("a상처B", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));

            this.Controls.Add(IDBox);
            this.Controls.Add(PWBox);
    }


        private void login_Load(object sender, EventArgs e)
        {

        }

        // 특수 문자 포함시 false 반환
        static public bool IsValidStr(string strText)
        {
            string Pattern = @"^[a-zA-Z0-9가-힣]*$";
            return Regex.IsMatch(strText, Pattern);
        }
        // 맞지 않으면 false, 맞으면 true
        static public bool IsValidEmail(string email)
        {
            // 이메일 정규화 형식.
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        // 구분자 있는지 검사하기 귀찮네.
        static public int Isdelimiter(string line)
        {
            return line.IndexOf(TCPClient.delimiter);
        }

        static public string Encryption(string Data)
        {
            SHA512 sha = new SHA512Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(Data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        private string GetIP()
        {
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.Default;
            string html = wc.DownloadString("http://ipip.kr");

            Regex regex = new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
            Match m = regex.Match(html);

            return m.ToString();
        }


        private void Register_Button_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Owner = this;      // 부모 폼을 설정합니다.
            //register.Activate();
            //register.Leave += new EventHandler(textBoxLost)
            register.ShowDialog();
            register.Dispose();
        }

        private void Play_Click(object sender, EventArgs e)
        {
            music.Play();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            music.Stop();
            tcpstop = true;
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            if (null == Program.tcp)
            {
                pictureBox1.Visible = true;
                //timer.Start();
                //FrmLoading = new Loading();
                //FrmLoading.Show();

                pictureBox1.Visible = true;
                Thread t1 = new Thread(new ThreadStart(tcp_Start));
                t1.Start();
                t1.Join();
                //Program.tcp = new TCPClient();
            }

            int num = Program.tcp.Login(IDBox.Text, Encryption(PWBox.Text));

            //pictureBox1.Visible = false;
            // 로그인 실패시
            if (num == 0)
            {
                MessageBox.Show("로그인에 실패하였습니다.", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // 다음 창
                MessageBox.Show("환영합니다.");
                //Program.avalron = new Avalron.Avalron(6);
                Program.lobbyLoading = new LobbyLoading(new UserInfo(null, num));
                Close();
            }

            //FrmLoading.Dispose();
            pictureBox1.Visible = false;
            PWBox.Clear();
        }

        private void findID_Button_Click(object sender, EventArgs e)
        {
            findID idForm = new findID();
            idForm.Owner = this;
            idForm.ShowDialog();
        }

        private void findPW_Button_Click(object sender, EventArgs e)
        {
            findPW pwForm = new findPW();
            pwForm.Owner = this;
            pwForm.ShowDialog();
        }

        private delegate void CrossThreadSafetySetPictureBox(bool val);
        private void a(bool val)
        {
            pictureBox1.Visible = val;
        }
        private void loadingbar()
        {
            timer.Start();
            //bWorker1.RunWorkerAsync();
            FrmLoading = new Loading();
            FrmLoading.ShowDialog();
        }

        private void tcp_Start()
        {
            //Image loading_Img = Image.FromFile(Application.StartupPath + @"\img\loading.gif", true);
            //Graphics g = CreateGraphics();
            //g.DrawImage(loading_Img, Width / 2, Height / 2);
            //if (pictureBox1.InvokeRequired)
            //    pictureBox1.Invoke(new CrossThreadSafetySetPictureBox(a), new object[] { false });
            //else
            //    pictureBox1.Visible = false;

            Program.tcp = new TCPClient();
        }

        private void IDBox_TextChanged(object sender, EventArgs e)
        {
            if (IsValidStr(IDBox.Text) == false)
            {
                IDBox.BackColor = Color.Red;

                if (null != Warning)
                {
                    Warning.Dispose();
                }
                Warning = new Label();
                Warning.Text = "영숫자만 올 수 있습니다.";
                Warning.AutoSize = true;
                Warning.Location = new Point(Width / 2, Height / 2);
                Controls.Add(Warning);
            }
            else
            {
                IDBox.BackColor = Color.White;

                if (null != Warning)
                    Warning.Dispose();
            }
        }

        private void Login_Button_MouseMove(object sender, MouseEventArgs e)
        {
            Login_Button.BackgroundImage = Properties.Resources.로그인;
        }

        private void Login_Button_MouseLeave(object sender, EventArgs e)
        {
            Login_Button.BackgroundImage = Properties.Resources.f_로그인;
        }

        private void Register_Button_MouseMove(object sender, MouseEventArgs e)
        {
            this.Register_Button.BackgroundImage = global::Avalron.Properties.Resources.가입;
        }

        private void Register_Button_MouseLeave(object sender, EventArgs e)
        {
            this.Register_Button.BackgroundImage = global::Avalron.Properties.Resources.회원가입2;
        }

        private void findID_Button_MouseMove(object sender, MouseEventArgs e)
        {
            this.findID_Button.BackgroundImage = global::Avalron.Properties.Resources.f_id;
        }

        private void findID_Button_MouseLeave(object sender, EventArgs e)
        {
            this.findID_Button.BackgroundImage = global::Avalron.Properties.Resources.ff_id;
        }

        private void findPW_Button_MouseMove(object sender, MouseEventArgs e)
        {
            this.findPW_Button.BackgroundImage = global::Avalron.Properties.Resources.비밀번호찾기;
        }

        private void findPW_Button_MouseLeave(object sender, EventArgs e)
        {
            this.findPW_Button.BackgroundImage = global::Avalron.Properties.Resources.비밀번호찾기2;
        }
    }
}