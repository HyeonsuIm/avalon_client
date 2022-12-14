using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron
{
    public class TitleBar 
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public readonly int WM_NLBUTTONDOWN = 0xA1;
        public readonly int HT_CAPTION = 0x2;

        private System.Windows.Forms.Button Exit = new Button();
        private System.Windows.Forms.Button Minimized = new Button();
        private System.Windows.Forms.Panel Title = new Panel();

        int ButtonWidth = 30;
        int Height = 25;

        protected Form form;

        public TitleBar(System.Windows.Forms.Form form, bool closingBtnEnable = true)
        {
            this.form = form;
            // 
            // Title
            // 
            this.Title.Controls.Add(this.Exit);
            this.Title.Controls.Add(this.Minimized);
            this.Title.Location = new System.Drawing.Point(-1, 0);
            this.Title.Name = "Title";
            this.Title.Margin = new Padding(0);
            this.Title.Size = new System.Drawing.Size(form.Size.Width+ 3, Height);
            this.Title.BackColor = System.Drawing.Color.Transparent;
            Title.BackColor = System.Drawing.ColorTranslator.FromHtml("#57534e");
            //this.Title.BackgroundImage = global::Avalron.Properties.Resources.대기방채팅;

            this.Title.BackgroundImageLayout = ImageLayout.Stretch;
            //this.Title.Margin.All = 0;
            //this.Title.TabIndex = 8;
            this.Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);
            // 
            // Exit
            // 
            this.Exit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Exit.Size = new System.Drawing.Size(ButtonWidth, Height);
            this.Exit.Location = new System.Drawing.Point(form.Size.Width - ButtonWidth, 0);
            this.Exit.Name = "Exit";
            //this.Exit.TabIndex = 11;
            this.Exit.Text = "X";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Minimized
            // 
            this.Minimized.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Minimized.Size = new System.Drawing.Size(ButtonWidth, Height);
            this.Minimized.Location = new System.Drawing.Point(Exit.Location.X - ButtonWidth, 0);
            this.Minimized.Name = "Minimized";
            //this.Minimized.TabIndex = 10;
            this.Minimized.Text = "_";
            this.Minimized.UseVisualStyleBackColor = true;
            this.Minimized.Click += new System.EventHandler(this.Minimized_Click);

            form.Controls.Add(Title);

            if ((Program.state%10) == 3) { Exit.Enabled = false; }
            if(false == closingBtnEnable)
            {
                Minimized.Visible = false;
                Minimized.Enabled = false;
                Exit.Visible = false;
                Exit.Enabled = false;
            }
        }

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 다른 컨트롤에 묶여있을 수 있을 수 있으므로 마우스캡쳐 해제
                ReleaseCapture();

                // 타이틀 바의 다운 이벤트처럼 보냄
                SendMessage(form.Handle, WM_NLBUTTONDOWN, HT_CAPTION, 0);
            }

            //base.OnMouseDown(e);
        }

        private void Minimized_Click(object sender, EventArgs e)
        {
            form.WindowState = FormWindowState.Minimized;
        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            Exit.Enabled = false;
            if((Program.tcp == null) || (Program.state == 1)) { Program.state = 0; Application.Exit(); }
            else {
                Program.tcp.DataSend((int)Lobby.GlobalOpcode.Nomal_EXIT, "");
                Lobby.Delay(500);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }
    }
}
