using System;
using System.Drawing;
using System.Windows.Forms;

namespace Avalron
{
    enum type{AVALON, AAA, BBBB, CCCC,DDD};
    class Room
    {
        public GroupBox Room_box;
        private Label Room_number;
        private PictureBox Room_type_img;
        private Label Room_name;
        private Label Room_persons;

        string RoomName;
        string RoomNumber;
        string RoomPersons;
        string RoomType;
        Bitmap RoomTypeImg; 
        Point RoomPosition;

        public Room(int i)
        {
            RoomType = "0";
            RoomPosition.X = i%2 * 277 + 35;
            RoomPosition.Y = i/2 * 100 + 55;
            
            // 그룹박스 방 할당
            Room_box = new GroupBox();
            Room_type_img = new PictureBox();
            Room_name = new Label();
            Room_number = new Label();
            Room_persons = new Label();

            // 
            // Room_type_img
            // 
            Room_type_img.Location = new Point(6, 17);
            Room_type_img.Name = "Room_type";
            Room_type_img.Size = new Size(74, 63);
            // 
            // Room_number
            // 
            Room_number.AutoSize = true;
            Room_number.Location = new Point(96, 17);
            Room_number.Name = "Room_number";
            Room_number.Size = new Size(41, 12);
            Room_number.Text = "방번호";
            // 
            // Room_name
            // 
            Room_name.AutoSize = true;
            Room_name.Font = new Font("굴림", 15F);
            Room_name.Location = new Point(93, 42);
            Room_name.Name = "Room_name";
            Room_name.Size = new Size(93, 27);
            Room_name.Text = "방제목";
            // 
            // Room_persons
            // 
            Room_persons.AutoSize = true;
            Room_persons.Location = new Point(211, 17);
            Room_persons.Name = "Room_persons";
            Room_persons.Size = new Size(41, 12);
            Room_persons.Text = "인원수";
            // 
            // Room_box
            // 
            Room_box.Controls.Add(Room_persons);
            Room_box.Controls.Add(Room_name);
            Room_box.Controls.Add(Room_number);
            Room_box.Controls.Add(Room_type_img);
            Room_box.Location = new Point(RoomPosition.X, RoomPosition.Y);
            Room_box.Name = "Room_box";
            Room_box.Size = new Size(267, 93);
        }
        
        public void getRoomInfo()
        {

        }

        public void setRoomInfo(string type, string num, string name, string person)
        {
            RoomType = type;
            Room_number.Text = num;
            Room_name.Text = name;
            Room_persons.Text = person;

            switch (RoomType)
            {
                case "01":
                    Room_type_img.Image = Image.FromFile(@"../../Resources/illust_006.jpg");
                    break;
                case "02":

                    break;
                case "03":

                    break;
            }
        }

        public void setRoom(Form Lobby) {
            Lobby.Controls.Add(Room_box);
        }
    }
}
