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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.memo = new System.Windows.Forms.TextBox();
            this.ownCard = new System.Windows.Forms.PictureBox();
            this.description = new System.Windows.Forms.Label();
            this.TeamBuildCompleteButton = new System.Windows.Forms.Button();
            this.labelTeamStr = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ownCard)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(368, 137);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(113, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(860, 181);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // memo
            // 
            this.memo.Location = new System.Drawing.Point(523, 228);
            this.memo.Multiline = true;
            this.memo.Name = "memo";
            this.memo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.memo.Size = new System.Drawing.Size(285, 314);
            this.memo.TabIndex = 13;
            this.memo.Enter += new System.EventHandler(this.memo_Enter);
            // 
            // ownCard
            // 
            this.ownCard.Location = new System.Drawing.Point(814, 228);
            this.ownCard.Name = "ownCard";
            this.ownCard.Size = new System.Drawing.Size(100, 50);
            this.ownCard.TabIndex = 14;
            this.ownCard.TabStop = false;
            // 
            // description
            // 
            this.description.AutoSize = true;
            this.description.Location = new System.Drawing.Point(814, 281);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(121, 12);
            this.description.TabIndex = 15;
            this.description.Text = "설명문이 들어옵니다.";
            // 
            // TeamBuildCompleteButton
            // 
            this.TeamBuildCompleteButton.Enabled = false;
            this.TeamBuildCompleteButton.Location = new System.Drawing.Point(878, 152);
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
            this.labelTeamStr.Location = new System.Drawing.Point(681, 192);
            this.labelTeamStr.Name = "labelTeamStr";
            this.labelTeamStr.Size = new System.Drawing.Size(173, 12);
            this.labelTeamStr.TabIndex = 15;
            this.labelTeamStr.Text = "총 0 명 중 0 명 선택되었습니다";
            // 
            // Avalron
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(997, 554);
            this.Controls.Add(this.labelTeamStr);
            this.Controls.Add(this.description);
            this.Controls.Add(this.ownCard);
            this.Controls.Add(this.memo);
            this.Controls.Add(this.TeamBuildCompleteButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Avalron";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Avalron";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ownCard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox memo;
        private System.Windows.Forms.PictureBox ownCard;
        private System.Windows.Forms.Label description;
        private System.Windows.Forms.Button TeamBuildCompleteButton;
        private System.Windows.Forms.Label labelTeamStr;
    }
}