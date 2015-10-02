using System;
using System.Drawing;
using System.Windows.Forms;

namespace Avalron
{
    enum type{Avalron, AAA, BBBB, CCCC,DDD};

    public class Room
    {
        public WATGroupBox Room_box;
        private Label Room_number;
        private PictureBox Room_type_img;
        private Label Room_name;
        private Label Room_persons;

        public string RoomName
        {
            get; set;
        }
        public string RoomNumber
        {
            get; set;
        }
        public string RoomMember
        {
            get; set;
        }
        public string RoomMaxMember
        {
            get; set;
        }
        public string RoomType
        {
            get; set;
        }
        public string RoomPassword
        {
            get; set;
        }
        Point RoomPosition;

        public Room(int i)
        {
            RoomType = "0";
            RoomNumber = "";
            RoomPosition.X = i%2 * 330 + 35;
            RoomPosition.Y = i/2 * 100 + 48;
            
            // 그룹박스 방 할당
            Room_box = new WATGroupBox();
            Room_type_img = new PictureBox();
            Room_name = new Label();
            Room_number = new Label();
            Room_persons = new Label();

            // 
            // Room_type_img
            // 
            Room_type_img.BackColor = System.Drawing.Color.Transparent;
            Room_type_img.Location = new Point(21, 11);
            Room_type_img.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            Room_type_img.Name = "Room_type";
            Room_type_img.Size = new Size(74, 74);
            Room_type_img.Click += new EventHandler(Room_Click);
            Room_type_img.ForeColor = System.Drawing.Color.Transparent;
            // 
            // Room_number
            // 
            Room_number.AutoSize = true;
            Room_number.Location = new Point(105, 17);
            Room_number.Name = "Room_number";
            Room_number.Size = new Size(41, 12);
            Room_number.Text = "방번호";
            Room_number.Click += new EventHandler(Room_Click);
            // 
            // Room_name
            // 
            Room_name.AutoSize = true;
            Room_name.Font = new Font("굴림", 15F);
            Room_name.Location = new Point(105, 42);
            Room_name.Name = "Room_name";
            Room_name.Size = new Size(93, 27);
            Room_name.Text = "방제목";
            Room_name.Click += new EventHandler(Room_Click);
            // 
            // Room_persons
            // 
            Room_persons.AutoSize = true;
            Room_persons.Location = new Point(250, 17);
            Room_persons.Name = "Room_persons";
            Room_persons.Size = new Size(41, 12);
            Room_persons.Text = "인원수";
            Room_persons.Click += new EventHandler(Room_Click);
            // 
            // Room_box
            // 
            Room_box.Controls.Add(Room_persons);
            Room_box.Controls.Add(Room_name);
            Room_box.Controls.Add(Room_number);
            Room_box.Controls.Add(Room_type_img);
            Room_box.BackColor = System.Drawing.Color.Transparent;
            Room_box.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            Room_box.BackgroundImage = global::Avalron.Properties.Resources.대기방배경;
            Room_box.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Room_box.ForeColor = System.Drawing.Color.Transparent;
            Room_box.Location = new Point(RoomPosition.X, RoomPosition.Y);
            Room_box.Name = "Room_box";
            Room_box.Size = new Size(315, 96);
            Room_box.Click += new EventHandler(Room_Click);
        }
        
        public void getRoomInfo()
        {

        }
        
        public void setRoomInfo(string[] roominfo)
        {
            if (roominfo[5].Equals("null")){
                RoomName = "빈방";
                RoomType = "00";
                RoomNumber = "";
                RoomMember = "0";
                RoomMaxMember = "0";
            }
            else
            {
                RoomName = roominfo[0];
                RoomType = roominfo[1];
                RoomPassword = roominfo[2];
                RoomMember = roominfo[3];
                RoomMaxMember = roominfo[4];
                RoomNumber = roominfo[5];
            }

            Room_name.Text = RoomName;
            Room_persons.Text = RoomMember + " / " + RoomMaxMember;
            Room_number.Text = RoomNumber;

            switch (RoomType)
            {
                case "0": // Avalron
                    this.Room_type_img.BackgroundImage = global::Avalron.Properties.Resources.mapicon;
                    break;
                case "1":

                    break;
                case "2":

                    break;
                default:
                    this.Room_type_img.BackgroundImage = global::Avalron.Properties.Resources.빈방;
                    break;
            }
        }

        public void setRoom(Form Lobby) {
            Lobby.Controls.Add(Room_box);
        }
        
        public void Room_Click(object sender, EventArgs e)
        {
            if (RoomNumber.Equals(""))      { return; }
            if (RoomPassword.Equals("")) { Program.tcp.DataSend((int)Lobby.LobbyOpcode.ROOM_JOIN, Program.userInfo.index + TCPClient.delimiter + RoomNumber + TCPClient.delimiter + ""); }
            else
            {  
                Program.lobby.cheakRoomPassword(RoomNumber);
            }
        }
    }

    // 그룹박스 테두리 투명화를 위한 그룹박스 클래스
    public class WATGroupBox : GroupBox
    {
        private Color borderColor;

        public WATGroupBox()
        {
            this.borderColor = System.Drawing.Color.Transparent;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Size tSize = TextRenderer.MeasureText(this.Text, this.Font);
            Rectangle borderRect = e.ClipRectangle;
            borderRect.Y += tSize.Height / 2;
            borderRect.Height -= tSize.Height / 2;
            ControlPaint.DrawBorder(e.Graphics, borderRect, this.borderColor, ButtonBorderStyle.Solid);

            Rectangle textRect = e.ClipRectangle;
            textRect.X += 6;
            textRect.Width = tSize.Width;
            textRect.Height = tSize.Height;
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), textRect);
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), textRect);

        }

    }
}
