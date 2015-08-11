using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Avalron
{
    public class TCPClient
    {
        byte[] data = new byte[1024];
        string input, stringData;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("203.255.3.92"), 9050);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        int recv;

        public TCPClient()
        {
            try
            {
                server.Connect(ipep);
            } catch(SocketException e)
            {
                MessageBox.Show("서버와 연결하는데 실패하였습니다.");
                return;
            }
            recv = server.Receive(data);
            stringData = Encoding.UTF8.GetString(data, 0, recv);
        }
        ~TCPClient()
        {
            //MessageBox.Show("서버와 연결을 끊고 있습니다.");
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        public void close()
        {
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        // 아래로 사용 함수
        public void Login_Send(string line)
        {
            input = line;
            server.Send(Encoding.UTF8.GetBytes(input));
            data = new byte[1024];
            recv = server.Receive(data);
            stringData = Encoding.UTF8.GetString(data, 0, recv);
            MessageBox.Show(stringData);
        }

        public void DataSend(string opcode, string line)
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

        public void Room_Refresh(string opcode)
        {
            server.Send(Encoding.UTF8.GetBytes(opcode));
        }
    }
}
