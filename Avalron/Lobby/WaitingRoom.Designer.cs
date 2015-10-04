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
            this.RoomINFO = new System.Windows.Forms.GroupBox();
            this.RoomMaxNumber = new System.Windows.Forms.Label();
            this.RoomType = new System.Windows.Forms.Label();
            this.RoomName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RoomOut = new System.Windows.Forms.Button();
            this.RoomGoButton = new System.Windows.Forms.CheckBox();
            this.RoomINFO.SuspendLayout();
            this.SuspendLayout();
            // 
            // RoomSettingButton
            // 
            this.RoomSettingButton.Location = new System.Drawing.Point(663, 456);
            this.RoomSettingButton.Name = "RoomSettingButton";
            this.RoomSettingButton.Size = new System.Drawing.Size(109, 38);
            this.RoomSettingButton.TabIndex = 0;
            this.RoomSettingButton.Text = "방설정";
            this.RoomSettingButton.UseVisualStyleBackColor = true;
            this.RoomSettingButton.Click += new System.EventHandler(this.RoomSetting_Click);
            // 
            // RoomINFO
            // 
            this.RoomINFO.Controls.Add(this.RoomMaxNumber);
            this.RoomINFO.Controls.Add(this.RoomType);
            this.RoomINFO.Controls.Add(this.RoomName);
            this.RoomINFO.Controls.Add(this.label1);
            this.RoomINFO.Controls.Add(this.label3);
            this.RoomINFO.Controls.Add(this.label2);
            this.RoomINFO.Location = new System.Drawing.Point(588, 310);
            this.RoomINFO.Name = "RoomINFO";
            this.RoomINFO.Size = new System.Drawing.Size(184, 140);
            this.RoomINFO.TabIndex = 13;
            this.RoomINFO.TabStop = false;
            this.RoomINFO.Text = "방정보";
            // 
            // RoomMaxNumber
            // 
            this.RoomMaxNumber.AutoSize = true;
            this.RoomMaxNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RoomMaxNumber.Location = new System.Drawing.Point(73, 77);
            this.RoomMaxNumber.Name = "RoomMaxNumber";
            this.RoomMaxNumber.Size = new System.Drawing.Size(60, 12);
            this.RoomMaxNumber.TabIndex = 4;
            this.RoomMaxNumber.Text = "UserNICK";
            // 
            // RoomType
            // 
            this.RoomType.AutoSize = true;
            this.RoomType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RoomType.Location = new System.Drawing.Point(73, 52);
            this.RoomType.Name = "RoomType";
            this.RoomType.Size = new System.Drawing.Size(60, 12);
            this.RoomType.TabIndex = 4;
            this.RoomType.Text = "UserNICK";
            // 
            // RoomName
            // 
            this.RoomName.AutoSize = true;
            this.RoomName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RoomName.Location = new System.Drawing.Point(73, 27);
            this.RoomName.Name = "RoomName";
            this.RoomName.Size = new System.Drawing.Size(60, 12);
            this.RoomName.TabIndex = 4;
            this.RoomName.Text = "UserNICK";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(13, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "최대인원";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(13, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(13, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "방제목";
            // 
            // RoomOut
            // 
            this.RoomOut.Location = new System.Drawing.Point(663, 500);
            this.RoomOut.Name = "RoomOut";
            this.RoomOut.Size = new System.Drawing.Size(109, 38);
            this.RoomOut.TabIndex = 14;
            this.RoomOut.Text = "나가기";
            this.RoomOut.UseVisualStyleBackColor = true;
            this.RoomOut.Click += new System.EventHandler(this.RoomOut_Click);
            // 
            // RoomGoButton
            // 
            this.RoomGoButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.RoomGoButton.Location = new System.Drawing.Point(588, 456);
            this.RoomGoButton.Name = "RoomGoButton";
            this.RoomGoButton.Size = new System.Drawing.Size(66, 82);
            this.RoomGoButton.TabIndex = 0;
            this.RoomGoButton.Text = "준비";
            this.RoomGoButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RoomGoButton.UseVisualStyleBackColor = true;
            this.RoomGoButton.Click += new System.EventHandler(this.Go_Click);
            // 
            // WaitingRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.RoomGoButton);
            this.Controls.Add(this.RoomOut);
            this.Controls.Add(this.RoomINFO);
            this.Controls.Add(this.RoomSettingButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WaitingRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "`";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WaitingRoom_FormClosing);
            this.Shown += new System.EventHandler(this.WaitingRoom_Shown);
            this.RoomINFO.ResumeLayout(false);
            this.RoomINFO.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RoomSettingButton;
        private System.Windows.Forms.GroupBox RoomINFO;
        private System.Windows.Forms.Label RoomMaxNumber;
        private System.Windows.Forms.Label RoomType;
        private System.Windows.Forms.Label RoomName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button RoomOut;
        private System.Windows.Forms.CheckBox RoomGoButton;
    }
}