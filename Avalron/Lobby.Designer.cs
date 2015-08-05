namespace Avalron
{
    partial class Lobby
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lobby));
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.UserList = new System.Windows.Forms.ListBox();
            this.SendMass = new System.Windows.Forms.Button();
            this.ChatingBar = new System.Windows.Forms.TextBox();
            this.ChatingWisper = new System.Windows.Forms.TextBox();
            this.Logout = new System.Windows.Forms.Button();
            this.TitleBar = new System.Windows.Forms.Panel();
            this.Exit = new System.Windows.Forms.Button();
            this.Minimized = new System.Windows.Forms.Button();
            this.RoomListLeft = new System.Windows.Forms.Button();
            this.RoomListRight = new System.Windows.Forms.Button();
            this.RoomListIndex = new System.Windows.Forms.Label();
            this.ChatingLog = new System.Windows.Forms.RichTextBox();
            this.RoomMake = new System.Windows.Forms.Button();
            this.Refresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.TitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // UserList
            // 
            resources.ApplyResources(this.UserList, "UserList");
            this.UserList.FormattingEnabled = true;
            this.UserList.Name = "UserList";
            // 
            // SendMass
            // 
            resources.ApplyResources(this.SendMass, "SendMass");
            this.SendMass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SendMass.Name = "SendMass";
            this.SendMass.UseVisualStyleBackColor = true;
            this.SendMass.Click += new System.EventHandler(this.SendMass_Click);
            // 
            // ChatingBar
            // 
            resources.ApplyResources(this.ChatingBar, "ChatingBar");
            this.ChatingBar.Name = "ChatingBar";
            this.ChatingBar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatingBar_KeyDown);
            // 
            // ChatingWisper
            // 
            resources.ApplyResources(this.ChatingWisper, "ChatingWisper");
            this.ChatingWisper.Name = "ChatingWisper";
            // 
            // Logout
            // 
            resources.ApplyResources(this.Logout, "Logout");
            this.Logout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Logout.Name = "Logout";
            this.Logout.UseVisualStyleBackColor = true;
            this.Logout.Click += new System.EventHandler(this.Logout_Click);
            // 
            // TitleBar
            // 
            this.TitleBar.Controls.Add(this.Exit);
            this.TitleBar.Controls.Add(this.Minimized);
            resources.ApplyResources(this.TitleBar, "TitleBar");
            this.TitleBar.Name = "TitleBar";
            this.TitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // Exit
            // 
            this.Exit.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.Exit, "Exit");
            this.Exit.Name = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Minimized
            // 
            this.Minimized.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.Minimized, "Minimized");
            this.Minimized.Name = "Minimized";
            this.Minimized.UseVisualStyleBackColor = true;
            this.Minimized.Click += new System.EventHandler(this.Minimalize_Click);
            // 
            // RoomListLeft
            // 
            this.RoomListLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RoomListLeft.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.RoomListLeft, "RoomListLeft");
            this.RoomListLeft.Image = global::Avalron.Properties.Resources.left_arrow_icon;
            this.RoomListLeft.Name = "RoomListLeft";
            this.RoomListLeft.UseVisualStyleBackColor = true;
            this.RoomListLeft.Click += new System.EventHandler(this.RoomListLeft_Click);
            // 
            // RoomListRight
            // 
            this.RoomListRight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RoomListRight.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.RoomListRight, "RoomListRight");
            this.RoomListRight.Image = global::Avalron.Properties.Resources.right_arrow_icon_16947;
            this.RoomListRight.Name = "RoomListRight";
            this.RoomListRight.UseVisualStyleBackColor = true;
            this.RoomListRight.Click += new System.EventHandler(this.RoomListRight_Click);
            // 
            // RoomListIndex
            // 
            this.RoomListIndex.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.RoomListIndex, "RoomListIndex");
            this.RoomListIndex.Name = "RoomListIndex";
            // 
            // ChatingLog
            // 
            resources.ApplyResources(this.ChatingLog, "ChatingLog");
            this.ChatingLog.Name = "ChatingLog";
            this.ChatingLog.ReadOnly = true;
            this.ChatingLog.TabStop = false;
            this.ChatingLog.TextChanged += new System.EventHandler(this.ChatingLog_TextChanged);
            // 
            // RoomMake
            // 
            this.RoomMake.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.RoomMake, "RoomMake");
            this.RoomMake.Name = "RoomMake";
            this.RoomMake.UseVisualStyleBackColor = true;
            this.RoomMake.Click += new System.EventHandler(this.RoomMake_Click);
            // 
            // Refresh
            // 
            resources.ApplyResources(this.Refresh, "Refresh");
            this.Refresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Refresh.FlatAppearance.BorderSize = 0;
            this.Refresh.Image = global::Avalron.Properties.Resources.Refresh_icon;
            this.Refresh.Name = "Refresh";
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // Lobby
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Avalron.Properties.Resources.illust_006;
            this.Controls.Add(this.Refresh);
            this.Controls.Add(this.RoomMake);
            this.Controls.Add(this.ChatingLog);
            this.Controls.Add(this.RoomListIndex);
            this.Controls.Add(this.RoomListRight);
            this.Controls.Add(this.RoomListLeft);
            this.Controls.Add(this.TitleBar);
            this.Controls.Add(this.UserList);
            this.Controls.Add(this.Logout);
            this.Controls.Add(this.ChatingWisper);
            this.Controls.Add(this.ChatingBar);
            this.Controls.Add(this.SendMass);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Lobby";
            this.Load += new System.EventHandler(this.Lobby_Load);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.TitleBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.ListBox UserList;
        private System.Windows.Forms.Button SendMass;
        private System.Windows.Forms.TextBox ChatingWisper;
        private System.Windows.Forms.TextBox ChatingBar;
        private System.Windows.Forms.Button Logout;
        private System.Windows.Forms.Panel TitleBar;
        private System.Windows.Forms.Label RoomListIndex;
        private System.Windows.Forms.Button RoomListRight;
        private System.Windows.Forms.Button RoomListLeft;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button Minimized;
        private System.Windows.Forms.RichTextBox ChatingLog;
        private System.Windows.Forms.Button Refresh;
        private System.Windows.Forms.Button RoomMake;
    }
}

