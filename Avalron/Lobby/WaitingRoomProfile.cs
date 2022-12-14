using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron
{
    class WaitingRoomProfile
    {
        GroupBox group = new GroupBox();
        PictureBox Picture = new PictureBox();
        Label Nick = new Label();
        PictureBox HostBorder = new PictureBox();
        PictureBox Check = new PictureBox();
        //Avalron.AvalronUserInfo avalronUserInfo;
        bool Clicked = false;
        bool host = false;
        delegate void UserLeaveCallback(); // 유저 떠나기 크로스 스레드
        delegate void ReadyShowCallback(bool ready); // 유저 레디 크로스 스레드

        public bool isHost() { return host; }
        
        public WaitingRoomProfile(Control.ControlCollection Controls, int i)
        {
            Picture.Location = new System.Drawing.Point(22, 17);
            Picture.Size = new System.Drawing.Size(70, 50);
            Picture.TabStop = false;
            //Picture.Image = Properties.Resources.WR_empty;
            Picture.SizeMode = PictureBoxSizeMode.Zoom;
            Picture.Click += new System.EventHandler(group_Click);

            Nick.AutoSize = true;
            Nick.Location = new System.Drawing.Point(0, 75);
            Nick.Size = new System.Drawing.Size(114, 15);
            Nick.AutoSize = false;
            Nick.Text = "";
            Nick.TextAlign = ContentAlignment.MiddleCenter;
            Nick.ForeColor = Color.White;
            Nick.BackColor = Color.Transparent;
            Nick.Parent = group;
            Nick.Click += new System.EventHandler(group_Click);

            HostBorder.Location = new System.Drawing.Point(12, 17);
            HostBorder.Size = new System.Drawing.Size(50, 50);
            HostBorder.SizeMode = PictureBoxSizeMode.Zoom;
            HostBorder.Parent = Picture;
            HostBorder.BackColor = Color.Transparent;
            HostBorder.BringToFront();
            HostBorder.Click += new System.EventHandler(group_Click);

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
            group.Location = new System.Drawing.Point((i % 5) * 150 + 30, (i / 5 ) * 100 + 30);
            group.Size = new System.Drawing.Size(114, 100);
            group.TabStop = false;
            group.Text = "";
            group.Click += new System.EventHandler(group_Click);

            index = -1;

            Controls.Add(group);
        }

        private delegate void SetInformDelegate(string text, int index, string PicturePath);

        // 폼에 Profile을 세팅
        public void SetInform(string NickName, int index, string PicturePath)
        {
            //avalronUserInfo = new Avalron.AvalronUserInfo(NickName, index);
            this.index = index;

            try
            {
                if(Nick.InvokeRequired)
                {
                    Nick.Invoke(new SetInformDelegate(SetInform), new object[] { NickName, index, PicturePath });
                }
                else
                {
                    Nick.Text = NickName;
                    if (index > 0)
                    {
                        Picture.Image = Properties.Resources.WR_사용자;
                    }
                    if (host) { Check.Image = Properties.Resources.icon; }
                }
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        // 유저 나가기 크로스 스레드
        public void UserLeave()
        {
            if (group.InvokeRequired)
            {
                UserLeaveCallback userLeaveCallback = new UserLeaveCallback(UserLeave);
                group.Invoke(userLeaveCallback);
            }
            else
            {
                SeatOpen();
            }
        }

        // 유저 레디 보여주기 크로스 스레드
        public void ReadyShow(bool ready)
        {
            if (Check.InvokeRequired)
            {
                ReadyShowCallback readyShowCallback = new ReadyShowCallback(ReadyShow);
                Check.Invoke(readyShowCallback, new object[] { ready });
            }
            else
            {
                if(ready)
                    Picture.Image = Properties.Resources.WR_사용자_준비상태;
                //Check.Image = Properties.Resources.icon;
                else
                    Picture.Image = Properties.Resources.WR_사용자;
                //Check.Image = null;
            }
        }

        // 호스트 지정
        public void SetHost()
        {
            //HostBorder.Image = Image.FromFile("Avalron/img/Leader.png");
            host = true;
        }

        // 표시를 해제합니다.
        public void HostClear()
        {
            HostBorder.Image = null;
        }

        // 현 객체 유저의 index
        public int index;
        //{
        //    get
        //    {
        //        //return avalronUserInfo.index;
        //        return index;
        //    }
        //    set
        //    {
        //        //if (null == avalronUserInfo)
        //        //    avalronUserInfo = new Avalron.AvalronUserInfo("set으로 할당", value);
        //        //else
        //        //    avalronUserInfo.index = value;
        //    }
        //}

        // Clicked변수 리턴하는 함수
        public bool clicked
        {
            get
            {
                return Clicked;
            }
        }

        // 객체 클릭시
        private void group_Click(object sender, EventArgs e)
        {
            if (!host) { return; }
            //if (false == Program.avalron.enableClick)
            //    return;
            
            if (index < 0) // 유저가 없을 때
            {
                if (Clicked) // 닫힌 상태일 때
                {
                    SeatOpen();
                    Check.Image = null;
                    Clicked = false;
                    //Avalron.Avalron.ClickCnt--;
                    return;
                }
                else // 열린 상태일 때
                {
                    SeatClose();
                    Clicked = true;
                    //Check.Image = Properties.Resources.icon;
                    //Avalron.Avalron.ClickCnt++;
                }
            }
            else // 유저가 있을 때
            {
                //Program.tcp.DataSend((int)TCPClient.RoomOpCode.Start, Program.userInfo.index.ToString()); // 강제 퇴장
            }
        }

        public void SeatClose()
        {
            Nick.Text = "닫힘";
            index = -2;
        }

        public void SeatOpen()
        {
            Picture.Image = null;
            Nick.Text = "";
            index = -1;
        }
    }
}
