using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Avalron.Avalron
{
    public class AvalronClient : TCPClient
    {
        public enum GameOpCode { CardInfo = 101, SelectLeader, GameStart };
        public enum TeamBuildingOpCode { TeamMemberNum = 200, TeamSelect, TeamDeSelect, TeamComplete };
        public enum VoteOpCode { StartVote = 300, Voting, VoteResult };
        public enum EtcSpecialOpCode { LadyOfTheLake = 400, OtherLadyOfTheLake, MerlinAssassinate };
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
                        if (1 != spriter.getCnt() + 1)
                        {
                            int[] indexs = new int[spriter.getCnt() + 1];
                            for (int i = 1; i < spriter.getCnt() + 1; i++)
                            {
                                indexs[i] = Convert.ToInt32(spriter.split[i]);
                                Program.avalron.evilShow(indexs[i]);
                            }

                            if (1 != Program.avalron.playerInfo.getCard())
                            {
                                foreach(int i in indexs)
                                    Program.avalron.evilShow(i);
                            }
                            else if(2 == Program.avalron.playerInfo.getCard())
                            {
                                foreach (int i in indexs)
                                    Program.avalron.percivalOrMorganaShow(i);
                            }
                        }
                        break;
                    case (int)GameOpCode.SelectLeader:
                        Program.avalron.SetLeader(Convert.ToInt32(spriter.split[0]));
                        break;
                    case (int)GameOpCode.GameStart:
                        Program.avalron.gameStart();
                        break;
                    case (int)TeamBuildingOpCode.TeamMemberNum:
                            Program.avalron.teamMaxNum = Convert.ToInt32(spriter.split[0]);
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
                        break;
                    case (int)VoteOpCode.Voting:
                        break;
                    case (int)VoteOpCode.VoteResult:
                        break;
                    case (int)EtcSpecialOpCode.LadyOfTheLake:
                        Program.avalron.ladyOfTheLakeResult(Convert.ToInt32(spriter.split[0]), Convert.ToInt32(spriter.split[1]));
                        //Program.avalron.chatting.addSystemText("호수의 여인 카드가 발동되었습니다.");
                        //Program.avalron.chatting.addSystemText("ㅁㅁㅁ 가 ㅁㅁㅁ에게 사용하였습니다.");
                        break;
                    case (int)EtcSpecialOpCode.OtherLadyOfTheLake:
                        Program.avalron.OtherladyOfTheLakeResult(Convert.ToInt32(spriter.split[0]), Convert.ToInt32(spriter.split[1]));
                        break;
                    case (int)EtcSpecialOpCode.MerlinAssassinate:
                        Program.avalron.merlinAssassinate(Convert.ToInt32(spriter.split[0]), Convert.ToInt32(spriter.split[1]));
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
