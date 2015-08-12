using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Avalron
{
    public class TCPClient
    {
        public enum FormNum { LOGIN, LOBBY, GAME, EXIT = 90000 };
        enum LobbyOpcode { CHAT = 100, WISPER, ROOM_REFRESH, USER_REFRESH, ROOM_MAKE };
        enum OpCode { LOGIN_REQUEST = 10, ID_CHECK, NICK_CHECK, EMAIL_CHECK, REGISTER, FIND_ID, FIND_PW };
        static public string delimiter = "\u0001";
        byte[] data = new byte[1024];
        string output;
        string stringData;
        string[] ArrData;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("203.255.3.72"), 9050);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public int recv = 0;
        private bool closed = false;
        Spriter sp;

        public TCPClient()
        {
            Initalize();
        }

        public TCPClient(string address)
        {
            try
            {
                IsValAddress(address);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            ipep = new IPEndPoint(IPAddress.Parse(address), 9050);

            Initalize();
        }
        ~TCPClient()
        {
            if (closed == true)
                return;

            //MessageBox.Show("서버와 연결을 끊습니다.");
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void Initalize()
        {
            Cursor.Current = Cursors.WaitCursor;
            //MessageBox.Show("서버와 연결을 시작합니다.");
            try
            {
                server.SendTimeout = 100000;
                server.ReceiveTimeout = 100000;
                server.Connect(ipep);
            }
            catch (SocketException e)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("서버와 연결하는데 실패하였습니다." + e.Message);
                return;
            }
            recv = server.Receive(data);
            output = Encoding.UTF8.GetString(data, 0, recv);
            Cursor.Current = Cursors.Default;

            closed = false;
        }

        public void IsValAddress(string ipAddress)
        {
            IPAddress address = IPAddress.Parse(ipAddress);
        }

        public void Close()
        {
            Send((int)FormNum.EXIT + "");
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            closed = true;
        }

        public string[] Send(string line)
        {
            if (0 == recv)
            {
                MessageBox.Show("서버와 연결이 되어있지 않습니다.");
                ArrData = new string[1];
                ArrData[0] = "-2";
                return ArrData;
            }

            server.Send(Encoding.UTF8.GetBytes(line));
            data = new byte[1024];
            recv = server.Receive(data);

            if (recv == 0 || recv == -1)
                MessageBox.Show("연결끊겼다" + recv);

            output = Encoding.UTF8.GetString(data, 0, recv);
            if (output == "")
            {
                ArrData = new string[1];
                ArrData[0] = "-1";
                return ArrData;
            }

            sp = new Spriter(output);
            string[] splited = sp.getSplit();

            //return ArrData;
            return splited;
        }

        // 로그인 성공시 일련번호, 실패시 -1
        public int Login(string ID, string PW)
        {
            ArrData = Send((int)FormNum.LOGIN + "" + (int)OpCode.LOGIN_REQUEST + "02" + ID + delimiter + PW);
            //바이트 수 계산용
            Encoding.Default.GetByteCount("asd");

            IsValidOp((int)OpCode.LOGIN_REQUEST);
            int result = Convert.ToInt32(ArrData[0]);

            return result;
        }
       
        protected bool IsValidOp(int opName)
        {
            if (sp.getCnt() != 1 && sp.getForm() != 0 && sp.getOpCode() != opName)
            {
                throw new Exception("예상한 op 코드가 아닙니다.");
                return false;
            }

            return true;
        }

        // 아래로 사용 함수
        public void DataSend(int opcode, string line)
        {
            string message = opcode + line;
            data = Encoding.UTF8.GetBytes(message);
            server.Send(data);
        }

        public string ReciveData()
        {
            data = new byte[1024];
            recv = server.Receive(data);
            stringData = Encoding.UTF8.GetString(data, 0, recv);
            return stringData;
        }

        public void LoadLobby(string id, string ip)
        {
            Program.userInfo = new UserInfo(id, id, ip);
            Program.tcp.DataSend((int)LobbyOpcode.ROOM_REFRESH, "");
        }
    }
}