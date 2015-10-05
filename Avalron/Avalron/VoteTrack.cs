using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron.Avalron
{
    public class VoteTrack
    {
        int Max_Count;
        int Rejected = 0;
        GroupBox group = new GroupBox();
        PictureBox marker = new PictureBox();
        PictureBox backGround = new PictureBox();

        public VoteTrack(int Count)
        {
            Max_Count = Count;
            try
            {
                marker.Image = Properties.Resources.Avalon_투표토큰;
                backGround.BackgroundImage = Properties.Resources.Avalron_투표;
                backGround.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            marker.Size = new Size(50, 50);
            marker.Location = new Point(0, 15);
            marker.SizeMode = PictureBoxSizeMode.StretchImage;
            marker.BackColor = Color.Transparent;
            //marker.Parent = backGround;

            backGround.Size = new Size(250, 50);
            backGround.SizeMode = PictureBoxSizeMode.StretchImage;
            backGround.Location = new Point(0, 15);
            group.Controls.Add(marker);
            group.Controls.Add(backGround);
            group.Text = "투표 트랙";
            group.Size = new Size(250 + 5, 50 + 15);
        }

        public int rejected
        {
            get
            {
                return Rejected;
            }
        }
        public void SetPosition(Point point)
        {
            group.Location = point;
        }

        public void SetCollection(Control.ControlCollection Collections)
        {
            Collections.Add(group);
        }

        // 5번 연속 부결시 false 반환, 아닐시 true
        public bool Next()
        {
            if (Rejected >= Max_Count)
                return false;

            try
            {
                if (marker.InvokeRequired)
                    marker.Invoke(new Delegate(Next), new object[] { });
                else
                {
                    Rejected++;
                    marker.Location = new System.Drawing.Point(Rejected * 50 + 0, 15);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return true;
        }

        private delegate bool Delegate();

        public void Clear()
        {
            Rejected = 0;
            marker.Location = new Point(Rejected * 50 + 0, 15);
        }
    }
}