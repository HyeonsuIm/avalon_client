using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron
{
    class Profile
    {
        GroupBox group = new GroupBox();
        PictureBox Picture = new PictureBox();
        Label Nick = new Label();
        PictureBox TeamBorder = new PictureBox();
        PictureBox LeaderBorder = new PictureBox();
        PictureBox Check = new PictureBox();
        Avalron.AvalronUserInfo avalronUserInfo;
        bool Clicked = false;
        
        public Profile(Control.ControlCollection Controls, int i)
        {
            Picture.Location = new System.Drawing.Point(12, 17);
            Picture.Size = new System.Drawing.Size(71, 50);
            Picture.TabStop = false;
            Picture.Image = Properties.Resources.Reject;
            Picture.SizeMode = PictureBoxSizeMode.Zoom;
            Picture.Click += new System.EventHandler(group_Click);

            Nick.AutoSize = true;
            Nick.Location = new System.Drawing.Point(25, 70);
            Nick.Size = new System.Drawing.Size(38, 12);
            Nick.Text = "닉네임(ID)";
            Nick.BackColor = Color.Transparent;
            Nick.Parent = group;
            Nick.Click += new System.EventHandler(group_Click);

            TeamBorder.Location = new System.Drawing.Point(12, 17);
            TeamBorder.Size = new System.Drawing.Size(71, 50);
            TeamBorder.SizeMode = PictureBoxSizeMode.Zoom;
            TeamBorder.Parent = Picture;
            TeamBorder.BackColor = Color.Transparent;
            TeamBorder.BringToFront();
            TeamBorder.Click += new System.EventHandler(group_Click);

            LeaderBorder.Location = new System.Drawing.Point(12, 17);
            LeaderBorder.Size = new System.Drawing.Size(50, 50);
            LeaderBorder.SizeMode = PictureBoxSizeMode.Zoom;
            LeaderBorder.Parent = Picture;
            LeaderBorder.BackColor = Color.Transparent;
            LeaderBorder.BringToFront();
            LeaderBorder.Click += new System.EventHandler(group_Click);

            Check.Location = new System.Drawing.Point(40, 10);
            Check.Size = new System.Drawing.Size(25, 20);
            Check.SizeMode = PictureBoxSizeMode.Zoom;
            Check.Parent = Picture;
            Check.BackColor = Color.Transparent;
            Check.BringToFront();
            Check.Click += new System.EventHandler(group_Click);

            group.BackColor = Color.Transparent;
            group.ResumeLayout(false);
            group.PerformLayout();
            //group.Controls.Add(Border);     // ㅅㅂ 꺼져
            group.Controls.Add(Nick);
            group.Controls.Add(Picture);
            group.Location = new System.Drawing.Point(i * 100, 30);
            group.Size = new System.Drawing.Size(113, 100);
            group.TabStop = false;
            group.Text = "";
            group.Click += new System.EventHandler(group_Click);

            Controls.Add(group);
        }
        public void SetInform(string NickName,  string groupName, string PicturePath)
        {
            Nick.Text = NickName;
            group.Text = groupName;
        }
        
        // 원정 나갈 사람을 표시합니다.
        public void SetTeam()
        {
            TeamBorder.Image = Properties.Resources.Team;
        }

        public void SetLeader()
        {
            LeaderBorder.Image = Properties.Resources.Leader;
        }

        // 표시를 해제합니다.
        public void TeamClear()
        {
            TeamBorder.Image = null;
        }

        public void LeaderClear()
        {
            LeaderBorder.Image = null;
        }

        public int index
        {
            get
            {
                return avalronUserInfo.index;
            }
        }

        public string nick
        {
            get
            {
                return avalronUserInfo.nick;
            }
        }

        public bool clicked
        {
            get
            {
                return Clicked;
            }
        }

        // 원정대원의 여부입니다.
        public bool team
        {
            get; set;
        }

        private void group_Click(object sender, EventArgs e)
        {
            //if (false == Program.avalron.enableClick)
            //    return;

            if (Clicked)
            {
                Check.Image = null;
                Clicked = false;
                Avalron.Avalron.ClickCnt--;
                return;
            }
            else
            {
                Check.Image = Properties.Resources.check;
                Clicked = true;
                Avalron.Avalron.ClickCnt++;
            }
        }
    }
}
