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
        GroupBox group = new GroupBox();
        PictureBox marker = new PictureBox();
        PictureBox backGround = new PictureBox();

        public Track(int Count, String Name = "")
        {
            Max_Count = Count;
            for(int i =0; i < 5; i++)
            {
                
            }
            try {
                marker.Image = Image.FromFile("Avalon/img/Track.jpg");
                backGround.Image = Image.FromFile("Avalon/img/TrackBG.png");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            marker.Size = new Size(50, 50);
            marker.Location = new System.Drawing.Point(5, 15);
            marker.SizeMode = PictureBoxSizeMode.StretchImage;
            marker.BackColor = Color.Transparent;
            marker.Parent = backGround;

            backGround.Size = new Size(200, 50);
            backGround.Location = new System.Drawing.Point(5, 15);
            backGround.SizeMode = PictureBoxSizeMode.StretchImage;
            group.Controls.Add(marker);
            group.Controls.Add(backGround);
            group.Size = new Size(200 + 5, 50 + 15);
            group.Text = Name;
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
            marker.Location = new System.Drawing.Point(Pos * 50 + 5, 15);
            return true;
        }   
    }
}
