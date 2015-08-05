using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron.Avalron
{
    public class Track
    {
        int Pos = 0;
        int Max_Count;
        Label k;
        GroupBox group = new GroupBox();
        PictureBox marker = new PictureBox();
        PictureBox backGround = new PictureBox();

        public Track(int Count)
        {
            Max_Count = Count;
            for(int i =0; i < 5; i++)
            {
                
            }
            marker.Image = Image.FromFile("img/Track.jpg");
            marker.SizeMode = PictureBoxSizeMode.AutoSize;
            marker.BackColor = Color.Transparent;
            marker.Parent = backGround;

            backGround.Size = new Size(200, 100);
            backGround.Image = Image.FromFile("img/TrackBG.png");
            backGround.SizeMode = PictureBoxSizeMode.StretchImage;
            group.Controls.Add(marker);
            group.Controls.Add(backGround);
        }

        public void SetPosition(Point point)
        {
            group.Location = point;
        }

        public void SetCollection(Control.ControlCollection Collections)
        {
            Collections.Add(group);
        }


        public bool Next()
        {
            if (Pos >= Max_Count)
                return false;

            Pos++;
            marker.Location = new System.Drawing.Point(Pos * 50 + 0, 0);
            return true;
        }   
    }
}
