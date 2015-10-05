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
            this.memo.Location = new System.Drawing.Point(737, 508);
            this.memo.Multiline = true;
            this.memo.Name = "memo";
            this.memo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.memo.Size = new System.Drawing.Size(551, 200);
            this.memo.TabIndex = 13;
            this.memo.Enter += new System.EventHandler(this.memo_Enter);
            // 
            // ownCard
            // 
            this.ownCard.BackColor = System.Drawing.Color.Transparent;
            this.ownCard.BackgroundImage = global::Avalron.Properties.Resources.대기방채팅;
            this.ownCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ownCard.Location = new System.Drawing.Point(1108, 250);
            this.ownCard.Name = "ownCard";
            this.ownCard.Size = new System.Drawing.Size(180, 252);
            this.ownCard.TabIndex = 14;
            this.ownCard.TabStop = false;
            this.ownCard.Click += new System.EventHandler(this.ownCard_Click);
            // 
            // TeamBuildCompleteButton
            // 
            this.TeamBuildCompleteButton.Enabled = false;
            this.TeamBuildCompleteButton.Location = new System.Drawing.Point(588, 270);
            this.TeamBuildCompleteButton.Name = "TeamBuildCompleteButton";
            this.TeamBuildCompleteButton.Size = new System.Drawing.Size(107, 23);
            this.TeamBuildCompleteButton.TabIndex = 12;
            this.TeamBuildCompleteButton.Text = "원정대 선택 완료";
            this.TeamBuildCompleteButton.UseVisualStyleBackColor = true;
            this.TeamBuildCompleteButton.Visible = false;
            this.TeamBuildCompleteButton.Click += new System.EventHandler(this.TeamBuildCompleteButton_Click);
            // 
            // labelTeamStr
            // 
            this.labelTeamStr.AutoSize = true;
            this.labelTeamStr.Location = new System.Drawing.Point(56, 270);
            this.labelTeamStr.Name = "labelTeamStr";
            this.labelTeamStr.Size = new System.Drawing.Size(173, 12);
            this.labelTeamStr.TabIndex = 15;
            this.labelTeamStr.Text = "총 0 명 중 0 명 선택되었습니다";
            // 
            // ManualBox
            // 
            this.ManualBox.Location = new System.Drawing.Point(1108, 250);
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
            this.BackgroundImage = global::Avalron.Properties.Resources.Avalron_배경;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1300, 720);
            this.Controls.Add(this.ManualBox);
            this.Controls.Add(this.labelTeamStr);
            this.Controls.Add(this.ownCard);
            this.Controls.Add(this.memo);
            this.Controls.Add(this.TeamBuildCompleteButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
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