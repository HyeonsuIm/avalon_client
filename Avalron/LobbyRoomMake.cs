using System;
using System.Windows.Forms;

namespace Avalron
{
    public partial class LobbyRoomMake : Form
    {
        TCPClient TCP;
        Commend comm = new Commend();
        string roomType;
        string roomPass;

        public LobbyRoomMake(TCPClient tcp)
        {
            InitializeComponent();
            TCP = tcp;
            Room_Make_Type_Avalron.Checked = true;
            roomType = "01";
        }

        private void Room_Make_PassBox_CheckedChanged(object sender, EventArgs e)
        {
            if(Room_Make_PassBox.Checked == false)
            {
                Room_Make_Pass.ReadOnly = true;
            }else { Room_Make_Pass.ReadOnly = false; }
        }

        private void LRM_Close_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void Room_Make_Click(object sender, EventArgs e)
        {
            roomPass = Room_Make_Pass.Text;
            if (Room_Make_PassBox.Checked == false)
            {
                roomPass = "";
            }
            TCP.DataSend(comm.order("roomMake"), Room_Make_Name.Text + comm.delimiter + roomPass + comm.delimiter + roomType);
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
