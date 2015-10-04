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
        int expeditionMaker; //원정대장 정보
        int ladyoftheLake; //호수의 여인 정보
        int round;
        int voteCount;
        int[] expeditionCountList; // 원정대 인원정보
        ExpeditionSelect expeditionSelected; // 원정대 선택정보
        VoteInfomation[] voteInfo; // 투표 결과정보

        //투표 결과 정보 클래스


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
            expeditionMaker = r.Next(0, clientCount - 1);
            round = 1;
            voteCount = 0;
            ladyoftheLake = -1; //호수의 여인 index정보. 여기선 사용자가 없으므로 -1
            expeditionCountCalc();
        }

        void expeditionCountCalc()
        {
            expeditionCountList = new int[5];
            if(clientCount == 5)
            {
                expeditionCountList[0] = 2;
                expeditionCountList[1] = 3;
                expeditionCountList[2] = 2;
                expeditionCountList[3] = 3;
                expeditionCountList[4] = 3;
            }
            else if (clientCount == 6)
            {
                expeditionCountList[0] = 2;
                expeditionCountList[1] = 3;
                expeditionCountList[2] = 4;
                expeditionCountList[3] = 3;
                expeditionCountList[4] = 4;
            }
            else if (clientCount == 7)
            {
                expeditionCountList[0] = 2;
                expeditionCountList[1] = 3;
                expeditionCountList[2] = 3;
                expeditionCountList[3] = 4;
                expeditionCountList[4] = 4;
            }
            else if (clientCount >= 8)
            {
                expeditionCountList[0] = 3;
                expeditionCountList[1] = 4;
                expeditionCountList[2] = 4;
                expeditionCountList[3] = 5;
                expeditionCountList[4] = 5;
            }
        }

        public void gameStart()
        {
            //카드 정보를 알려준다
            for (int i = 0; i < clientCount; i++) {
                int argumentCount = 0;
                string argument = "";
                if (player[i].getCard() == (int)CharacterCard.Card.Merlin)
                {
                    for (int j = 0; j < clientCount; j++)
                    {
                        if (player[j].getTeam() == 2)
                        {
                            if (argumentCount != 0)
                                argument += server.delimiter;
                            argument += j;
                            argumentCount++;

                        }
                    }
                }
                else if (player[i].getCard() == (int)CharacterCard.Card.Percival) {
                    for (int j = 0; j < clientCount; j++)
                    {
                        if (player[j].getCard() == (int)CharacterCard.Card.Morgana || player[j].getCard() == (int)CharacterCard.Card.Merlin)
                        {
                            if (argumentCount != 0)
                                argument += server.delimiter;
                            argument += j;
                            argumentCount++;

                        }
                    }
                }
                else if(player[i].getTeam() == 2)
                {
                    for (int j = 0; j < clientCount; j++)
                    {
                        if (player[j].getTeam() == 2)
                        {
                            if (argumentCount != 0)
                                argument += server.delimiter;
                            argument += j;
                            argumentCount++;
                        }
                    }
                }
                server.sendToMessage("1010" + (argumentCount+1) +player[i].getCard()+server.delimiter + argument,i);
            }

            //원정대장을 알려준다.
            server.sendToMessageAll("10201" + expeditionMaker);
            //게임 시작
            server.sendToMessageAll("10300");

            // 라운드 정보를 알려준다.
            server.sendToMessageAll("20001" + expeditionCountList[round]);
            
            
        }


        //원정대원 선택, 해제(1 : 선택, 2 : 해제)
        public void selectExpedition(int index, int selected, int opcode)
        {
            server.sendToMessageAll(""+ opcode + "02" + index + server.delimiter + selected);
        }

        //원정대원 인원체크
        public void setExpedition(int opcode, int index)
        {
            //인원체크하고 true이면 전체에게 30000을, false이면 원정대장에게 인원 다시 체크하도록...
            if(expeditionCountList[round] == expeditionSelected.getCount(round))
            {
                server.sendToMessageAll("30000");
            }
            else
            {
                server.sendToMessage("" + opcode+"01"+"0", );
            }
        }

        //호수의 여인 체크
        public void useLake(int index, int seleceted)
        {
            //사용자에게 select한 유저의 선악정보를, 나머지에게는 누구에게 사용했는지 알려줌.
            for(int i = 0; i<clientCount;i++)
            {

            }
        }


        public void killMerlin(int index)
        {
            string result = "40101";

            if (player[index].getCard() == 0)
                result += 1;
            else
                result += 0;

            server.sendToMessageAll(result);
        }


        //2라운드 시작할때 호수의 여인 카드를 얻음
        public void getLake()
        {
            ladyoftheLake = (clientCount + expeditionMaker - 1) % clientCount;
        }
    }


    class VoteInfomation
    {
        int index;
        int selectedIndex;

        public void setVote(int index, int selectedIndex)
        {
            this.index = index;
            this.selectedIndex = selectedIndex;
        }
        public void init()
        {
            index = -1;
            selectedIndex = -1;
        }
    }


    class ExpeditionSelect
    {
        bool[] selected;
        int count;

        public void init(int count)
        {
            selected = new bool[count];
            count = 0;
        }

        public bool getSelected(int index)
        {
            return selected[index];    
        }
        public void setSelected(int index)
        {
            if (selected[index])
                count--;
            else
                count++;
            selected[index] = !selected[index];
        }
        public int getCount(int index)
        {
            return count;
        }
    }
}
