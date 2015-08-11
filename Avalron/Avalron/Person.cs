using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron
{
    class Person
    {
        GroupBox group = new GroupBox();
        PictureBox Picture = new PictureBox();
        Label Nick = new Label();

        public Person(Control.ControlCollection Controls, int i)
        {
            group.Controls.Add(Nick);
            group.Controls.Add(Picture);
            group.Location = new System.Drawing.Point(i*100, 0);
            group.Size = new System.Drawing.Size(113, 100);
            group.TabStop = false;
            group.Text = "그룹";

            Picture.Location = new System.Drawing.Point(12, 17);
            Picture.Size = new System.Drawing.Size(71, 50);
            Picture.TabStop = false;

            Nick.AutoSize = true;
            Nick.Location = new System.Drawing.Point(25, 70);
            Nick.Size = new System.Drawing.Size(38, 12);
            Nick.Text = "닉네임(ID)";

            Controls.Add(group);
            group.ResumeLayout(false);
            group.PerformLayout();
        }
        public void SetInform(string NickName,  string groupName, string PicturePath)
        {
            Nick.Text = NickName;
            group.Text = groupName;
        }
    }
}
