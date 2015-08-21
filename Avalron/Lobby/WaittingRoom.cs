using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron
{
    public partial class WaittingRoom : Form
    {
        WaittingRoomProfile[] wattingRoomProfile = new WaittingRoomProfile[10];
        WaittingRoomChatting wattingRoomChatting; 

        public WaittingRoom()
        {
            InitializeComponent();

            for(int i =0; i < wattingRoomProfile.Length; i++)
            {
                wattingRoomProfile[i] = new WaittingRoomProfile(Controls, i);
            }
            wattingRoomChatting = new WaittingRoomChatting(Controls);
        }
    }
}
