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
            this.RoomListLeft = new System.Windows.Forms.Button();
            this.RoomListRight = new System.Windows.Forms.Button();
            this.RoomListIndex = new System.Windows.Forms.Label();
            this.ChatingLog = new System.Windows.Forms.RichTextBox();
            this.Refresh = new System.Windows.Forms.Button();
            this.UserINFO = new System.Windows.Forms.GroupBox();
            this.UserSCORE = new System.Windows.Forms.Label();
            this.UserNICK = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RoomMake = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.UserINFO.SuspendLayout();
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
            this.SendMass.BackColor = System.Drawing.Color.Transparent;
            this.SendMass.BackgroundImage = global::Avalron.Properties.Resources.배경;
            this.SendMass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SendMass.FlatAppearance.BorderSize = 0;
            this.SendMass.Name = "SendMass";
            this.SendMass.UseVisualStyleBackColor = false;
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
            this.Logout.BackColor = System.Drawing.Color.Transparent;
            this.Logout.BackgroundImage = global::Avalron.Properties.Resources.배경;
            this.Logout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Logout.FlatAppearance.BorderSize = 0;
            this.Logout.Name = "Logout";
            this.Logout.UseVisualStyleBackColor = false;
            this.Logout.Click += new System.EventHandler(this.Logout_Click);
            // 
            // RoomListLeft
            // 
            this.RoomListLeft.BackColor = System.Drawing.Color.Transparent;
            this.RoomListLeft.BackgroundImage = global::Avalron.Properties.Resources.이전;
            resources.ApplyResources(this.RoomListLeft, "RoomListLeft");
            this.RoomListLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RoomListLeft.FlatAppearance.BorderSize = 0;
            this.RoomListLeft.Name = "RoomListLeft";
            this.RoomListLeft.UseVisualStyleBackColor = false;
            this.RoomListLeft.Click += new System.EventHandler(this.RoomListLeft_Click);
            // 
            // RoomListRight
            // 
            this.RoomListRight.BackColor = System.Drawing.Color.Transparent;
            this.RoomListRight.BackgroundImage = global::Avalron.Properties.Resources.다음;
            resources.ApplyResources(this.RoomListRight, "RoomListRight");
            this.RoomListRight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RoomListRight.FlatAppearance.BorderSize = 0;
            this.RoomListRight.Name = "RoomListRight";
            this.RoomListRight.UseVisualStyleBackColor = false;
            this.RoomListRight.Click += new System.EventHandler(this.RoomListRight_Click);
            // 
            // RoomListIndex
            // 
            this.RoomListIndex.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.RoomListIndex, "RoomListIndex");
            this.RoomListIndex.Image = global::Avalron.Properties.Resources.배경;
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
            // Refresh
            // 
            this.Refresh.BackColor = System.Drawing.Color.Transparent;
            this.Refresh.BackgroundImage = global::Avalron.Properties.Resources.새로고침;
            resources.ApplyResources(this.Refresh, "Refresh");
            this.Refresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Refresh.FlatAppearance.BorderSize = 0;
            this.Refresh.Name = "Refresh";
            this.Refresh.UseVisualStyleBackColor = false;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // UserINFO
            // 
            this.UserINFO.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.UserINFO, "UserINFO");
            this.UserINFO.Controls.Add(this.UserSCORE);
            this.UserINFO.Controls.Add(this.UserNICK);
            this.UserINFO.Controls.Add(this.label3);
            this.UserINFO.Controls.Add(this.label2);
            this.UserINFO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UserINFO.ForeColor = System.Drawing.Color.White;
            this.UserINFO.Name = "UserINFO";
            this.UserINFO.TabStop = false;
            // 
            // UserSCORE
            // 
            resources.ApplyResources(this.UserSCORE, "UserSCORE");
            this.UserSCORE.Name = "UserSCORE";
            // 
            // UserNICK
            // 
            resources.ApplyResources(this.UserNICK, "UserNICK");
            this.UserNICK.Name = "UserNICK";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // RoomMake
            // 
            this.RoomMake.BackColor = System.Drawing.Color.Transparent;
            this.RoomMake.BackgroundImage = global::Avalron.Properties.Resources.방만들기;
            resources.ApplyResources(this.RoomMake, "RoomMake");
            this.RoomMake.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RoomMake.FlatAppearance.BorderSize = 0;
            this.RoomMake.Name = "RoomMake";
            this.RoomMake.UseVisualStyleBackColor = false;
            this.RoomMake.Click += new System.EventHandler(this.RoomMake_Click);
            // 
            // Lobby
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Avalron.Properties.Resources.대기방_배경;
            this.Controls.Add(this.UserINFO);
            this.Controls.Add(this.Refresh);
            this.Controls.Add(this.RoomMake);
            this.Controls.Add(this.ChatingLog);
            this.Controls.Add(this.RoomListIndex);
            this.Controls.Add(this.RoomListRight);
            this.Controls.Add(this.RoomListLeft);
            this.Controls.Add(this.UserList);
            this.Controls.Add(this.Logout);
            this.Controls.Add(this.ChatingWisper);
            this.Controls.Add(this.ChatingBar);
            this.Controls.Add(this.SendMass);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Lobby";
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.UserINFO.ResumeLayout(false);
            this.UserINFO.PerformLayout();
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
        private System.Windows.Forms.Label RoomListIndex;
        private System.Windows.Forms.Button RoomListRight;
        private System.Windows.Forms.Button RoomListLeft;
        private System.Windows.Forms.RichTextBox ChatingLog;
        private System.Windows.Forms.Button Refresh;
        private System.Windows.Forms.GroupBox UserINFO;
        private System.Windows.Forms.Label UserSCORE;
        private System.Windows.Forms.Label UserNICK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button RoomMake;
    }
}