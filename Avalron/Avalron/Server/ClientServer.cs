using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using AvalonServer;
namespace Avalron.Avalron.Server
{
    public class ClientServer
    {
        static int port = 9051;
        string[] IPList;
        int clientCount;
        Socket serverSocket;
        List<ClientSocket> clientList;
        int leaderIDNum;
        GameServer gameServer;
        TcpUserInfo[] userInfo;
        public int state;
        Thread gameServerThread;

        public char delimiter = '\u0001';

        public ClientServer(string[] IPList, TcpUserInfo[] userInfo) {
            clientCount = IPList.Length;
            this.IPList = IPList;
            this.userInfo = userInfo;
            state = 0;
        }

        public void setGameServer(GameServer gameServer)
        {
            this.gameServer = gameServer;
        }

        public string[] getIPList() {
            return IPList;
        }


        public int getClientCount() {
            return clientCount;
        }
        //서버 설정 및 게임시작
        public void serverSetting() {
            GetInformation();
            waitingClient(IPList);

            while (0 != SystemCheck())
            {
                Thread.Sleep(100);
            }
            
            gameServer.setServer(this);
        }

        //ip가 맞는지 확인해야함
        int waitingClient(string[] IPList) {
            clientList = new List<ClientSocket>();
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, port);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(ipep);
            serverSocket.Listen(10);

            Socket temp;
            Socket[] client = new Socket[6];
            ClientSocket[] clientSocket = new ClientSocket[6];


            state = 1;
            for (int i = 0; i < clientCount; i++)
            {
                temp = serverSocket.Accept();
                string ip = temp.RemoteEndPoint.ToString().Split(':')[0];
                int j;
                for (j=0;j< clientCount; j++)
                {
                    if (ip == IPList[j])
                    {
                        client[j] = temp;
                        break;
                    }
                }
                clientSocket[j] = new ClientSocket(serverSocket, temp, j);
                clientSocket[j].setClientServer(this);
                clientSocket[j].setGameServer(gameServer);

                Thread clientThread = new Thread(new ThreadStart(clientSocket[j].Handle));
                clientThread.Start();
            }
            for(int i = 0; i < clientCount; i++)
            {
                clientList.Add(clientSocket[i]);
            }

            gameServerThread = new Thread(new ThreadStart(gameServer.gameStart));
            gameServerThread.Start();
            return 0;
        }

        void GetInformation()
        {
            // 접속하는 사람의 인수는?
            // 접속하는 사람의 IP, Nick, ID, ID일련번호 에 대한 정보 받기
        }

        int SystemCheck()
        {
            // 사용자가 모두 접속하였는가?
            // 사용자와 서버는 모두 통신이 가능한가?
            if (ClientSocket.getConnectionCount() != clientCount)
                return 1;

            return 0;
        }

        bool IsEnd()
        {
            return false;
        }

        public void sendToMessageAll(string data)
        {
            for (int i = 0; i < clientCount; i++)
            {
                sendToMessage(data, i);
            }
        }

        public void sendToMessage(string data, int index) {
            clientList.ElementAt<ClientSocket>(index).sendMessage(data);
        }
    }

    class PInformation
    {
        string Address;
        string Nick;
        string ID;
        int IDNum;
        CharacterCard.Card Card;
        bool IsLeader;
    }
}