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
                        Program.avalron.GameStart();
                        break;
                    case (int)TeamBuildingOpCode.TeamMemberNum:
                        break;
                    case (int)TeamBuildingOpCode.TeamSelect:
                        break;
                    case (int)TeamBuildingOpCode.TeamDeSelect:
                        break;
                    case (int)TeamBuildingOpCode.TeamComplete:
                        break;
                    case (int)VoteOpCode.StartVote:
                        break;
                    case (int)VoteOpCode.Voting:
                        break;
                    case (int)VoteOpCode.VoteResult:
                        break;
                    case (int)EtcSpecialOpCode.LadyOfTheLake:
                        break;
                    case (int)EtcSpecialOpCode.MerlinAssassinate:
                        break;
                    case (int)ChattingOpCode.CHATSEND:
                        Program.avalron.chatting.addText(getString);
                        break;
                    case (int)ChattingOpCode.ChattingOn:
                        break;
                    case (int)ChattingOpCode.ChattingOff:
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
