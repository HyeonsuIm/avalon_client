using Avalron.Avalron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvalonServer;

namespace Avalron.Avalron.Server
{
    public class GameServer
    {
        int clientCount;
        PlayerInfo[] player;
        ClientServer server;

        

        public GameServer(int clientCount, TcpUserInfo[] userInfo)
        {
            this.clientCount = clientCount;
            player = new PlayerInfo[clientCount];
            for(int i=0;i< clientCount; i++)
            {
                player[i] = new PlayerInfo();
                player[i].setUser(userInfo[i]);
            }
        }
        public void setServer(ClientServer server)
        {
            this.server = server;
        }

        public void gameInit()
        {
            CharacterCard characterCard = new CharacterCard(clientCount);
            characterCard.TeamSetting(out player);
            Random r = new Random();
            int makingExpedition = r.Next(0, clientCount - 1);
        }

        public void gameStart()
        {
            //게임 시작 알려주고
            server.sendToMessageAll(""); // 게임시작 OPCODE 전송해야함

            //자기 정보 알려주고
            for (int i = 0; i < clientCount; i++) {
                server.sendToMessage(""+player.ToString(), i); // 플레이어 정보 전송해야함
            }
            
            //채팅하면서 원정대 만들기 시작


            //selectExpedition();
        }
    }
}
