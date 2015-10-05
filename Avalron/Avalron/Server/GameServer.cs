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
        int evilCount;
        PlayerInfo[] player;
        ClientServer server;
        int expeditionMaker; // 원정대장 정보
        int ladyoftheLake; // 호수의 여인 정보
        int round; //라운드
        int rejectCount; // 투표 실패 갯수
        int Success; // 원정 성공 횟수
        int[] expeditionCountList; // 원정대 인원정보
        ExpeditionSelect expeditionSelected; // 원정대 선택정보
        VoteInfo voteInfo; // 투표 결과정보
        EvilVoteInfo evilVoteInfo;
        
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
            characterCard.TeamSetting(ref player);
            Random r = new Random((int)DateTime.Now.Ticks);
            expeditionMaker = r.Next(0, clientCount - 1);
            round = 1;
            rejectCount = 0;
            ladyoftheLake = -1; //호수의 여인 index정보. 여기선 사용자가 없으므로 -1
            expeditionSelected = new ExpeditionSelect(clientCount);
            Success = 0;
            expeditionCountCalc();
            evilCount = (clientCount + 2) / 3;
            voteInfo = new VoteInfo();
            evilVoteInfo = new EvilVoteInfo();
            voteInfo.init(clientCount);
            

            
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
            //호수의 여인 획득 이벤트
            getLake();
            // 라운드 정보를 알려준다.
            server.sendToMessageAll("20001" + (expeditionCountList[round-1]));


        }

        //원정대원 선택, 해제 이벤트(1 : 선택, 0 : 해제)
        public void selectExpedition(int index, int selected, int opcode)
        {
            expeditionSelected.setSelected(index);
            server.sendToMessageAll(""+ opcode + "02" + index + server.delimiter + selected);
        }

        //원정대원 인원체크
        public void setExpedition(int opcode, int index)
        {
            //인원체크하고 true이면 전체에게 30000을, false이면 원정대장에게 인원 다시 체크하도록...
            if(expeditionCountList[round-1] == expeditionSelected.getCount(round))
            {
                server.sendToMessageAll("203011");
                server.sendToMessageAll("30000");
            }
            else
            {
                server.sendToMessage("" + opcode+"01"+"0",index);
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
                    afterResult = "102"+ toIndex + server.delimiter + (1 - player[toIndex].getCard() / 8);
                else
                    afterResult = "202" + toIndex + server.delimiter + fromIndex;

                ladyoftheLake = toIndex;
                server.sendToMessage(result+ afterResult, i);
            }
            server.sendToMessageAll("20001" + fromIndex);
        }

        //멀린 죽이는 이벤트
        public void killMerlin(int dieIndex)
        {
            string result = "40401";
            int winFlag;

            if (player[dieIndex].getCard() == 0)
                winFlag = 0;
            else
                winFlag = 1;

            server.sendToMessageAll(result);
            endofGame(winFlag);
        }
        //호수의 여인 사용시점을 알려주는 이벤트
        public void setLake()
        {
            server.sendToMessageAll("40301" + ladyoftheLake);
        }

        //호수의 여인 카드 얻는 이벤트
        public void getLake()
        {
            ladyoftheLake = (clientCount + expeditionMaker - 1) % clientCount;
            server.sendToMessageAll("40001" + ladyoftheLake);
        }
        
        //원정 완료 이벤트
        public void expeditionSuccess(int successCheck)
        {
            round++;
            rejectCount = 0;
            if (successCheck == 0)
                Success++;
            expeditionSelected = new ExpeditionSelect(clientCount);
            voteInfo.init(clientCount);

            server.sendToMessageAll("30603" + successCheck + server.delimiter + round + server.delimiter + expeditionMaker);
            if(Success == 3)
            {
                endofGame(1);
            }
            if (round <= 5)
            {
                if (round >= 3)
                    setLake();
                else
                    server.sendToMessageAll("20001" + expeditionCountList[round - 1]);
            }
            else if (round - Success > 2) 
            {
                endofGame(0);
            }
            else if(round == 5)
            {
                for (int i = 0; i < clientCount; i++)
                {
                    if(player[i].getCard() == 8)
                        server.sendToMessageAll("40300");
                }
                server.sendToMessageAll("80100");

            }
        }



        //원정 성공여부 투표 이벤트
        public void setEvilVote(int voteResult)
        {
            int peopleInfo = evilVoteInfo.setVote(voteResult);

            if (peopleInfo == 0)//투표 완료시
                expeditionSuccess(evilVoteInfo.getFailCount());
        }

        //원정 투표 이벤트
        public void setExpeditionVote(int clientIndex, int voteResult)
        {
            
            if (voteInfo.setVote(clientIndex, voteResult) == 0)//투표 완료시
            {
                string result = "";
                
                int vote = voteInfo.getVoteResult(0);

                

                result += "0" + server.delimiter + vote;

                for (int i = 1; i < clientCount; i++) 
                {
                    vote = voteInfo.getVoteResult(i);
                    result += ""+ server.delimiter + i + server.delimiter + vote;
                }


                expeditionMaker = (expeditionMaker + 1) % clientCount;

                if (voteInfo.getAgreeCount() * 2 > clientCount)  // 투표 가결 이벤트
                {
                    server.sendToMessageAll("302" + (clientCount * 2) + result);
                    server.sendToMessageAll("303011");
                    expedition();
                }
                else // 투표 부결 이벤트
                {
                    rejectCount++;

                    if (rejectCount == 5)
                        endofGame(0);
                    else
                    {
                        expeditionSelected = new ExpeditionSelect(clientCount);
                        voteInfo.init(clientCount);
                        server.sendToMessageAll("302" + (clientCount * 2) + result);
                        server.sendToMessageAll("30303" + "0" + server.delimiter + rejectCount + server.delimiter + expeditionMaker);
                        server.sendToMessageAll("10201" + expeditionMaker);
                        server.sendToMessageAll("20001" + expeditionCountList[round - 1]);
                        
                    }
                }

            }
        }
        //원정 수행 이벤트
        public void expedition()
        {
            int[] member;
            expeditionSelected.getMember(out member);

            evilVoteInfo.init(member.Length);
            //악 진형 플레이어들에게 선택지를 보냄
            for (int i = 0; i < member.Length; i++)
            {
                server.sendToMessage("30400", member[i]);
            }
        }

        public void endofGame(int win)
        {
            for(int i =0;i< clientCount;i++)
            {
                if((player[i].getCard() / 8) != win)
                    server.sendToMessageAll("500011");//승리
                else
                    server.sendToMessageAll("500010");//패배
            }
        }
    }

    

    class ExpeditionSelect
    {
        bool[] selected;
        int count;//선택된 원정대원의 인원수

        public ExpeditionSelect(int clientCount)
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
    class EvilVoteInfo
    {
        int peopleCount;
        int fail;
        public void init(int memberCount)
        {
            peopleCount = memberCount;
            fail = 0 ;
        }
        public int getFailCount()
        {
            return fail;
        }

        public int setVote(int voteResult)
        {
            peopleCount--;
            fail += 1-voteResult;
            return peopleCount;
                     
        }
    }

    class VoteInfo
    {
        int[] vote;
        int peopleCount; //전체인원 - 투표수
        int agree;
        public void init(int memberCount)
        {
            vote = new int[memberCount];
            peopleCount = memberCount; // 인원수로 votecount를 초기화\
            agree = 0;
        }
        // 투표완료시 votecount를 1개 줄임
        public int setVote(int clientIndex, int voteResult)
        {
            vote[clientIndex] = voteResult;
            peopleCount--;
            agree += voteResult;
            return peopleCount;
        }
        public int getVoteResult(int clientIndex)
        {
            return vote[clientIndex];
        }
        public int getAgreeCount()
        {
            return agree;
        }
    }
}
