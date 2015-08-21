namespace Avalron
{
    partial class login
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Login_Button = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.IDBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PWBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Play = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.findID_Button = new System.Windows.Forms.Button();
            this.findPW_Button = new System.Windows.Forms.Button();
            this.TitleBar = new System.Windows.Forms.Panel();
            this.Exit = new System.Windows.Forms.Button();
            this.Minimized = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.TitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Login_Button
            // 
            this.Login_Button.Location = new System.Drawing.Point(324, 367);
            this.Login_Button.Name = "Login_Button";
            this.Login_Button.Size = new System.Drawing.Size(92, 23);
            this.Login_Button.TabIndex = 2;
            this.Login_Button.Text = "로그인";
            this.Login_Button.UseVisualStyleBackColor = true;
            this.Login_Button.Click += new System.EventHandler(this.Login_Button_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(206, 367);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "회원가입";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // IDBox
            // 
            this.IDBox.Font = new System.Drawing.Font("굴림", 15F);
            this.IDBox.Location = new System.Drawing.Point(222, 159);
            this.IDBox.Name = "IDBox";
            this.IDBox.Size = new System.Drawing.Size(185, 30);
            this.IDBox.TabIndex = 0;
            this.IDBox.TextChanged += new System.EventHandler(this.IDBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(220, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "ID";
            // 
            // PWBox
            // 
            this.PWBox.Font = new System.Drawing.Font("굴림", 15F);
            this.PWBox.Location = new System.Drawing.Point(222, 261);
            this.PWBox.Name = "PWBox";
            this.PWBox.PasswordChar = '●';
            this.PWBox.Size = new System.Drawing.Size(185, 30);
            this.PWBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "PW";
            // 
            // Play
            // 
            this.Play.Location = new System.Drawing.Point(574, 515);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(75, 23);
            this.Play.TabIndex = 6;
            this.Play.Text = "재생";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(655, 515);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(75, 23);
            this.Stop.TabIndex = 7;
            this.Stop.Text = "정지";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // findID_Button
            // 
            this.findID_Button.Location = new System.Drawing.Point(206, 415);
            this.findID_Button.Name = "findID_Button";
            this.findID_Button.Size = new System.Drawing.Size(92, 23);
            this.findID_Button.TabIndex = 4;
            this.findID_Button.Text = "아이디 찾기";
            this.findID_Button.UseVisualStyleBackColor = true;
            this.findID_Button.Click += new System.EventHandler(this.findID_Button_Click);
            // 
            // findPW_Button
            // 
            this.findPW_Button.Location = new System.Drawing.Point(324, 415);
            this.findPW_Button.Name = "findPW_Button";
            this.findPW_Button.Size = new System.Drawing.Size(92, 23);
            this.findPW_Button.TabIndex = 5;
            this.findPW_Button.Text = "비밀번호 찾기";
            this.findPW_Button.UseVisualStyleBackColor = true;
            this.findPW_Button.Click += new System.EventHandler(this.findPW_Button_Click);
            // 
            // TitleBar
            // 
            this.TitleBar.Controls.Add(this.Exit);
            this.TitleBar.Controls.Add(this.Minimized);
            this.TitleBar.Location = new System.Drawing.Point(0, 0);
            this.TitleBar.Name = "TitleBar";
            this.TitleBar.Size = new System.Drawing.Size(786, 25);
            this.TitleBar.TabIndex = 8;
            this.TitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);
            // 
            // Exit
            // 
            this.Exit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Exit.Location = new System.Drawing.Point(751, 0);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(32, 26);
            this.Exit.TabIndex = 11;
            this.Exit.Text = "X";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Minimized
            // 
            this.Minimized.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Minimized.Location = new System.Drawing.Point(720, 0);
            this.Minimized.Name = "Minimized";
            this.Minimized.Size = new System.Drawing.Size(32, 26);
            this.Minimized.TabIndex = 10;
            this.Minimized.Text = "_";
            this.Minimized.UseVisualStyleBackColor = true;
            this.Minimized.Click += new System.EventHandler(this.Minimized_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(478, 223);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(94, 94);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // login
            // 
            this.AcceptButton = this.Login_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::Avalron.Properties.Resources.login_bg1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TitleBar);
            this.Controls.Add(this.findPW_Button);
            this.Controls.Add(this.findID_Button);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PWBox);
            this.Controls.Add(this.IDBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Login_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "login";
            this.Text = "로그인";
            this.Load += new System.EventHandler(this.login_Load);
            this.TitleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Login_Button;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox IDBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PWBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button findID_Button;
        private System.Windows.Forms.Button findPW_Button;
        private System.Windows.Forms.Panel TitleBar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button Minimized;
        private System.Windows.Forms.Timer timer;
    }
}
