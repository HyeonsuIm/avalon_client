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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.Register_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.findID_Button = new System.Windows.Forms.Button();
            this.findPW_Button = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.Play = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Login_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Register_Button
            // 
            this.Register_Button.BackColor = System.Drawing.Color.Transparent;
            this.Register_Button.BackgroundImage = global::Avalron.Properties.Resources.회원가입2;
            this.Register_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Register_Button.FlatAppearance.BorderSize = 0;
            this.Register_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Register_Button.ForeColor = System.Drawing.Color.Transparent;
            this.Register_Button.Location = new System.Drawing.Point(87, 347);
            this.Register_Button.Name = "Register_Button";
            this.Register_Button.Size = new System.Drawing.Size(120, 40);
            this.Register_Button.TabIndex = 3;
            this.Register_Button.UseVisualStyleBackColor = false;
            this.Register_Button.Click += new System.EventHandler(this.Register_Button_Click);
            this.Register_Button.MouseLeave += new System.EventHandler(this.Register_Button_MouseLeave);
            this.Register_Button.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Register_Button_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(85, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(85, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "PW";
            // 
            // findID_Button
            // 
            this.findID_Button.BackColor = System.Drawing.Color.Transparent;
            this.findID_Button.BackgroundImage = global::Avalron.Properties.Resources.ff_id;
            this.findID_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.findID_Button.FlatAppearance.BorderSize = 0;
            this.findID_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.findID_Button.Location = new System.Drawing.Point(87, 396);
            this.findID_Button.Name = "findID_Button";
            this.findID_Button.Size = new System.Drawing.Size(120, 40);
            this.findID_Button.TabIndex = 4;
            this.findID_Button.UseVisualStyleBackColor = false;
            this.findID_Button.Click += new System.EventHandler(this.findID_Button_Click);
            this.findID_Button.MouseLeave += new System.EventHandler(this.findID_Button_MouseLeave);
            this.findID_Button.MouseMove += new System.Windows.Forms.MouseEventHandler(this.findID_Button_MouseMove);
            // 
            // findPW_Button
            // 
            this.findPW_Button.BackColor = System.Drawing.Color.Transparent;
            this.findPW_Button.BackgroundImage = global::Avalron.Properties.Resources.비밀번호찾기2;
            this.findPW_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.findPW_Button.FlatAppearance.BorderSize = 0;
            this.findPW_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.findPW_Button.Location = new System.Drawing.Point(213, 396);
            this.findPW_Button.Name = "findPW_Button";
            this.findPW_Button.Size = new System.Drawing.Size(120, 40);
            this.findPW_Button.TabIndex = 5;
            this.findPW_Button.UseVisualStyleBackColor = true;
            this.findPW_Button.Click += new System.EventHandler(this.findPW_Button_Click);
            this.findPW_Button.MouseLeave += new System.EventHandler(this.findPW_Button_MouseLeave);
            this.findPW_Button.MouseMove += new System.Windows.Forms.MouseEventHandler(this.findPW_Button_MouseMove);
            // 
            // Play
            // 
            this.Play.Location = new System.Drawing.Point(699, 549);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(75, 23);
            this.Play.TabIndex = 6;
            this.Play.Text = "재생";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(780, 549);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(75, 23);
            this.Stop.TabIndex = 7;
            this.Stop.Text = "정지";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(478, 223);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(94, 94);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // Login_Button
            // 
            this.Login_Button.BackColor = System.Drawing.Color.Transparent;
            this.Login_Button.BackgroundImage = global::Avalron.Properties.Resources.f_로그인;
            this.Login_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Login_Button.FlatAppearance.BorderSize = 0;
            this.Login_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Login_Button.ForeColor = System.Drawing.Color.Transparent;
            this.Login_Button.Location = new System.Drawing.Point(213, 346);
            this.Login_Button.Name = "Login_Button";
            this.Login_Button.Size = new System.Drawing.Size(120, 40);
            this.Login_Button.TabIndex = 10;
            this.Login_Button.UseVisualStyleBackColor = false;
            this.Login_Button.Click += new System.EventHandler(this.Login_Button_Click);
            this.Login_Button.MouseLeave += new System.EventHandler(this.Login_Button_MouseLeave);
            this.Login_Button.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Login_Button_MouseMove);
            // 
            // login
            // 
            this.AcceptButton = this.Login_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::Avalron.Properties.Resources.main_b;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.Login_Button);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.findPW_Button);
            this.Controls.Add(this.findID_Button);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Register_Button);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "로그인";
            this.Load += new System.EventHandler(this.login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Register_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button findID_Button;
        private System.Windows.Forms.Button findPW_Button;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Login_Button;
    }
}
