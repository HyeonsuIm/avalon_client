namespace Avalron
{
    partial class WaitingRoom
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
            this.RoomSettingButton = new System.Windows.Forms.Button();
            this.RoomGoButton = new System.Windows.Forms.Button();
            this.RoomInvitationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RoomSettingButton
            // 
            this.RoomSettingButton.Location = new System.Drawing.Point(616, 456);
            this.RoomSettingButton.Name = "RoomSettingButton";
            this.RoomSettingButton.Size = new System.Drawing.Size(75, 23);
            this.RoomSettingButton.TabIndex = 0;
            this.RoomSettingButton.Text = "방설정";
            this.RoomSettingButton.UseVisualStyleBackColor = true;
            this.RoomSettingButton.Click += new System.EventHandler(this.RoomSetting_Click);
            // 
            // RoomGoButton
            // 
            this.RoomGoButton.Location = new System.Drawing.Point(616, 485);
            this.RoomGoButton.Name = "RoomGoButton";
            this.RoomGoButton.Size = new System.Drawing.Size(156, 60);
            this.RoomGoButton.TabIndex = 0;
            this.RoomGoButton.Text = "준비";
            this.RoomGoButton.UseVisualStyleBackColor = true;
            this.RoomGoButton.Click += new System.EventHandler(this.Go_Click);
            // 
            // RoomInvitationButton
            // 
            this.RoomInvitationButton.Location = new System.Drawing.Point(697, 456);
            this.RoomInvitationButton.Name = "RoomInvitationButton";
            this.RoomInvitationButton.Size = new System.Drawing.Size(75, 23);
            this.RoomInvitationButton.TabIndex = 1;
            this.RoomInvitationButton.Text = "초대";
            this.RoomInvitationButton.UseVisualStyleBackColor = true;
            // 
            // WaittingRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.RoomInvitationButton);
            this.Controls.Add(this.RoomGoButton);
            this.Controls.Add(this.RoomSettingButton);
            this.Name = "WaittingRoom";
            this.Text = "WaittingRoom";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RoomSettingButton;
        private System.Windows.Forms.Button RoomGoButton;
        private System.Windows.Forms.Button RoomInvitationButton;
    }
}