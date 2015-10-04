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
        int expeditionMaker; // 원정대장 정보
        int ladyoftheLake; // 호수의 여인 정보
        int round;
        int voteCount; // 실패 갯수
        int Success; // 원정 성공 횟수
        int[] expeditionCountList; // 원정대 인원정보
        ExpeditionSelect expeditionSelected; // 원정대 선택정보
        VoteInfo voteInfo; // 투표 결과정보
        
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
            expeditionSelected = new ExpeditionSelect();
            Success = 0;
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


        //원정대원 선택, 해제 이벤트(1 : 선택, 0 : 해제)
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
                server.sendToMessage("" + opcode+"01"+"0", index);
            }
        }

        //호수의 여인 사용 이벤트
        public void useLake(int toIndex, int fromIndex, int opcode)
        {
            //사용자에게 select한 유저의 선악정보를, 나머지에게는 누구에게 사용했는지 알려줌.

            string result = "40";
            for(int i = 0; i<clientCount;i++)
            {
                string afterResult;
                if(i == fromIndex)
                    afterResult = "101" + player[toIndex].getCard();
                else
                    afterResult = "002" + toIndex + server.delimiter + fromIndex;

                server.sendToMessage(result+ afterResult, i);
            }
        }

        //멀린 죽이는 이벤트
        public void killMerlin(int dieIndex)
        {
            string result = "40101";

            if (player[dieIndex].getCard() == 0)
                result += 1;
            else
                result += 0;

            server.sendToMessageAll(result);
        }


        //호수의 여인 카드 얻는 이벤트
        public void getLake()
        {
            ladyoftheLake = (clientCount + expeditionMaker - 1) % clientCount;
        }


 

        //투표 저장 이벤트
        public void setVote(int clientIndex, int voteResult)
        {
            
            if (voteInfo.setVote(clientIndex, voteResult) == 0)//투표 완료시
            {
                string result = "";
                int agreeCount = 0;

                int vote = voteInfo.getVoteResult(0);
                agreeCount += vote;

                result += 0 + server.delimiter + vote;

                for (int i = 1; i < clientCount; i++) 
                {
                    vote = voteInfo.getVoteResult(i);
                    result += server.delimiter + i + server.delimiter + vote;
                }


                expeditionMaker = (expeditionMaker + 1) % clientCount;

                if (agreeCount*2 > clientCount) // 투표 가결 이벤트
                {
                    round++;
                    result += server.delimiter + "1" + server.delimiter + expedition() + server.delimiter + round + server.delimiter + expeditionMaker; //만들어야됨 추가부분;
                    server.sendToMessageAll("302" + (clientCount * 2 + 4) + result);
                }
                else // 투표 부결 이벤트
                {

                    voteCount++;
                    result += server.delimiter + "0" + server.delimiter + voteCount + server.delimiter + expeditionMaker;
                    server.sendToMessageAll("302" + (clientCount * 2 + 3) + result);
                }
            }
        }
        //원정 수행 이벤트
        public int expedition()
        {
            int result = 1;
            int[] member;
            expeditionSelected.getMember(out member);
            for (int i = 0; i < member.Length; i++)
            {
                result *= (1 - (player[member[i]].getCard() / 8));// 1~6 : 선 진영 8~14 악 진형이므로 1- (cardindex/8)을하면 악 진영에 0, 선 진영에 1반환
            }
            Success += result;
            return result;
        }

    }

    class ExpeditionSelect
    {
        bool[] selected;
        int count;//선택된 원정대원의 인원수

        public void init(int clientCount)
        {
            selected = new bool[clientCount];
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
        public void getMember(out int[] index)
        {
            int temp = 0;
            index = new int[count];

            for (int i = 0;i<selected.Length;i++)
            {
                if (selected[i])
                {
                    index[temp] = i;
                    temp++;
                }
            }
        }
    }
    class VoteInfo
    {
        int[] vote;
        int peopleCount; //전체인원 - 투표수

        VoteInfo(int clientCount)
        {
            vote = new int[clientCount];
            peopleCount = clientCount; // 인원수로 votecount를 초기화
        }
        // 투표완료시 votecount를 1개 줄임
        public int setVote(int clientIndex, int voteResult)
        {
            vote[clientIndex] = voteResult;
            peopleCount--;

            return peopleCount;
        }
        public int getVoteResult(int clientIndex)
        {
            return vote[clientIndex];
        }
        
    }
}
