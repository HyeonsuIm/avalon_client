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
        PictureBox Evil = new PictureBox();
        PictureBox PercivalOrMorgana = new PictureBox();
        PictureBox Vote = new PictureBox();
        Label Nick = new Label();
        PictureBox TeamBorder = new PictureBox();
        PictureBox LeaderBorder = new PictureBox();
        PictureBox Check = new PictureBox();
        Avalron.AvalronUserInfo avalronUserInfo;
        public bool Clicked
        {
            get;set;
        }
        int arrayIndex;          // 프로필의 배열 인덱스 입니다.
        
        public Profile(Control.ControlCollection Controls, int i, string nick, int index)
        {
            Clicked = false;
            arrayIndex = i;

            Evil.Location = new Point(30, 0);
            Evil.Size = new Size(20, 20);
            Evil.TabStop = false;
            Evil.Image = null;
            Evil.SizeMode = PictureBoxSizeMode.Zoom;
            Evil.Click += new System.EventHandler(group_Click);

            PercivalOrMorgana.Location = new Point(30, 20);
            PercivalOrMorgana.Size = new Size(15, 15);
            PercivalOrMorgana.SizeMode = PictureBoxSizeMode.Zoom;
            PercivalOrMorgana.Click += new System.EventHandler(group_Click);

            Picture.Location = new System.Drawing.Point(12, 17);
            Picture.Size = new System.Drawing.Size(71, 50);
            Picture.TabStop = false;
            Picture.Image = Properties.Resources.Reject;
            Picture.SizeMode = PictureBoxSizeMode.Zoom;
            Picture.Click += new System.EventHandler(group_Click);

            Vote.Location = new Point(0, 30);
            Vote.Size = new Size(10, 30);
            Vote.Click += new EventHandler(group_Click);

            Nick.AutoSize = true;
            Nick.Location = new System.Drawing.Point(25, 70);
            Nick.Size = new System.Drawing.Size(38, 12);
            Nick.Text = nick;
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
            group.Controls.Add(Evil);
            group.Controls.Add(PercivalOrMorgana);
            group.Location = new System.Drawing.Point(i * 100, 30);
            group.Size = new System.Drawing.Size(113, 100);
            group.TabStop = false;
            group.Text = "";
            group.Click += new System.EventHandler(group_Click);

            avalronUserInfo = new Avalron.AvalronUserInfo(nick, index);

            Controls.Add(group);
        }
        public void SetInformImg(string groupName, string PicturePath)
        {
            group.Text = groupName;
        }
        
        // 원정 나갈 사람을 표시합니다.
        public void SetTeam()
        {
            TeamBorder.Image = Properties.Resources.Team;
            team = true;
        }

        public void SetLeader()
        {
            LeaderBorder.Image = Properties.Resources.Leader;
        }

        public void setEvil()
        {
            Evil.Image = Properties.Resources.evil;
        }

        public void setPercivalOrMorgana()
        {
            PercivalOrMorgana.Image = Properties.Resources.question;
        }

        public void voteShow(bool vote)
        {
            if (vote)
                this.Vote.Image = Properties.Resources.Approve;
            else
                this.Vote.Image = Properties.Resources.Reject;
        }

        // 표시를 해제합니다.
        public void TeamClear()
        {
            TeamBorder.Image = null;
            team = false;
        }

        public void LeaderClear()
        {
            LeaderBorder.Image = null;
        }

        public void EvilClear()
        {
            Evil.Image = null;
        }

        public void PercivalOrMorganaClear()
        {
            PercivalOrMorgana.Image = null;
        }

        public void voteClear()
        {
            Vote.Image = null;
        }

        // 유저의 일련번호입니다.
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

        // 원정대원의 여부입니다.
        public bool team
        {
            get; set;
        }

        private void group_Click(object sender, EventArgs e)
        {
            if (false == Program.avalron.enableClick)
                return;


            if (Clicked)
            {
                Program.avalron.gameClient.DataSend((int)Avalron.AvalronClient.TeamBuildingOpCode.TeamDeSelect, arrayIndex.ToString());

                Check.Image = null;
                Clicked = false;
                return;
            }
            else
            {
                if (Program.avalron.teamCnt >= Program.avalron.teamMaxNum)
                    return;

                Program.avalron.gameClient.DataSend((int)Avalron.AvalronClient.TeamBuildingOpCode.TeamSelect, arrayIndex.ToString());

                Check.Image = Properties.Resources.check;
                Clicked = true;
            }
        }
    }
}
