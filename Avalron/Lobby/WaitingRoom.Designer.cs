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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitingRoom));
            this.RoomSettingButton = new System.Windows.Forms.Button();
            this.RoomOut = new System.Windows.Forms.Button();
            this.RoomGoButton = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // RoomSettingButton
            // 
            this.RoomSettingButton.BackColor = System.Drawing.Color.Transparent;
            this.RoomSettingButton.BackgroundImage = global::Avalron.Properties.Resources.WR_방_설정;
            this.RoomSettingButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RoomSettingButton.FlatAppearance.BorderSize = 0;
            this.RoomSettingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RoomSettingButton.Location = new System.Drawing.Point(642, 456);
            this.RoomSettingButton.Name = "RoomSettingButton";
            this.RoomSettingButton.Size = new System.Drawing.Size(109, 38);
            this.RoomSettingButton.TabIndex = 0;
            this.RoomSettingButton.UseVisualStyleBackColor = false;
            this.RoomSettingButton.Click += new System.EventHandler(this.RoomSetting_Click);
            // 
            // RoomOut
            // 
            this.RoomOut.BackColor = System.Drawing.Color.Transparent;
            this.RoomOut.BackgroundImage = global::Avalron.Properties.Resources.WR_나가기;
            this.RoomOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RoomOut.FlatAppearance.BorderSize = 0;
            this.RoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RoomOut.Location = new System.Drawing.Point(642, 500);
            this.RoomOut.Name = "RoomOut";
            this.RoomOut.Size = new System.Drawing.Size(109, 38);
            this.RoomOut.TabIndex = 14;
            this.RoomOut.UseVisualStyleBackColor = false;
            this.RoomOut.Click += new System.EventHandler(this.RoomOut_Click);
            // 
            // RoomGoButton
            // 
            this.RoomGoButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.RoomGoButton.BackColor = System.Drawing.Color.Transparent;
            this.RoomGoButton.BackgroundImage = global::Avalron.Properties.Resources.WR_준비;
            this.RoomGoButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RoomGoButton.FlatAppearance.BorderSize = 0;
            this.RoomGoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RoomGoButton.Location = new System.Drawing.Point(554, 456);
            this.RoomGoButton.Name = "RoomGoButton";
            this.RoomGoButton.Size = new System.Drawing.Size(79, 82);
            this.RoomGoButton.TabIndex = 0;
            this.RoomGoButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RoomGoButton.UseVisualStyleBackColor = false;
            this.RoomGoButton.CheckedChanged += new System.EventHandler(this.RoomGoButton_CheckedChanged);
            this.RoomGoButton.Click += new System.EventHandler(this.Go_Click);
            // 
            // WaitingRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Avalron.Properties.Resources.WR_대기방;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.RoomGoButton);
            this.Controls.Add(this.RoomOut);
            this.Controls.Add(this.RoomSettingButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WaitingRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "`";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WaitingRoom_FormClosing);
            this.Shown += new System.EventHandler(this.WaitingRoom_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RoomSettingButton;
        private System.Windows.Forms.Button RoomOut;
        private System.Windows.Forms.CheckBox RoomGoButton;
    }
}