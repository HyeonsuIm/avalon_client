using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron.Avalron
{
    public class RoundTrack
    {
        int Pos = 0;
        int Max_Count;
        GroupBox group = new GroupBox();
        PictureBox[] marker  = new PictureBox[5];
        PictureBox circle = new PictureBox();
        PictureBox backGround = new PictureBox();

        public RoundTrack(int Count)                // 투명화 되게 바꾸자.
        {
            Max_Count = Count;
            for(int i =0; i < 5; i++)
            {
                marker[i] = new PictureBox();
                marker[i].Size = new Size(50, 50);
                marker[i].Location = new System.Drawing.Point(i * 50 + 5, 15);
                marker[i].SizeMode = PictureBoxSizeMode.Zoom;
                marker[i].BackColor = Color.Transparent;
                marker[i].Parent = backGround;
                group.Controls.Add(marker[i]);
            }
            try {
                backGround.Image = Image.FromFile("Avalon/img/TrackBG.png");
                circle.Image = Image.FromFile("Avalon/img/circle.png");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

            circle.Size = new Size(50, 50);
            circle.Location = new System.Drawing.Point(0 * 50 + 5, 15);
            circle.SizeMode = PictureBoxSizeMode.Zoom;
            circle.BackColor = Color.Transparent;
            circle.Parent = backGround;
            group.Controls.Add(circle);

            backGround.Size = new Size(200, 50);
            backGround.Location = new System.Drawing.Point(5, 15);
            backGround.SizeMode = PictureBoxSizeMode.StretchImage;
            group.Controls.Add(backGround);
            group.Size = new Size(200 + 5, 50 + 15);
            group.Text = "라운드 트랙";
        }

        public void SetPosition(Point point)
        {
            group.Location = point;
        }

        public void SetCollection(Control.ControlCollection Collections)
        {
            Collections.Add(group);
        }

        // 원정 결과를 저장합니다.
        public void SetResult(bool result)
        {
            if (Pos >= Max_Count)
                throw new Exception("RoundTrack : SetResult : Pos가 max초과" + Pos);

            if(result)
            {
                marker[Pos++].Image = Image.FromFile("Avalon/img/Win.png");
            }
            else
            {
                marker[Pos++].Image = Image.FromFile("Avalon/img/Lose.png");
            }

            // 원정 가야할 곳을 표시하자.
            circle.Location = new Point(Pos * 50 + 5, 15);
        }
    }
}
