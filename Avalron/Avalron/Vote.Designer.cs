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
            ((System.ComponentModel.ISupportInitialize)(this.reject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.approve)).BeginInit();
            this.SuspendLayout();
            // 
            // reject
            // 
            this.reject.Image = global::Avalron.Properties.Resources.Reject;
            this.reject.Location = new System.Drawing.Point(278, 53);
            this.reject.Name = "reject";
            this.reject.Size = new System.Drawing.Size(180, 187);
            this.reject.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.reject.TabIndex = 1;
            this.reject.TabStop = false;
            this.reject.Click += new System.EventHandler(this.reject_Click);
            // 
            // approve
            // 
            this.approve.Image = global::Avalron.Properties.Resources.Approve;
            this.approve.Location = new System.Drawing.Point(54, 53);
            this.approve.Name = "approve";
            this.approve.Size = new System.Drawing.Size(178, 187);
            this.approve.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.approve.TabIndex = 0;
            this.approve.TabStop = false;
            this.approve.Click += new System.EventHandler(this.approve_Click);
            // 
            // Vote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 310);
            this.Controls.Add(this.reject);
            this.Controls.Add(this.approve);
            this.Name = "Vote";
            this.Text = "Vote";
            ((System.ComponentModel.ISupportInitialize)(this.reject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.approve)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox approve;
        private System.Windows.Forms.PictureBox reject;
    }
}