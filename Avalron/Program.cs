using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        public static TCPClient tcp;
        public static UserInfo userInfo = new UserInfo("admin2Nick", 21);
        public static LobbyLoading lobbyLoading;
        public static Lobby lobby;
        public static Command cmd = new Command();
        public static Avalron.Avalron avalron; 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new WaitingRoom(new Room(0)));
            //Application.Exit();

            Application.Run(new login());
            if (null != lobbyLoading)
            {
                Application.Run(lobbyLoading);
            }
            if(null != lobby)
            {
                Application.Run(lobby);
                avalron = new Avalron.Avalron(6);
            }
            if (avalron != null)
                Application.Run(avalron);

            Application.Exit();
        }

        public static void tcpAllocation()
        {
            if (null == tcp)
                tcp = new TCPClient();
        }
    }
}
