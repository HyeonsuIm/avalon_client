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

        public login()
        {
            InitializeComponent();

            //music.Play();
            pictureBox1.Visible = false;
            try
            {
                login_bg = Image.FromFile(Application.StartupPath + @"\Login\img\login_bg.jpg", true);
                this.BackgroundImage = login_bg;
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("배경이미지를 불러오는데 에러가 발생했습니다.");
            }

            TitleBar titlebar = new TitleBar(this);
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
   }
}