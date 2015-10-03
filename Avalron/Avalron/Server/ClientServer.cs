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
        string[] ipList;
        int clientCount;
        Socket serverSocket;
        List<ClientSocket> clientList;
        int leaderIDNum;
        GameServer gameServer;
        TcpUserInfo[] userInfo;

        public ClientServer(string[] ipList, TcpUserInfo[] userInfo) {
            clientCount = ipList.Length;
            this.ipList = ipList;
            this.userInfo = userInfo;
        }

        public void setGameServer(GameServer gameServer)
        {
            this.gameServer = gameServer;
        }

        public int getClientCount() {
            return clientCount;
        }
        //서버 설정 및 게임시작
        public void serverSetting() {
            GetInformation();
            waitingClient(ipList);

            while (0 != SystemCheck())
            {
                Thread.Sleep(100);
            }
            
            gameServer.setServer(this);
        }

        //ip가 맞는지 확인해야함
        int waitingClient(string[] ipList) {
            clientList = new List<ClientSocket>();
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, port);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(ipep);
            serverSocket.Listen(clientCount - 1);

            Socket client;

            for (int i = 0; i < clientCount-1; i++)
            {
                client = serverSocket.Accept();
                ClientSocket clientSocket = new ClientSocket(serverSocket, client);
                clientList.Add(clientSocket);
                Thread clientThread = new Thread(new ThreadStart(clientSocket.Handle));
                clientThread.Start();
            }
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