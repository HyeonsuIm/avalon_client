using System;
using System.Windows.Forms;

namespace Avalron
{
    public partial class LobbyRoomMake : Form
    {
        // 변수선언
        enum LobbyOpcode { CHAT = 200, WISPER, ROOM_REFRESH, USER_REFRESH, ROOM_MAKE };
        string roomType;
        string roomPass;

        public LobbyRoomMake(TCPClient tcp)
        {
            InitializeComponent();
            Program.tcp = tcp;
            Room_Make_Type_Avalron.Checked = true;
            roomType = "01";
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
            if (Room_Make_PassBox.Checked == false)
            {
                roomPass = "";
            }
            Program.tcp.DataSend((int)LobbyOpcode.ROOM_REFRESH, roomType + '\u0001' + Room_Make_Name.Text + '\u0001' + roomPass + '\u0001' + "asdf" + '\u0001' + 10); // 10은 최대인원수(수정중
            MessageBox.Show(Room_Make_Name.Text + roomPass + roomType);
            Close();
        }

        private void Room_Make_Type_Avalron_Click(object sender, EventArgs e)
        {
            roomType = "01";
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            roomType = "02";
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            roomType = "03";
        }
    }
}
