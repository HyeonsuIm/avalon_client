namespace Avalron
{
    partial class LobbyRoomMake
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
            this.Room_Make = new System.Windows.Forms.Button();
            this.LRM_Close = new System.Windows.Forms.Button();
            this.Room_Make_Name = new System.Windows.Forms.TextBox();
            this.Room_Make_Pass = new System.Windows.Forms.TextBox();
            this.Room_Make_NameLabel = new System.Windows.Forms.Label();
            this.Room_Make_PassLabel = new System.Windows.Forms.Label();
            this.Room_Make_PassBox = new System.Windows.Forms.CheckBox();
            this.Room_Make_MaxMember = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Room_Make_Type = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Room_Make
            // 
            this.Room_Make.BackColor = System.Drawing.Color.Transparent;
            this.Room_Make.BackgroundImage = global::Avalron.Properties.Resources.버튼;
            this.Room_Make.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Room_Make.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Room_Make.FlatAppearance.BorderSize = 0;
            this.Room_Make.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Room_Make.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Room_Make.Location = new System.Drawing.Point(125, 239);
            this.Room_Make.Name = "Room_Make";
            this.Room_Make.Size = new System.Drawing.Size(130, 40);
            this.Room_Make.TabIndex = 0;
            this.Room_Make.Text = "방만들기";
            this.Room_Make.UseVisualStyleBackColor = false;
            this.Room_Make.Click += new System.EventHandler(this.Room_Make_Click);
            // 
            // LRM_Close
            // 
            this.LRM_Close.BackColor = System.Drawing.Color.Transparent;
            this.LRM_Close.BackgroundImage = global::Avalron.Properties.Resources.버튼;
            this.LRM_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LRM_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LRM_Close.FlatAppearance.BorderSize = 0;
            this.LRM_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LRM_Close.ForeColor = System.Drawing.SystemColors.Control;
            this.LRM_Close.Location = new System.Drawing.Point(288, 239);
            this.LRM_Close.Name = "LRM_Close";
            this.LRM_Close.Size = new System.Drawing.Size(130, 40);
            this.LRM_Close.TabIndex = 1;
            this.LRM_Close.Text = "닫기";
            this.LRM_Close.UseVisualStyleBackColor = false;
            this.LRM_Close.Click += new System.EventHandler(this.LRM_Close_Click);
            // 
            // Room_Make_Name
            // 
            this.Room_Make_Name.Location = new System.Drawing.Point(168, 86);
            this.Room_Make_Name.MaxLength = 15;
            this.Room_Make_Name.Name = "Room_Make_Name";
            this.Room_Make_Name.Size = new System.Drawing.Size(218, 21);
            this.Room_Make_Name.TabIndex = 2;
            this.Room_Make_Name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Room_Make_Name_KeyDown);
            // 
            // Room_Make_Pass
            // 
            this.Room_Make_Pass.BackColor = System.Drawing.Color.Gray;
            this.Room_Make_Pass.Location = new System.Drawing.Point(168, 132);
            this.Room_Make_Pass.MaxLength = 10;
            this.Room_Make_Pass.Name = "Room_Make_Pass";
            this.Room_Make_Pass.ReadOnly = true;
            this.Room_Make_Pass.Size = new System.Drawing.Size(215, 21);
            this.Room_Make_Pass.TabIndex = 3;
            this.Room_Make_Pass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Room_Make_Pass_KeyDown);
            // 
            // Room_Make_NameLabel
            // 
            this.Room_Make_NameLabel.AutoSize = true;
            this.Room_Make_NameLabel.BackColor = System.Drawing.Color.Transparent;
            this.Room_Make_NameLabel.Location = new System.Drawing.Point(93, 91);
            this.Room_Make_NameLabel.Name = "Room_Make_NameLabel";
            this.Room_Make_NameLabel.Size = new System.Drawing.Size(45, 12);
            this.Room_Make_NameLabel.TabIndex = 4;
            this.Room_Make_NameLabel.Text = "방 제목";
            // 
            // Room_Make_PassLabel
            // 
            this.Room_Make_PassLabel.AutoSize = true;
            this.Room_Make_PassLabel.BackColor = System.Drawing.Color.Transparent;
            this.Room_Make_PassLabel.Location = new System.Drawing.Point(93, 135);
            this.Room_Make_PassLabel.Name = "Room_Make_PassLabel";
            this.Room_Make_PassLabel.Size = new System.Drawing.Size(53, 12);
            this.Room_Make_PassLabel.TabIndex = 5;
            this.Room_Make_PassLabel.Text = "비밀번호";
            // 
            // Room_Make_PassBox
            // 
            this.Room_Make_PassBox.AutoSize = true;
            this.Room_Make_PassBox.BackColor = System.Drawing.Color.Transparent;
            this.Room_Make_PassBox.Location = new System.Drawing.Point(401, 135);
            this.Room_Make_PassBox.Name = "Room_Make_PassBox";
            this.Room_Make_PassBox.Size = new System.Drawing.Size(60, 16);
            this.Room_Make_PassBox.TabIndex = 7;
            this.Room_Make_PassBox.Text = "비밀방";
            this.Room_Make_PassBox.UseVisualStyleBackColor = false;
            this.Room_Make_PassBox.CheckedChanged += new System.EventHandler(this.Room_Make_PassBox_CheckedChanged);
            // 
            // Room_Make_MaxMember
            // 
            this.Room_Make_MaxMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Room_Make_MaxMember.FormattingEnabled = true;
            this.Room_Make_MaxMember.Location = new System.Drawing.Point(364, 183);
            this.Room_Make_MaxMember.Name = "Room_Make_MaxMember";
            this.Room_Make_MaxMember.Size = new System.Drawing.Size(60, 20);
            this.Room_Make_MaxMember.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(309, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "인원수";
            // 
            // Room_Make_Type
            // 
            this.Room_Make_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Room_Make_Type.FormattingEnabled = true;
            this.Room_Make_Type.Location = new System.Drawing.Point(154, 183);
            this.Room_Make_Type.Name = "Room_Make_Type";
            this.Room_Make_Type.Size = new System.Drawing.Size(138, 20);
            this.Room_Make_Type.TabIndex = 12;
            this.Room_Make_Type.SelectedIndexChanged += new System.EventHandler(this.Room_Make_Type_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(102, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "Type";
            // 
            // LobbyRoomMake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.BackgroundImage = global::Avalron.Properties.Resources.짧은배경;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(550, 350);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Room_Make_Type);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Room_Make_MaxMember);
            this.Controls.Add(this.Room_Make_PassBox);
            this.Controls.Add(this.Room_Make_PassLabel);
            this.Controls.Add(this.Room_Make_NameLabel);
            this.Controls.Add(this.Room_Make_Pass);
            this.Controls.Add(this.Room_Make_Name);
            this.Controls.Add(this.LRM_Close);
            this.Controls.Add(this.Room_Make);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LobbyRoomMake";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LobbyRoomMake";
            this.Shown += new System.EventHandler(this.LobbyRoomMake_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Room_Make;
        private System.Windows.Forms.Button LRM_Close;
        private System.Windows.Forms.TextBox Room_Make_Name;
        private System.Windows.Forms.TextBox Room_Make_Pass;
        private System.Windows.Forms.Label Room_Make_NameLabel;
        private System.Windows.Forms.Label Room_Make_PassLabel;
        private System.Windows.Forms.CheckBox Room_Make_PassBox;
        private System.Windows.Forms.ComboBox Room_Make_MaxMember;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Room_Make_Type;
        private System.Windows.Forms.Label label2;
    }
}