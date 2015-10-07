using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        public static WaitingRoom room;
        public static Command cmd = new Command();
        public static Avalron.Avalron avalron;
        public static Avalron.Server.ClientServer server;
        public static int state = 1;
        static int bState = 99;
        //public static TCP.Logger logger = new TCP.Logger();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new WaitingRoom(new Room(0)));
            //Application.Exit();

            // 중복실행 방지
            bool bnew;
            Mutex mutex = new Mutex(true, "MutexName", out bnew);
            if (bnew)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new login());
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("프로그램이 실행중입니다.");
                Application.Exit();
            }

            if (null != lobbyLoading)
            {
                Application.Run(lobbyLoading);
            }
            if(null != lobby)
            {
                Application.Run(lobby);
            }

            while (state != 0)
            {
                if (state == bState)
                {
                    continue;
                }
                else { bState = state; }

                switch (state)
                {
                    // game to room
                    case 32:
                    // lobby to room
                    case 12:
                        if (null != room)
                        {
                            Application.Run(room);
                        }
                        else { MessageBox.Show("room이 Null입니다. state : " + state); }
                        break;
                    // room to lobby
                    case 21:
                        if (null != lobby)
                        {
                            lobby.ShowDialog();
                        }
                        else { MessageBox.Show("lobby가 Null입니다. state : " + state); }
                        break;
                    // room to game
                    case 23:
                        if (null != avalron)
                        {
                            try {
                                Application.Run(avalron);
                            }
                            catch(InvalidOperationException e)
                            {

                            }
                        }
                        break;
                    // game to lobby
                    case 31:
                        if (null != lobby)
                        {
                            lobby.ShowDialog();
                        }
                        else { MessageBox.Show("lobby가 Null입니다. state : " + state); }
                        break;
                    // exit
                    case 0:
                        break;
                    default:
                        break;
                }
            }

            System.Diagnostics.Process.GetCurrentProcess().Kill();
            Application.Exit();
        }

        public static void tcpAllocation()
        {
            if (null == tcp)
                tcp = new TCPClient();
        }
    }
}
