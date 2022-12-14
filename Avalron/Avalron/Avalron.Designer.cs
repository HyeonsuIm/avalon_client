namespace Avalron.Avalron
{
    partial class Avalron
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Avalron));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.memo = new System.Windows.Forms.TextBox();
            this.ownCard = new System.Windows.Forms.PictureBox();
            this.TeamBuildCompleteButton = new System.Windows.Forms.Button();
            this.labelTeamStr = new System.Windows.Forms.Label();
            this.ManualBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ownCard)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // memo
            // 
            this.memo.Location = new System.Drawing.Point(737, 536);
            this.memo.Multiline = true;
            this.memo.Name = "memo";
            this.memo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.memo.Size = new System.Drawing.Size(551, 172);
            this.memo.TabIndex = 13;
            this.memo.Enter += new System.EventHandler(this.memo_Enter);
            // 
            // ownCard
            // 
            this.ownCard.BackColor = System.Drawing.Color.Transparent;
            this.ownCard.BackgroundImage = global::Avalron.Properties.Resources.대기방채팅;
            this.ownCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ownCard.Location = new System.Drawing.Point(1108, 273);
            this.ownCard.Name = "ownCard";
            this.ownCard.Size = new System.Drawing.Size(180, 252);
            this.ownCard.TabIndex = 14;
            this.ownCard.TabStop = false;
            this.ownCard.Click += new System.EventHandler(this.ownCard_Click);
            // 
            // TeamBuildCompleteButton
            // 
            this.TeamBuildCompleteButton.BackColor = System.Drawing.Color.Transparent;
            this.TeamBuildCompleteButton.BackgroundImage = global::Avalron.Properties.Resources.Avalron_원정출발;
            this.TeamBuildCompleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TeamBuildCompleteButton.Enabled = false;
            this.TeamBuildCompleteButton.FlatAppearance.BorderSize = 0;
            this.TeamBuildCompleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TeamBuildCompleteButton.Location = new System.Drawing.Point(560, 250);
            this.TeamBuildCompleteButton.Name = "TeamBuildCompleteButton";
            this.TeamBuildCompleteButton.Size = new System.Drawing.Size(119, 52);
            this.TeamBuildCompleteButton.TabIndex = 12;
            this.TeamBuildCompleteButton.UseVisualStyleBackColor = false;
            this.TeamBuildCompleteButton.Visible = false;
            this.TeamBuildCompleteButton.Click += new System.EventHandler(this.TeamBuildCompleteButton_Click);
            // 
            // labelTeamStr
            // 
            this.labelTeamStr.AutoSize = true;
            this.labelTeamStr.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelTeamStr.Location = new System.Drawing.Point(86, 268);
            this.labelTeamStr.Name = "labelTeamStr";
            this.labelTeamStr.Size = new System.Drawing.Size(305, 21);
            this.labelTeamStr.TabIndex = 15;
            this.labelTeamStr.Text = "총 0 명 중 0 명 선택되었습니다";
            // 
            // ManualBox
            // 
            this.ManualBox.Location = new System.Drawing.Point(1108, 273);
            this.ManualBox.Multiline = true;
            this.ManualBox.Name = "ManualBox";
            this.ManualBox.ReadOnly = true;
            this.ManualBox.Size = new System.Drawing.Size(180, 252);
            this.ManualBox.TabIndex = 0;
            this.ManualBox.TabStop = false;
            this.ManualBox.Text = "메뉴얼 박스";
            this.ManualBox.Click += new System.EventHandler(this.ManualBox_Click);
            // 
            // Avalron
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Avalron.Properties.Resources.Avalron_BG2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1300, 720);
            this.Controls.Add(this.ManualBox);
            this.Controls.Add(this.labelTeamStr);
            this.Controls.Add(this.ownCard);
            this.Controls.Add(this.memo);
            this.Controls.Add(this.TeamBuildCompleteButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Avalron";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Avalron";
            ((System.ComponentModel.ISupportInitialize)(this.ownCard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox memo;
        private System.Windows.Forms.PictureBox ownCard;
        private System.Windows.Forms.Button TeamBuildCompleteButton;
        private System.Windows.Forms.Label labelTeamStr;
        private System.Windows.Forms.TextBox ManualBox;
    }
}