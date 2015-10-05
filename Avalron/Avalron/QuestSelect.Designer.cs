namespace Avalron.Avalron
{
    partial class QuestSelect 
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
            this.reject = new System.Windows.Forms.PictureBox();
            this.approve = new System.Windows.Forms.PictureBox();
            this.Notice = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.reject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.approve)).BeginInit();
            this.SuspendLayout();
            // 
            // reject
            // 
            this.reject.Image = global::Avalron.Properties.Resources.Avalon_fail;
            this.reject.Location = new System.Drawing.Point(196, 74);
            this.reject.Name = "reject";
            this.reject.Size = new System.Drawing.Size(180, 187);
            this.reject.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.reject.TabIndex = 1;
            this.reject.TabStop = false;
            this.reject.Click += new System.EventHandler(this.reject_Click);
            // 
            // approve
            // 
            this.approve.Image = global::Avalron.Properties.Resources.Avalon_success;
            this.approve.Location = new System.Drawing.Point(12, 74);
            this.approve.Name = "approve";
            this.approve.Size = new System.Drawing.Size(178, 187);
            this.approve.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.approve.TabIndex = 0;
            this.approve.TabStop = false;
            this.approve.Click += new System.EventHandler(this.approve_Click);
            // 
            // Notice
            // 
            this.Notice.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Notice.Location = new System.Drawing.Point(1, 36);
            this.Notice.Name = "Notice";
            this.Notice.Size = new System.Drawing.Size(393, 21);
            this.Notice.TabIndex = 2;
            this.Notice.Text = "원정의 성공여부를 선택해 주세요";
            this.Notice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // QuestSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 285);
            this.ControlBox = false;
            this.Controls.Add(this.Notice);
            this.Controls.Add(this.reject);
            this.Controls.Add(this.approve);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuestSelect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuestSelect";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.reject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.approve)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox approve;
        private System.Windows.Forms.PictureBox reject;
        private System.Windows.Forms.Label Notice;
    }
}