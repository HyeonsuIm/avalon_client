namespace Avalron.Avalron
{
    partial class Vote
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
            this.reject.BackColor = System.Drawing.Color.Transparent;
            this.reject.Image = global::Avalron.Properties.Resources.Avalon_reject;
            this.reject.Location = new System.Drawing.Point(200, 66);
            this.reject.Name = "reject";
            this.reject.Size = new System.Drawing.Size(180, 187);
            this.reject.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.reject.TabIndex = 1;
            this.reject.TabStop = false;
            this.reject.Click += new System.EventHandler(this.reject_Click);
            // 
            // approve
            // 
            this.approve.BackColor = System.Drawing.Color.Transparent;
            this.approve.Image = global::Avalron.Properties.Resources.Avalon_Approuve;
            this.approve.Location = new System.Drawing.Point(16, 66);
            this.approve.Name = "approve";
            this.approve.Size = new System.Drawing.Size(178, 187);
            this.approve.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.approve.TabIndex = 0;
            this.approve.TabStop = false;
            this.approve.Click += new System.EventHandler(this.approve_Click);
            // 
            // Notice
            // 
            this.Notice.AutoSize = true;
            this.Notice.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Notice.Location = new System.Drawing.Point(64, 30);
            this.Notice.Name = "Notice";
            this.Notice.Size = new System.Drawing.Size(272, 21);
            this.Notice.TabIndex = 3;
            this.Notice.Text = "원정에 찬성하시겠습니까?";
            // 
            // Vote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 274);
            this.ControlBox = false;
            this.Controls.Add(this.Notice);
            this.Controls.Add(this.reject);
            this.Controls.Add(this.approve);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Vote";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vote";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.reject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.approve)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox approve;
        private System.Windows.Forms.PictureBox reject;
        private System.Windows.Forms.Label Notice;
    }
}