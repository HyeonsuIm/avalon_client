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
        GameServer gameServer;
        ClientServer clientServer;
        static int connection = 0;
        static char delimiter = '\u0001';
        int index;

        public ClientSocket(Socket serverSocket, Socket socket, int index)
        {
            this.serverSocket = serverSocket;
            this.socket = socket;
            this.index = index;
            connection++;
            
        }
        public void setGameServer(GameServer server)
        {
            gameServer = server;
        }

        public void setClientServer(ClientServer server)
        {
            clientServer = server;
        }
        public static int getConnectionCount()
        {
            return connection;
        }

        public void Handle() {
            byte[] data;

            while (true)
            {
                data = receiveVarData();
                if (data == null)
                    break;
                opcodeAnalysis(Encoding.UTF8.GetString(data));
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

        public void sendMessage(string data) {
            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(data);
            sendVarData(buffer);
        }
        void opcodeAnalysis(string data)
        {
            int opcode; // phase + opcode 번호
            int argumentCount; // 매개변수 개수
            string[] argumentList= null; //매개변수들을 저장할 배열
            
            //opcode 분할부
            opcodeSplit(data,out opcode, out argumentCount);
            
            if (argumentCount > 0)
            {
                setArgumentList(out argumentList,data);
                if (argumentList.Length != argumentCount)
                    throw new ArgumentException();
            }
            switch(opcode)
            {
                case 201:
                    gameServer.selectExpedition(int.Parse(argumentList[0]), 1, opcode);
                    break;
                case 202:
                    gameServer.selectExpedition(int.Parse(argumentList[0]), 0, opcode);
                    break;
                case 203:
                    gameServer.setExpedition(opcode, index);
                    break;

                case 301:
                    gameServer.setVote(index, int.Parse(argumentList[0]));
                    
                    break;
                case 400:
                    gameServer.useLake(int.Parse(argumentList[0]), int.Parse(argumentList[1]), opcode);
                    break;
                case 402:
                    gameServer.killMerlin(int.Parse(argumentList[0]));
                    break;
                case 800:
                     clientServer.sendToMessageAll("80002" + argumentList[0] + clientServer.delimiter + argumentList[1]);
                    break;

            }
        }

        void opcodeSplit(string data, out int opcode, out int argumentCount)
        {
            opcode = int.Parse(data.Substring(0, 3));
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
