namespace Avalron
{
    partial class findID
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
            this.label5 = new System.Windows.Forms.Label();
            this.EmailBox = new System.Windows.Forms.TextBox();
            this.find = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "이메일주소";
            // 
            // EmailBox
            // 
            this.EmailBox.Location = new System.Drawing.Point(117, 88);
            this.EmailBox.Name = "EmailBox";
            this.EmailBox.Size = new System.Drawing.Size(146, 21);
            this.EmailBox.TabIndex = 0;
            this.EmailBox.Leave += new System.EventHandler(this.EmailBox_Leave);
            // 
            // find
            // 
            this.find.Location = new System.Drawing.Point(197, 137);
            this.find.Name = "find";
            this.find.Size = new System.Drawing.Size(75, 23);
            this.find.TabIndex = 1;
            this.find.Text = "찾기";
            this.find.UseVisualStyleBackColor = true;
            this.find.Click += new System.EventHandler(this.find_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "회원가입시 입력하신 이메일주소를 입력해주세요";
            // 
            // findID
            // 
            this.AcceptButton = this.find;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 179);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.EmailBox);
            this.Controls.Add(this.find);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "findID";
            this.ShowIcon = false;
            this.Text = "아이디 찾기";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox EmailBox;
        private System.Windows.Forms.Button find;
        private System.Windows.Forms.Label label1;
    }
}