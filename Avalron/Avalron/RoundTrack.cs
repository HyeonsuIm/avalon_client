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
        private int CurRound = 0;
        int Max_Count;
        int Successful = 0;
        int Fail = 0;
        GroupBox group = new GroupBox();
        PictureBox[] marker = new PictureBox[5];
        PictureBox circle = new PictureBox();
        PictureBox backGround = new PictureBox();

        public RoundTrack(int Count)                // 투명화 되게 바꾸자.
        {
            Max_Count = Count;
            for (int i = 0; i < 5; i++)
            {
                marker[i] = new PictureBox();
                marker[i].Size = new Size(50, 50);
                marker[i].Location = new System.Drawing.Point(i * 50, 0);
                marker[i].SizeMode = PictureBoxSizeMode.Zoom;
                marker[i].BackColor = Color.Transparent;
                marker[i].Parent = backGround;
            }
            try
            {
                backGround.BackgroundImage = Properties.Resources.Avalron_라운드;
                backGround.BackgroundImageLayout = ImageLayout.Stretch;
                circle.Image = Properties.Resources.Avalon_원정토큰;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            circle.Size = new Size(30, 30);
            circle.Location = new System.Drawing.Point(20, 25);
            circle.SizeMode = PictureBoxSizeMode.Zoom;
            circle.BackColor = Color.Transparent;
            circle.Parent = backGround;
            circle.BringToFront();

            backGround.Size = new Size(250, 50);
            backGround.Location = new System.Drawing.Point(10, 18);
            backGround.SizeMode = PictureBoxSizeMode.StretchImage;
            group.Controls.Add(backGround);
            group.ForeColor = Color.White;
            group.BackColor = Color.Transparent;
            group.Size = new Size(250 + 20, 50 + 22);
            group.Text = "라운드 트랙";
        }

        public int curRound
        {
            get
            {
                return CurRound;
            }
        }

        public int successful
        {
            get
            {
                return Successful;
            }
        }

        public int fail
        {
            get
            {
                return Fail;
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

        // 원정 결과를 저장합니다.
        public void SetResult(int failCnt)
        {
            if (CurRound >= Max_Count)
                throw new Exception("RoundTrack : SetResult : CurRound가 max초과" + CurRound);

            try
            {
                if (circle.InvokeRequired)
                    circle.Invoke(new Delegate(SetResult), new object[] { failCnt });
                else
                {
                    if (failCnt == 0)
                    {
                        marker[CurRound++].Image = Properties.Resources.Avalon_선이김;
                        Successful++;
                    }
                    else
                    {
                        marker[CurRound++].Image = Properties.Resources.Avalon_악이김;
                        Fail++;
                    }
                    // 원정 가야할 곳을 표시하자.
                    circle.Location = new Point(CurRound * 50 + 23, 25);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private delegate void Delegate(int result);
    }
}
