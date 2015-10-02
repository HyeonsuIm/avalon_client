namespace Avalron
{
    partial class LobbyRoomPassword
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
            this.LobbyRoomPassword_Comein = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LobbyRoomPassword_Passbox = new System.Windows.Forms.TextBox();
            this.LobbyRoomPassword_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LobbyRoomPassword_Comein
            // 
            this.LobbyRoomPassword_Comein.Location = new System.Drawing.Point(66, 84);
            this.LobbyRoomPassword_Comein.Name = "LobbyRoomPassword_Comein";
            this.LobbyRoomPassword_Comein.Size = new System.Drawing.Size(75, 23);
            this.LobbyRoomPassword_Comein.TabIndex = 0;
            this.LobbyRoomPassword_Comein.Text = "입장";
            this.LobbyRoomPassword_Comein.UseVisualStyleBackColor = true;
            this.LobbyRoomPassword_Comein.Click += new System.EventHandler(this.LobbyRoomPassword_Comein_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "비밀번호";
            // 
            // LobbyRoomPassword_Passbox
            // 
            this.LobbyRoomPassword_Passbox.Location = new System.Drawing.Point(108, 45);
            this.LobbyRoomPassword_Passbox.Name = "LobbyRoomPassword_Passbox";
            this.LobbyRoomPassword_Passbox.Size = new System.Drawing.Size(196, 21);
            this.LobbyRoomPassword_Passbox.TabIndex = 0;
            this.LobbyRoomPassword_Passbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LobbyRoomPassword_Passbox_KeyDown);
            // 
            // LobbyRoomPassword_Close
            // 
            this.LobbyRoomPassword_Close.Location = new System.Drawing.Point(206, 84);
            this.LobbyRoomPassword_Close.Name = "LobbyRoomPassword_Close";
            this.LobbyRoomPassword_Close.Size = new System.Drawing.Size(75, 23);
            this.LobbyRoomPassword_Close.TabIndex = 3;
            this.LobbyRoomPassword_Close.Text = "닫기";
            this.LobbyRoomPassword_Close.UseVisualStyleBackColor = true;
            this.LobbyRoomPassword_Close.Click += new System.EventHandler(this.LobbyRoomPassword_Close_Click);
            // 
            // LobbyRoomPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = global::Avalron.Properties.Resources.긴배경;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(347, 139);
            this.Controls.Add(this.LobbyRoomPassword_Close);
            this.Controls.Add(this.LobbyRoomPassword_Passbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LobbyRoomPassword_Comein);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LobbyRoomPassword";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LobbyRoomPassword";
            this.Shown += new System.EventHandler(this.LobbyRoomPassword_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LobbyRoomPassword_Comein;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LobbyRoomPassword_Passbox;
        private System.Windows.Forms.Button LobbyRoomPassword_Close;
    }
}