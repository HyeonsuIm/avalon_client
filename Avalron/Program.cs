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
        public static UserInfo userInfo = new UserInfo("17", 1);
        public static LobbyLoading lobbyLoading;
        public static Lobby lobby;
        public static Command cmd = new Command();
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LobbyLoading(userInfo));
            Application.Run(lobby);
            Application.Exit();
        }

        public static void tcpAllocation()
        {
            if (null == tcp)
                tcp = new TCPClient();
        }
    }
}
