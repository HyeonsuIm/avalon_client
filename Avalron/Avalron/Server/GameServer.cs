using Avalron.Avalron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron.Avalron.Server
{
    public class GameServer
    {
        int clientCount;
        PlayerInfo[] player;
        Server server;

        public void setServer(Server server)
        {
            this.server = server;
        }

        public GameServer(int clientCount)
        {
            this.clientCount = clientCount;
        }

        void gameInit()
        {
            CharacterCard characterCard = new CharacterCard(clientCount);
            characterCard.TeamSetting(out player);
            Random r = new Random();
            int makingExpedition = r.Next(0, clientCount - 1);
        }

        void gameStart()
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
