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
        PictureBox Border = new PictureBox();

        public Profile(Control.ControlCollection Controls, int i)
        {
            Picture.Location = new System.Drawing.Point(12, 17);
            Picture.Size = new System.Drawing.Size(71, 50);
            Picture.TabStop = false;
            Picture.Image = Image.FromFile("Avalon/img/Reject.png");
            Picture.SizeMode = PictureBoxSizeMode.Zoom;

            Nick.AutoSize = true;
            Nick.Location = new System.Drawing.Point(25, 70);
            Nick.Size = new System.Drawing.Size(38, 12);
            Nick.Text = "닉네임(ID)";
            Nick.BackColor = Color.Transparent;
            Nick.Parent = group;

            Border.Location = new System.Drawing.Point(12, 17);
            Border.Size = new System.Drawing.Size(71, 50);
            Border.SizeMode = PictureBoxSizeMode.Zoom;
            Border.Parent = Picture;
            Border.BackColor = Color.Transparent;
            Border.BringToFront();

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
            Border.Image = Image.FromFile("Avalon/img/Team.png");
        }

        // 표시를 해제합니다.
        public void Clear()
        {
            Border.Image = null;
        }
    }
}
