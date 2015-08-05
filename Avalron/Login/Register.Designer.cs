namespace Avalron
{
    partial class Register
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PWBox = new System.Windows.Forms.TextBox();
            this.IDBox = new System.Windows.Forms.TextBox();
            this.DoRegister = new System.Windows.Forms.Button();
            this.PWConformBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.IDCheck = new System.Windows.Forms.Button();
            this.NickNameBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.NickNameCheck = new System.Windows.Forms.Button();
            this.EmailBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.EmailConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "비밀번호";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "아이디";
            // 
            // PWBox
            // 
            this.PWBox.Location = new System.Drawing.Point(105, 65);
            this.PWBox.Name = "PWBox";
            this.PWBox.PasswordChar = '●';
            this.PWBox.Size = new System.Drawing.Size(100, 21);
            this.PWBox.TabIndex = 2;
            this.PWBox.Leave += new System.EventHandler(this.PWBox_Leave);
            // 
            // IDBox
            // 
            this.IDBox.Location = new System.Drawing.Point(105, 38);
            this.IDBox.Name = "IDBox";
            this.IDBox.Size = new System.Drawing.Size(100, 21);
            this.IDBox.TabIndex = 0;
            this.IDBox.TextChanged += new System.EventHandler(this.IDBox_TextChanged);
            this.IDBox.Leave += new System.EventHandler(this.IDBox_Leave);
            // 
            // DoRegister
            // 
            this.DoRegister.Location = new System.Drawing.Point(184, 205);
            this.DoRegister.Name = "DoRegister";
            this.DoRegister.Size = new System.Drawing.Size(75, 23);
            this.DoRegister.TabIndex = 7;
            this.DoRegister.Text = "회원가입";
            this.DoRegister.UseVisualStyleBackColor = true;
            this.DoRegister.Click += new System.EventHandler(this.DoRegister_Click);
            // 
            // PWConformBox
            // 
            this.PWConformBox.Location = new System.Drawing.Point(105, 94);
            this.PWConformBox.Name = "PWConformBox";
            this.PWConformBox.PasswordChar = '●';
            this.PWConformBox.Size = new System.Drawing.Size(100, 21);
            this.PWConformBox.TabIndex = 3;
            this.PWConformBox.Leave += new System.EventHandler(this.PWConformBox_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "비밀번호 확인";
            // 
            // IDCheck
            // 
            this.IDCheck.Location = new System.Drawing.Point(211, 36);
            this.IDCheck.Name = "IDCheck";
            this.IDCheck.Size = new System.Drawing.Size(61, 23);
            this.IDCheck.TabIndex = 1;
            this.IDCheck.Text = "중복검사";
            this.IDCheck.UseVisualStyleBackColor = true;
            this.IDCheck.Click += new System.EventHandler(this.IDCheck_Click);
            // 
            // NickNameBox
            // 
            this.NickNameBox.Location = new System.Drawing.Point(105, 124);
            this.NickNameBox.Name = "NickNameBox";
            this.NickNameBox.Size = new System.Drawing.Size(100, 21);
            this.NickNameBox.TabIndex = 4;
            this.NickNameBox.TextChanged += new System.EventHandler(this.NickNameBox_TextChanged);
            this.NickNameBox.Leave += new System.EventHandler(this.NickNameBox_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "닉네임";
            // 
            // NickNameCheck
            // 
            this.NickNameCheck.Location = new System.Drawing.Point(211, 122);
            this.NickNameCheck.Name = "NickNameCheck";
            this.NickNameCheck.Size = new System.Drawing.Size(61, 23);
            this.NickNameCheck.TabIndex = 5;
            this.NickNameCheck.Text = "중복검사";
            this.NickNameCheck.UseVisualStyleBackColor = true;
            this.NickNameCheck.Click += new System.EventHandler(this.NickNameCheck_Click);
            // 
            // EmailBox
            // 
            this.EmailBox.Location = new System.Drawing.Point(105, 151);
            this.EmailBox.Name = "EmailBox";
            this.EmailBox.Size = new System.Drawing.Size(100, 21);
            this.EmailBox.TabIndex = 6;
            this.EmailBox.Leave += new System.EventHandler(this.EmailBox_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "이메일주소";
            // 
            // EmailConfirm
            // 
            this.EmailConfirm.Location = new System.Drawing.Point(211, 151);
            this.EmailConfirm.Name = "EmailConfirm";
            this.EmailConfirm.Size = new System.Drawing.Size(61, 23);
            this.EmailConfirm.TabIndex = 10;
            this.EmailConfirm.Text = "인증하기";
            this.EmailConfirm.UseVisualStyleBackColor = true;
            this.EmailConfirm.Click += new System.EventHandler(this.EmailConfirm_Click);
            // 
            // Register
            // 
            this.AcceptButton = this.DoRegister;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.EmailConfirm);
            this.Controls.Add(this.NickNameCheck);
            this.Controls.Add(this.IDCheck);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PWConformBox);
            this.Controls.Add(this.EmailBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NickNameBox);
            this.Controls.Add(this.PWBox);
            this.Controls.Add(this.IDBox);
            this.Controls.Add(this.DoRegister);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Register";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "회원가입";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Register_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PWBox;
        private System.Windows.Forms.TextBox IDBox;
        private System.Windows.Forms.Button DoRegister;
        private System.Windows.Forms.TextBox PWConformBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button IDCheck;
        private System.Windows.Forms.TextBox NickNameBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button NickNameCheck;
        private System.Windows.Forms.TextBox EmailBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button EmailConfirm;
    }
}