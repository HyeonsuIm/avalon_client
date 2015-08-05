namespace Avalron
{
    partial class Confirm
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ConfirmBox = new System.Windows.Forms.TextBox();
            this.OK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ReSent = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ConfirmBox
            // 
            this.ConfirmBox.Location = new System.Drawing.Point(118, 80);
            this.ConfirmBox.Name = "ConfirmBox";
            this.ConfirmBox.Size = new System.Drawing.Size(100, 21);
            this.ConfirmBox.TabIndex = 0;
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(201, 114);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 4;
            this.OK.Text = "확인";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "인증번호";
            // 
            // ReSent
            // 
            this.ReSent.Location = new System.Drawing.Point(201, 143);
            this.ReSent.Name = "ReSent";
            this.ReSent.Size = new System.Drawing.Size(75, 23);
            this.ReSent.TabIndex = 4;
            this.ReSent.Text = "재전송";
            this.ReSent.UseVisualStyleBackColor = true;
            this.ReSent.Click += new System.EventHandler(this.ReSent_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "도착하지 않을시 스펨메일 함을 확인해주세요";
            // 
            // Confirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 178);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ReSent);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ConfirmBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Confirm";
            this.Text = "AccountConfirmation";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox ConfirmBox;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ReSent;
        private System.Windows.Forms.Label label2;
    }
}