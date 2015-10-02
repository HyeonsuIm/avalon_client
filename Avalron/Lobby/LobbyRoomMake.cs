using System;
using System.Windows.Forms;

namespace Avalron
{
    public partial class LobbyRoomMake : Form
    {
        // 변수선언
        enum LobbyOpcode { CHAT = 100, WISPER, ROOM_REFRESH, USER_REFRESH, ROOM_MAKE};
        int roomType;
        string roomPass;
        string maxMember;
        string[] TypeData = { "Avalron", "토마토", "포도" };
        bool IsModify = false;

        public LobbyRoomMake(TCPClient tcp)
        {
            InitializeComponent();
            Program.tcp = tcp;

            roomType = 0;

            // 각 콤보박스에 데이타를 초기화
            Room_Make_Type.Items.AddRange(TypeData);

            // 처음 선택값 지정. 첫째 아이템 선택
            Room_Make_Type.SelectedIndex = 0;

        }

        private void LobbyRoomMake_Shown(object sender, EventArgs e)
        {
            Room_Make_Name.Focus();
        }

        // 비밀번호 체크박스
        private void Room_Make_PassBox_CheckedChanged(object sender, EventArgs e)
        {
            if(Room_Make_PassBox.Checked == false)
            {
                Room_Make_Pass.ReadOnly = true;
            }else { Room_Make_Pass.ReadOnly = false; }
        }

        // 닫기
        private void LRM_Close_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        // 방만들기 버튼
        private void Room_Make_Click(object sender, EventArgs e)
        {
            roomPass = Room_Make_Pass.Text;
            maxMember = Room_Make_MaxMember.Text;
            if (Room_Make_PassBox.Checked == false)
            {
                roomPass = "";
            }
            else
            {
                if (Room_Make_Pass.Text == "") { return; }
            }
            Program.tcp.DataSend((int)LobbyOpcode.ROOM_MAKE, roomType.ToString() + '\u0001' + Room_Make_Name.Text + '\u0001' + roomPass + '\u0001' + "asdf" + '\u0001' + maxMember);
            MessageBox.Show(Room_Make_Name.Text + " @ " + roomPass + " @ " + roomType + " @ " + maxMember);
            Close();
            Program.lobby.Close();
            Room room = new Room(0);
            room.RoomName = Room_Make_Name.Text;
            room.RoomType = roomType.ToString();
            room.RoomPassword = roomPass;
            room.RoomMaxMember = maxMember;

            //Program.lobby.reciveDataThread.Wait();

            Program.room = new WaitingRoom(room);
            Program.state = 12;
        }

        // 방 수정 버튼 클릭
        private void Room_Make_Modify_Click(object sender, EventArgs e)
        {
            roomPass = Room_Make_Pass.Text;
            maxMember = Room_Make_MaxMember.Text;
            if (Room_Make_PassBox.Checked == false)
            {
                roomPass = "";
            }
            Program.tcp.DataSend((int)WaitingRoom.OpCode.RoomModify, roomType.ToString() + '\u0001' + Room_Make_Name.Text + '\u0001' + roomPass + '\u0001' + "11" + '\u0001' + maxMember);
            MessageBox.Show(Room_Make_Name.Text + " @ " + roomPass + " @ " + roomType + " @ " + maxMember);
            Close();
        }

        private void Room_Make_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = Room_Make_Type.Text;
            switch (type)
            {
                case "Avalron":
                    roomType = 0;
                    Room_Make_MaxMember.Items.Clear();
                    setMember(10, 4);
                    Room_Make_MaxMember.SelectedIndex = 0;
                    break;
                case "토마토":
                    roomType = 1;
                    Room_Make_MaxMember.Items.Clear();
                    setMember(5, 2);
                    Room_Make_MaxMember.SelectedIndex = 0;
                    break;
                case "포도":
                    roomType = 2;
                    Room_Make_MaxMember.Items.Clear();
                    setMember(8, 3);
                    Room_Make_MaxMember.SelectedIndex = 0;
                    break;
                default:
                    break;
            }
        }

        private void setMember(int maxMem, int minMem)
        {
            string item;
            while (maxMem != minMem)
            {
                item = maxMem.ToString();
                Room_Make_MaxMember.Items.Add(item);
                maxMem--;
            }
        }

        public void Modify(Room room)
        {
            Room_Make_Name.Text = room.RoomName;
            Room_Make_Pass.Text = room.RoomPassword;
            if(null != Room_Make_Pass.Text)
                Room_Make_PassBox.Enabled = true;

            Room_Make_Type.SelectedIndex = Convert.ToInt32(room.RoomType);
            Room_Make_MaxMember.SelectedItem = room.RoomMaxMember;

            Room_Make.Text = "방 수정";
            this.Room_Make.Click += new System.EventHandler(this.Room_Make_Modify_Click);
            this.Room_Make.Click -= new System.EventHandler(this.Room_Make_Click);
        }

        public void Modify(AvalonServer.RoomInfo roomInfo)
        {
            string[] infoStr = roomInfo.getRoomInfo();

            Room_Make_Name.Text = infoStr[0];
            Room_Make_Pass.Text = infoStr[2];
            if (null != Room_Make_Pass.Text)
                Room_Make_PassBox.Enabled = true;

            Room_Make_Type.SelectedIndex = Convert.ToInt32(infoStr[1]);
            Room_Make_MaxMember.SelectedItem = infoStr[4];

            Room_Make.Text = "방 수정";
            this.Room_Make.Click += new System.EventHandler(this.Room_Make_Modify_Click);
            this.Room_Make.Click -= new System.EventHandler(this.Room_Make_Click);
        }

        private void Room_Make_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // 입장 버튼누르기
                Room_Make_Click(sender, e);

                // 엔터키 소리제거
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Room_Make_Pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // 입장 버튼누르기
                Room_Make_Click(sender, e);

                // 엔터키 소리제거
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
} 