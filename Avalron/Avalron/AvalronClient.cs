using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron.Avalron
{
    public class AvalronClient : TCPClient
    {
        public enum GameOpCode { CardInfo = 101, SelectLeader, GameStart };
        public enum TeamBuildingOpCode { TeamMemberNum = 200, TeamSelect, TeamDeSelect, TeamComplete };
        public enum VoteOpCode { StartVote = 300, Voting, VoteComplete, VoteResult, QuestStart, Questing, QuestResult };
        public enum EtcSpecialOpCode { GetLadyOfTheLake = 400, LadyOfTheLakeResult, OtherLadyOfTheLake, MyLadyOfTheLake, MerlinAssassinate };
        public enum GameComplete { GameEnd = 500 };
        public enum ChattingOpCode { CHATSEND = 800, ChattingOn, ChattingOff };

        public AvalronClient() : base()
        {

        }

        public AvalronClient(string address, int port) : base(address, port)
        {

        }

        public void ChatSend(string nick, string line)
        {
            //Send((int)FormNum.GAME + (int)OpCode.CHATSEND + "02" + nick + delimiter +  line);
            DataSend(Convert.ToInt32((int)FormNum.LOBBY + "0" +(int)ChattingOpCode.CHATSEND), nick + delimiter + line);
        }

        public void WisperSend(string nick, string ToNick, string line)
        {
            DataSend(Convert.ToInt32((int)FormNum.LOBBY + "01"), nick + delimiter + ToNick + delimiter + line);
        }

        public void avalronRecv()
        {
            string getString = "";
            while(Program.state % 10 == 3)
            {
                try {
                    getString = ReciveData();
                }
                catch(System.Net.Sockets.SocketException e)
                {
                    MessageBoxEx.Show(e.Message);
                }
                 
                if (getString == "")
                    continue;

                Spriter spriter = new Spriter(getString);
                int opCode = spriter.getJustOpCode();

                switch (opCode)
                {
                    case (int)GameOpCode.CardInfo:
                        // 자기 카드숫자, 필요한 능력
                        Thread.Sleep(1000);
                        Program.avalron.playerInfo = new PlayerInfo();
                        Program.avalron.playerInfo.setCard(Convert.ToInt32(spriter.split[0]));

                        // 능력에 대한 추가정보를 처리합니다. // 0을 제외함.
                        if (1 != spriter.getCnt())
                        {
                            //int[] indexs = new int[spriter.getCnt()];
                            for (int i = 1; i < spriter.getCnt() + 1; i++)
                            {
                                int index = Convert.ToInt32(spriter.split[i]);
                                Program.avalron.evilShow(index);
                            }
                        }
                        Program.avalron.setOwnCard(Program.avalron.playerInfo.getCard());
                        break;
                    case (int)GameOpCode.SelectLeader:
                        Program.avalron.SetLeader(Convert.ToInt32(spriter.split[0]));
                        break;
                    case (int)GameOpCode.GameStart:
                        Program.avalron.gameStart();
                        break;
                    case (int)TeamBuildingOpCode.TeamMemberNum:
                        Program.avalron.setPhaseState(Avalron.PhaseState.TeamBuilding);
                        Program.avalron.selectQuestTeamStart(Convert.ToInt32(spriter.split[0]));
                        break;
                    case (int)TeamBuildingOpCode.TeamSelect:
                        Program.avalron.selectQuestTeam(Convert.ToInt32(spriter.split[0]));
                        break;
                    case (int)TeamBuildingOpCode.TeamDeSelect:
                        Program.avalron.deSelectQuestTeam(Convert.ToInt32(spriter.split[0]));
                        break;
                    case (int)TeamBuildingOpCode.TeamComplete:
                        //Program.avalron.chatting.addSystemText("원정 선택이 완료되었습니다.");
                        break;
                    case (int)VoteOpCode.StartVote:
                        Program.avalron.setPhaseState(Avalron.PhaseState.Vote);
                        Program.avalron.Vote("원정대원으로 원정을 가시겠습니까?");
                        break;
                    case (int)VoteOpCode.Voting:
                        break;
                    case (int)VoteOpCode.VoteComplete:
                        { 
                            int[] indexs = new int[spriter.getCnt()];
                            for (int i = 0; i < spriter.getCnt();)
                            {
                                indexs[i] = Convert.ToInt32(spriter.split[i]);
                                int voteInt = Convert.ToInt32(spriter.split[i + 1]);
                                bool vote = false;

                                if (0 == voteInt)
                                    vote = false;
                                else if (1 == voteInt)
                                    vote = true;

                                Program.avalron.voteShow(indexs[i], vote);
                                i = i + 2;
                            }
                        }
                        // 이전 원정대원들의 표시를 지우자.
                        Program.avalron.questTeamClear();
                        break;
                    case (int)VoteOpCode.VoteResult:
                        int voteTemp = Convert.ToInt32(spriter.split[0]);

                        // 투표 가결시 아무것도 안함 ㅋ
                        if(1 == voteTemp)
                        {
                            Program.avalron.questTeamClear();
                        }
                        else if(0 == voteTemp)
                        {
                            Program.avalron.voteTrack.Next();
                            if (Program.avalron.voteTrack.rejected != Convert.ToInt32(spriter.split[1]))
                            {
                                MessageBox.Show("서버와 클라간 투표 거절수가 같지 않음" + '\n'
                                    + "서버 " + Convert.ToInt32(spriter.split[1])
                                    + "클라 " + Program.avalron.voteTrack.rejected);
                            }

                            Program.avalron.SetLeader(Convert.ToInt32(spriter.split[2]));
                        }
                        break;
                    case (int)VoteOpCode.QuestStart:
                        Program.avalron.setPhaseState(Avalron.PhaseState.MyQuest);
                        Program.avalron.questStart();
                        break;
                    case (int)VoteOpCode.Questing:
                        Program.avalron.setPhaseState(Avalron.PhaseState.OtherQuest);
                        break;
                    case (int)VoteOpCode.QuestResult:

                        int failCnt = Convert.ToInt32(spriter.split[0]);

                        Program.avalron.questionShow(failCnt);
                        Program.avalron.voteTrack.Clear();

                        if (Program.avalron.roundTrack.curRound+1 != Convert.ToInt32(spriter.split[1]))
                            MessageBox.Show("서버와 라운드숫자가 틀립니다." + '\n'
                                    + "서버 " + Convert.ToInt32(spriter.split[1])
                                    + "클라 " + Program.avalron.roundTrack.curRound);

                        Program.avalron.SetLeader(Convert.ToInt32(spriter.split[2]));
                        break;
                    case (int)EtcSpecialOpCode.GetLadyOfTheLake:
                        Program.avalron.setPhaseState(Avalron.PhaseState.OtherLadyOfTheLake);
                        Program.avalron.ladyOfTheLakeShow(Convert.ToInt32(spriter.split[0]));
                        break;
                    case (int)EtcSpecialOpCode.LadyOfTheLakeResult:
                        Program.avalron.setPhaseState(Avalron.PhaseState.MyLadyOfTheLake);
                        Program.avalron.ladyOfTheLakeResult(Convert.ToInt32(spriter.split[0]), Convert.ToInt32(spriter.split[1]));
                        //Program.avalron.chatting.addSystemText("호수의 여인 카드가 발동되었습니다.");
                        //Program.avalron.chatting.addSystemText("ㅁㅁㅁ 가 ㅁㅁㅁ에게 사용하였습니다.");
                        break;
                    case (int)EtcSpecialOpCode.OtherLadyOfTheLake:
                        Program.avalron.OtherladyOfTheLakeResult(Convert.ToInt32(spriter.split[0]), Convert.ToInt32(spriter.split[1]));
                        break;
                    case (int)EtcSpecialOpCode.MyLadyOfTheLake:
                        Program.avalron.setPhaseState(Avalron.PhaseState.MyLadyOfTheLake);
                        Program.avalron.ladyOfTheLakeStart(Convert.ToInt32(spriter.split[0]));
                        break;
                    case (int)EtcSpecialOpCode.MerlinAssassinate:
                        Program.avalron.merlinAssassinate(Convert.ToInt32(spriter.split[0]), Convert.ToInt32(spriter.split[1]));
                        break;
                    case (int)GameComplete.GameEnd:
                        Program.avalron.gameEnd(Convert.ToInt32(spriter.split[0]));
                        break;
                    case (int)ChattingOpCode.CHATSEND:
                        string[] parameter;
                        string chatLine;
                        parameter = getString.Substring(5).Split('\u0001');
                        chatLine = parameter[0] + " : " + parameter[1];
                        Program.avalron.chatting.addText(chatLine);
                        break;
                    case (int)ChattingOpCode.ChattingOn:
                        Program.avalron.chatting.chattingOnOff(true);
                        break;
                    case (int)ChattingOpCode.ChattingOff:
                        Program.avalron.chatting.chattingOnOff(false);
                        break;
                    default:
                        break;
                }
                
            }
        }
   }
}
