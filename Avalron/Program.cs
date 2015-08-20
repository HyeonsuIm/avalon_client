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
        //public static TCPClient tcp = new TCPClient();
        public static TCPClient tcp;
        public static UserInfo userInfo;
        public static Lobby lobby;
        public static Command cmd = new Command();
        public static Avalron.Avalron avalron;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Lobby("guest","1.1.1.1"));
            Application.Run(new login());
            if(avalron != null)
                Application.Run(avalron);

            Application.Exit();
        }
    }
}
