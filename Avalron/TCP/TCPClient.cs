using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Avalron
{
    public class TCPClient
    {
        public enum FormNum : int { LOGIN, LOBBY, ROOM, AVALRON_GAME, EXIT = 90000 };
        public enum LobbyOpcode { CHAT = 100, WISPER, ROOM_REFRESH, USER_REFRESH, ROOM_MAKE };
        enum OpCode : int { LOGIN_REQUEST = 10, ID_CHECK, NICK_CHECK, EMAIL_CHECK, REGISTER, FIND_ID, FIND_PW };
        public enum RoomOpCode : int { Chat = 200, Wisper = 804, Connect = 210, DisConnect, SeatClose, Modify, Delete, Start, Ready, };

        static public string delimiter = "\u0001";
        int sent;
        string output;
        string stringData;
        string[] ArrData;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("203.255.3.92"), 9050);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public int recv = 0;
        private bool closed = false;
        Spriter sp;
        static bool synchronized = false; // true면 실행 중

        public TCPClient()
        {
            Initalize();
        }

        public bool Connected
        {
            get
            {
                return server.Connected;
            }
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
            SendVarData(Encoding.UTF8.GetBytes(((int)FormNum.EXIT).ToString()));
            server.Close();
        }

        private void Initalize()
        {
            Cursor.Current = Cursors.WaitCursor;
            //MessageBox.Show("서버와 연결을 시작합니다.");
            try
            {
                server.SendTimeout = 10000;
                server.ReceiveTimeout = 10000;
                server.Connect(ipep);
            }
            catch (SocketException e)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("서버와 연결하는데 실패하였습니다." + e.Message);
                return;
            }
            byte[] data;
            ReceiveVarData(out data);
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
            //server.Shutdown(SocketShutdown.Both);
            SendVarData(Encoding.UTF8.GetBytes(((int)FormNum.EXIT).ToString()));
            server.Close();

            closed = true;
        }

        // 가장 기본적인 송신부입니다.
        protected int SendVarData(byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;

            byte[] datasize = new byte[4];
            datasize = BitConverter.GetBytes(size);
            sent = server.Send(datasize);

            while (total < size)
            {
                sent = server.Send(data, total, dataleft, SocketFlags.None);
                total += sent;
                dataleft -= sent;
            }
            // 디버그용도입니다.
            System.Diagnostics.Debug.WriteLine("send : " + Encoding.UTF8.GetString(data).Replace(delimiter[0], 'ㆎ'));
            return total;
        }

        // 가장 기본적인 수신부입니다.
        protected int ReceiveVarData(out byte[] data)
        {
            int total = 0;
            //int recv;
            byte[] datasize = new byte[4];

            recv = server.Receive(datasize, 0, 4, 0);
            int size = BitConverter.ToInt32(datasize, 0);
            int dataleft = size;
            data = new byte[size];
            while (total < size)
            {
                recv = server.Receive(data, total, dataleft, 0);
                if (recv == 0)
                {
                    data = Encoding.UTF8.GetBytes("exit");
                    throw new Exception("수신된 길이만큼을 받지 못하였습니다." + data);
                    break;
                }
                total += recv;
                dataleft -= recv;
            }
            // 디버그 용도입니다.
            System.Diagnostics.Debug.WriteLine("recv : " + Encoding.UTF8.GetString(data).Replace(delimiter[0], '+'));
            return total;
        }

        // tcp를 송신후 바로 다시 받습니다.
        public string[] Send(string line)
        {
            if(false)
            //if (0 == recv)
            {
                MessageBox.Show("서버와 연결이 되어있지 않습니다.");
                ArrData = new string[1];
                ArrData[0] = "-2";
                return ArrData;
            }

            byte[] data;
            //server.Send(Encoding.UTF8.GetBytes(line));
            sent = SendVarData(Encoding.UTF8.GetBytes(line));
            //data = new byte[1024];
            ReceiveVarData(out data);

            if (recv == 0 || recv == -1)
                MessageBox.Show("연결끊겼다" + recv);

            //output = Encoding.UTF8.GetString(data, 0, recv);
            // 아래로 대체합니다.
            output = Encoding.UTF8.GetString(data);

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

        // 로그인 성공시 일련번호, 실패시 0 
        public int Login(string ID, string PW)
        {
            ArrData = Send((int)FormNum.LOGIN + "" + (int)OpCode.LOGIN_REQUEST + "02" + ID + delimiter + PW);
            //바이트 수 계산용
            Encoding.Default.GetByteCount("asd");

            if (0 == recv || -1 == recv)
                return 0;

            IsValidOp((int)OpCode.LOGIN_REQUEST);
            int result = Convert.ToInt32(ArrData[0]);

            return result;
        }

        protected bool IsValidOp(int opName)
        {
            if (false)
            //if (sp.getCnt() != 1 && sp.getForm() != 0) 
            //&& sp.getJustOpCode() != opName)
            {
                throw new Exception("예상한 op 코드가 아닙니다.");
            }

            return true;
        }

        // 아래로 사용 함수
        // tcp 데이터를 송신만 합니다.
        public void DataSend(int opcode, string line)
        {
            while (synchronized)
            {
                Thread.Sleep(100);
            }
            synchronized = true;

            byte[] Sdata = new byte[1024];
            string message = opcode + line;

            // 임시 spliter
            int count = 1;
            if (line.Equals("")) { count = 0; }
            foreach (char c in message)
                if (c.Equals(delimiter[0])) count++;

            if (count < 10)
            {
                message = opcode + "0" + count + line;
            }
            else
            {
                message = opcode + count + line;
            }

            Sdata = Encoding.UTF8.GetBytes(message);
            //server.Send(Sdata);
            sent = SendVarData(Sdata);

            synchronized = false;
        }

        public void ReciveBData(out byte[] Bdata, out int Blength)
        {
            byte[] Rdata = new byte[0];
            Blength = ReceiveVarData(out Rdata);
            Bdata = Rdata;
        }

        public string ReciveData()
        {
            byte[] Rdata;
            //= new byte[1024];
            ReceiveVarData(out Rdata);
            stringData = Encoding.UTF8.GetString(Rdata);
            return stringData;
        }

        public bool IsClosed()
        {
            if (recv == 0 || recv == -1)
                return true;
            return false;
        }

        // 연결되있을시 true, 연결 실패시 false 재시도수 10
        public bool connectReTry()
        {
            if (Connected)
                return true;

            for (int i = 0; i < 10; i++)
            {
                Initalize();
                if (Connected)
                    return true;
            }

            return false;
        }
    }
}