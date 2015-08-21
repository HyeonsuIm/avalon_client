using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron
{
    class WaittingRoomProfile
    {
        GroupBox group = new GroupBox();
        PictureBox Picture = new PictureBox();
        Label Nick = new Label();
        PictureBox HostBorder = new PictureBox();
        PictureBox Check = new PictureBox();
        Avalron.AvalronUserInfo avalronUserInfo;
        bool Clicked = false;
         
        public WaittingRoomProfile(Control.ControlCollection Controls, int i)
        {
            Picture.Location = new System.Drawing.Point(12, 17);
            Picture.Size = new System.Drawing.Size(71, 50);
            Picture.TabStop = false;
            Picture.Image = Image.FromFile("Avalron/img/Reject.png");
            Picture.SizeMode = PictureBoxSizeMode.Zoom;
            Picture.Click += new System.EventHandler(group_Click);

            Nick.AutoSize = true;
            Nick.Location = new System.Drawing.Point(25, 70);
            Nick.Size = new System.Drawing.Size(38, 12);
            Nick.Text = "닉네임(ID)";
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
            group.Location = new System.Drawing.Point((i % 5) * 200 + 30, (i / 5 ) * 100 + 10);
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
        
        public void SetHost()
        {
            HostBorder.Image = Image.FromFile("Avalron/img/Leader.png");
        }

        // 표시를 해제합니다.
        public void HostClear()
        {
            HostBorder.Image = null;
        }

        public int index
        {
            get
            {
                return avalronUserInfo.index;
            }
        }

        public bool clicked
        {
            get
            {
                return Clicked;
            }
        }

        private void group_Click(object sender, EventArgs e)
        {
            //if (false == Program.avalron.enableClick)
            //    return;

            if (Clicked)
            {
                Check.Image = null;
                Clicked = false;
                //Avalron.Avalron.ClickCnt--;
                return;
            }
            else
            {
                Check.Image = Image.FromFile("Avalron/img/check.png");
                Clicked = true;
                //Avalron.Avalron.ClickCnt++;
            }
        }
    }
}
