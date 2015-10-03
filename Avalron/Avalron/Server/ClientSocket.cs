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
                data = new byte[1024];
                recv = socket.Receive(data);
                if (recv == 0)
                    break;
            }
            socket.Close();
            connection--;
        }

        public static int getConnectionCount()
        {
            return connection;
        }

        public void sendMessage(string data) {
            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(data);
            socket.Send(buffer);
        }
        void opcodeAnalysis()
        {
            
        }
    }
}
