using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace Avalron.Avalron.Server
{
    class ClientSocket
    {
        Socket serverSocket;
        Socket socket;
        static int connection = 0;
        static char delimiter = '\u0001';

        public ClientSocket(Socket serverSocket, Socket socket)
        {
            this.serverSocket = serverSocket;
            this.socket = socket;
            connection++;
        }

        public void Handle() {
            int recv;
            byte[] data;

            while (true)
            {
                data = receiveVarData();
                if (data == null)
                    break;
            }
            socket.Close();
            connection--;
        }

        private byte[] receiveVarData()
        {
            int total = 0;
            int recv;
            byte[] datasize = new byte[4];
            recv = socket.Receive(datasize, 0, 4, 0);
            if (recv == 0)
                return null;
            int size = BitConverter.ToInt32(datasize, 0);
            int dataleft = size;
            byte[] data = new byte[size];

            while (total < size)
            {
                recv = socket.Receive(data, total, dataleft, 0);
                if (recv == 0)
                {
                    data = Encoding.UTF8.GetBytes("exit");
                    break;
                }
                total += recv;
                dataleft -= recv;
            }
            return data;
        }

        private int sendVarData(byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;

            byte[] datasize = new byte[4];
            datasize = BitConverter.GetBytes(size);
            sent = socket.Send(datasize);

            while (total < size)
            {
                sent = socket.Send(data, total, dataleft, SocketFlags.None);
                total += sent;
                dataleft = -sent;
            }

            return total;
        }

        public static int getConnectionCount()
        {
            return connection;
        }

        public void sendMessage(string data) {
            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(data);
            sendVarData(buffer);
        }
        void opcodeAnalysis(string data)
        {
            int phase; //페이즈 번호
            int opcode; // opcode 번호
            int argumentCount; // 매개변수 개수
            string[] argumentList; //매개변수들을 저장할 배열
            
            //opcode 분할부
            opcodeSplit(data, out phase, out opcode, out argumentCount);
            
            if (argumentCount > 0)
            {
                setArgumentList(out argumentList,data);
                if (argumentList.Length != argumentCount)
                    throw new ArgumentException();
            }
            switch(phase)
            {
                case 1:
                    gameStartControl(opcode);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 8:
                    break;
                case 9:
                    break;

            }
        }

        void opcodeSplit(string data, out int phase, out int opcode, out int argumentCount)
        {
            phase = int.Parse(data.Substring(0, 1));
            opcode = int.Parse(data.Substring(1, 2));
            argumentCount = int.Parse(data.Substring(3, 2));
        }
        void setArgumentList(out string[] argumentList, string data)
        {
            argumentList = data.Substring(5).Split(delimiter);
        }

        void gameStartControl(int opcode)
        {
            switch(opcode)
            {

            }
        }
    }
}
