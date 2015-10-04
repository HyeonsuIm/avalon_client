using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron.Avalron
{
    public class AvalronClient : TCPClient
    {
        enum GameOpCode { CardInfo = 101, SelectLeader, GameStart };
        enum TeamBuildingOpCode { TeamMemberNum = 200, TeamSelect, TeamDeSelect, TeamComplete };
        enum VoteOpCode { StartVote = 300, Voting, VoteResult };
        enum EtcSpecialOpCode { LadyOfTheLake = 400, MerlinAssassinate };
        enum ChattingOpCode { CHATSEND = 800, ChattingOn, ChattingOff };

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
                    getString = Program.tcp.ReciveData() + "\n";
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
                        Program.avalron.playerInfo = new PlayerInfo();
                        Program.avalron.playerInfo.setCard(Convert.ToInt32(spriter.split[0]));

                        // 능력에 대한 추가정보를 처리합니다.
                        if(1 != spriter.getCnt()+1)
                            if (false) { int i = 0; }
                        break;
                    case (int)GameOpCode.SelectLeader:
                        Program.avalron.SetLeader(Convert.ToInt32(spriter.split[0]));
                        break;
                    case (int)GameOpCode.GameStart:
                        Program.avalron.chatting.addSystemText("게임을 시작합니다.");
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
                        Program.avalron.chatting.addSystemText("원정 선택이 완료되었습니다.");
                        break;
                    case (int)VoteOpCode.StartVote:
                        break;
                    case (int)VoteOpCode.Voting:
                        break;
                    case (int)VoteOpCode.VoteResult:
                        break;
                    case (int)EtcSpecialOpCode.LadyOfTheLake:
                        Program.avalron.chatting.addSystemText("호수의 여인 카드가 발동되었습니다.");
                        Program.avalron.chatting.addSystemText("ㅁㅁㅁ 가 ㅁㅁㅁ에게 사용하였습니다.");
                        break;
                    case (int)EtcSpecialOpCode.MerlinAssassinate:
                        break;
                    case (int)ChattingOpCode.CHATSEND:
                        Program.avalron.chatting.addText(getString);
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
                
                // 채팅 금지시
                if(false)
                {
                    Program.avalron.chatting.ChatBoxEnabled = false;
                }

                // 채팅 금지 해지시
                if(false)
                {
                    Program.avalron.chatting.ChatBoxEnabled = true;
                }
            }
        }
   }
}
