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
        //PictureBox MerlinOrMorgana = new PictureBox();
        PictureBox LadyOfTheLake = new PictureBox();
        PictureBox Vote = new PictureBox();
        Label Nick = new Label();
        PictureBox TeamBorder = new PictureBox();
        PictureBox LeaderBorder = new PictureBox();
        PictureBox Check = new PictureBox();
        Avalron.AvalronUserInfo avalronUserInfo;
        public bool Clicked
        {
            get; set;
        }
        int arrayIndex;          // 프로필의 배열 인덱스 입니다.

        public Profile(Control.ControlCollection Controls, int i, string nick, int index)
        {
            Clicked = false;
            arrayIndex = i;

            /// <summary>
            ///  Evil
            /// </summary>
            Evil.Location = new System.Drawing.Point(0, 5);
            Evil.Size = new System.Drawing.Size(130, 30);
            Evil.TabStop = false;
            Evil.SizeMode = PictureBoxSizeMode.Zoom;
            Evil.BackgroundImage = Properties.Resources.Avalon_NickBG;
            Evil.BackgroundImageLayout = ImageLayout.Stretch;
            Evil.Parent = Picture;
            Evil.Click += new System.EventHandler(this.group_Click);

            /// <summary>
            /// Picture
            /// </summary>
            Picture.Location = new System.Drawing.Point(0, 0);
            Picture.Size = new System.Drawing.Size(130, 200);
            Picture.TabStop = false;
            Picture.BackgroundImage = Properties.Resources.Avalon_User;
            Picture.BackgroundImageLayout = ImageLayout.Stretch;
            Picture.SizeMode = PictureBoxSizeMode.Zoom;
            Picture.Click += new System.EventHandler(this.group_Click);

            /// <summary>
            /// Vote
            /// </summary>
            Vote.Location = new Point(5, 150);
            Vote.Size = new Size(30, 50);
            Vote.BackgroundImage = null;
            Vote.BackgroundImageLayout = ImageLayout.Stretch;
            Vote.Parent = Picture;
            Vote.Click += new System.EventHandler(this.group_Click);

            /// <summary>
            /// Nick
            /// </summary>
            Nick.AutoSize = true;
            Nick.Location = new System.Drawing.Point(0, 0);
            Nick.Size = new System.Drawing.Size(130, 30);
            Nick.Text = nick;
            Nick.BackColor = Color.Transparent;
            Nick.ForeColor = Color.White;
            Nick.Font = new Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            Nick.FlatStyle = FlatStyle.Flat;
            Nick.BringToFront();
            Nick.AutoSize = false;
            Nick.TextAlign = ContentAlignment.MiddleCenter;
            Nick.Parent = Evil;
            Nick.Click += new System.EventHandler(this.group_Click);

            /// <summary>
            /// TeamBorder
            /// </summary>
            TeamBorder.Location = new System.Drawing.Point(45, 150);
            TeamBorder.Size = new System.Drawing.Size(40, 50);
            TeamBorder.SizeMode = PictureBoxSizeMode.Zoom;
            TeamBorder.Parent = Picture;
            TeamBorder.BackColor = Color.Transparent;
            TeamBorder.BringToFront();
            //TeamBorder.BackgroundImage = Properties.Resources.Avalon_대원;
            TeamBorder.BackgroundImageLayout = ImageLayout.Stretch;
            TeamBorder.Click += new System.EventHandler(this.group_Click);

            /// <summary>
            /// LeaderBorder
            /// </summary>
            LeaderBorder.Location = new System.Drawing.Point(20, 25);
            LeaderBorder.Size = new System.Drawing.Size(90, 90);
            LeaderBorder.SizeMode = PictureBoxSizeMode.Zoom;
            LeaderBorder.Parent = Picture;
            LeaderBorder.BackColor = Color.Transparent;
            //LeaderBorder.BackgroundImage = Properties.Resources.Avalon_원정대장;
            LeaderBorder.BackgroundImageLayout = ImageLayout.Stretch;
            LeaderBorder.BorderStyle = BorderStyle.None;
            LeaderBorder.Click += new System.EventHandler(this.group_Click);

            /// <summary>
            /// Check
            /// </summary>
            Check.Location = new System.Drawing.Point(95, 150);
            Check.Size = new System.Drawing.Size(30, 50);
            Check.SizeMode = PictureBoxSizeMode.Zoom;
            Check.Parent = Picture;
            Check.BackColor = Color.Transparent;
            Check.BringToFront();
            //Check.BackgroundImage = Properties.Resources.Avalon_river;
            Check.BackgroundImageLayout = ImageLayout.Stretch;
            Check.Click += new System.EventHandler(this.group_Click);

            /// <summary>
            /// group
            /// </summary>
            group.BackColor = Color.Transparent;
            group.ResumeLayout(false);
            group.PerformLayout();
            //group.Controls.Add(Border);     // ㅅㅂ 꺼져
            group.Controls.Add(Picture);
            group.Location = new System.Drawing.Point(arrayIndex * 130, 50);
            group.Size = new System.Drawing.Size(130, 200);
            group.TabStop = false;
            group.Text = "";
            group.Click += new System.EventHandler(this.group_Click);

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
            TeamBorder.BackgroundImage = Properties.Resources.Avalon_대원;
            team = true;
        }

        public void SetLeader()
        {
            LeaderBorder.BackgroundImage = Properties.Resources.Avalon_원정대장;
        }

        public void setEvil()
        {
            Evil.BackgroundImage = Properties.Resources.Avalon_NickBG_Avility;
        }

        public void setLadyOfTheLake()
        {

        }

        public void voteShow(bool vote)
        {
            if (vote)
                this.Vote.BackgroundImage = Properties.Resources.Avalon_Approuve;
            else
                this.Vote.BackgroundImage = Properties.Resources.Avalon_reject;
        }

        public void assasination(){
            LeaderBorder.BackgroundImage = Properties.Resources.Avalon_assasination;      // 멀린인가? 암살하자!!
        }

        // 표시를 해제합니다.
        public void TeamClear()
        {
            TeamBorder.BackgroundImage = null;
            team = false;
        }

        public void LeaderClear()
        {
            LeaderBorder.BackgroundImage = null;
        }

        public void EvilClear()
        {
            Evil.BackgroundImage = null;
        }

        public void voteClear()
        {
            Vote.Image = null;
        }

        public void clickClear()
        {
            Clicked = false;
            Check.BackgroundImage = null;
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
            if (false == Program.avalron.enableClick) {
                MessageBox.Show("원정대장이 아닙니다.");
                return;
            }

            switch (Program.avalron.phaseState)
            {
                case Avalron.Avalron.PhaseState.TeamBuilding:
                    if (Clicked)
                    {
                        Program.avalron.gameClient.DataSend((int)Avalron.AvalronClient.TeamBuildingOpCode.TeamDeSelect, arrayIndex.ToString());

                        TeamBorder.BackgroundImage = null;
                        Clicked = false;
                        return;
                    }
                    else
                    {
                        if (Program.avalron.teamCnt >= Program.avalron.teamMaxNum)
                            return;

                        Program.avalron.gameClient.DataSend((int)Avalron.AvalronClient.TeamBuildingOpCode.TeamSelect, arrayIndex.ToString());

                        TeamBorder.BackgroundImage = Properties.Resources.Avalon_대원;
                        Clicked = true;
                    }
                    break;

                case Avalron.Avalron.PhaseState.MyLadyOfTheLake:
                    {
                        Program.avalron.gameClient.DataSend((int)Avalron.AvalronClient.EtcSpecialOpCode.LadyOfTheLakeResult, arrayIndex.ToString());
                        MessageBox.Show(arrayIndex + "호수의 여인을 이사람에게 보냄");

                        Program.avalron.ladyOfTheLakeIndex = arrayIndex;

                        Check.BackgroundImage = Properties.Resources.Avalon_river; // 임시 입니다. 호수의 여인 토큰이 와야 합니다.
                    }
                    break;
                case Avalron.Avalron.PhaseState.MyMerlinAssassinate:
                    {
                        Program.avalron.gameClient.DataSend((int)Avalron.AvalronClient.EtcSpecialOpCode.MerlinAssassinate, arrayIndex.ToString());
                    }
                    break;
            }
        }
    }
}
