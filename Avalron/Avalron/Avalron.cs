using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Avalron.Avalron.Server;

namespace Avalron.Avalron
{
    public partial class Avalron : Form
    {
        public enum Team { None, Evil, Good };
        public AvalronClient gameClient;
        Profile[] profile;
        public VoteTrack voteTrack = new VoteTrack(5);         // 투표 바입니다.
        public RoundTrack roundTrack = new RoundTrack(5);      // 
        GroupBox Track = new GroupBox();
        public Chatting chatting;
        Thread GetClient;
        int maxnum;     //
        AvalronUserInfo user = new AvalronUserInfo(Program.userInfo.nick, Program.userInfo.index);
        bool isServer = false;
        int leader = 0;             // 이전 리더의 인덱스 번호입니다.
        bool isLeader = false;      // 자신이 원정 대장인지의 값입니다.
        int myIndex = -1;

        public bool enableClick
        {
            get; set;
        }
        static public int ClickCnt = 0;
        Thread serverThread;
        ClientServer server;
        GameServer gameServer;
        public PlayerInfo playerInfo;

        // 원정대원의 값입니다.
        public int teamCnt
        {
            get; set;
        }
        public int teamMaxNum
        {
            get; set;
        }

        // 투표 결과값입니다.
        public bool vote
        {
            get; set;
        }

        public bool questSelect
        {
            get; set;
        }

        public enum PhaseState : int { TeamBuilding = 1, Vote, MyQuest, OtherQuest, MyLadyOfTheLake, OtherLadyOfTheLake, MyMerlinAssassinate, OtherMerlinAssassinate};
        public PhaseState phaseState;

        public Avalron(string[] ips, AvalonServer.TcpUserInfo[] userInfo)
        {

            InitializeComponent();

            if (1 != ips.Length)
                isServer = true;

            TitleBar titleBar = new TitleBar(this);

            // 현재 인원수를 서버와 통신하여 가져 옵니다.  -> useInfo의 개수로 변경.
            int max_num = 0;
            for(int i =0; i < userInfo.Length; i++)
            {
                if (null == userInfo[i])
                    break;
                max_num++;
            }
            if (false)
                //if (max_num > 10 || max_num < 6)
                throw new Exception("max_num 에러입니다." + max_num);
            profile = new Profile[max_num];
            maxnum = max_num;

            for (int i = 0; i < profile.Length; i++)
            {
                profile[i] = new Profile(this.Controls, i, userInfo[i].userNick, userInfo[i].userIndex);

                //profile[i].SetTeam();
                if (Program.userInfo.index == profile[i].index)
                    myIndex = i;
            }
            chatting = new Chatting(Controls);

            for (int i = 0; i < ips.Length; i++)
                ips[i] = ips[i].Split(':')[0];

            if (isServer)
            {

                server = new ClientServer(ips, userInfo);
                serverThread = new Thread(new ThreadStart(server.serverSetting));

                gameServer = new GameServer(server.getClientCount(), userInfo);

                gameServer.setServer(server);
                server.setGameServer(gameServer);
                gameServer.gameInit();

                serverThread.Start();
            }
            //while (0 = server.state)
            {
                Thread.Sleep(5000);
            }
            gameClient = new AvalronClient(ips[0], 9051);

            voteTrack.SetPosition(new Point(737, 400));
            voteTrack.SetCollection(this.Controls);
            //voteTrack.Next();

            roundTrack.SetPosition(new Point(737, 300));
            roundTrack.SetCollection(this.Controls);
            
            Track.BackgroundImage = Properties.Resources.Avalon_TrackBG;
            Track.BackgroundImageLayout = ImageLayout.Stretch;
            Track.Size = new Size(360 ,252);
            Track.Location = new Point(737, 273);
            Track.BackColor = Color.Transparent;
            this.Controls.Add(Track);
            

            GetClient = new Thread(new ThreadStart(gameClient.avalronRecv));
            GetClient.Start();

            enableClick = false;
            teamCnt = 0;

            memo.Text = "메모장입니다. 자유롭게 작성하세요. 저장기능 x ..;; ㅋㅋㅋ 누르면 사라지는건 덤 ㅋㅋㅋ!!!";
            // 서버에서 현재 인원수, 방정보 받아옴.
            // 서버에서 타인의 유저정보를 가져옴. (Nick, 순서 등)
            // 서버에서 자신의 유저정보를 가져옴.
            // 서버에서 누가 먼저 시작하는지를 받아옴.
            // 쓰레드를 통하여 게임을 시작함.
        }

        ~Avalron()
        {
            gameClient.Close();
            if (GetClient.IsAlive)
                GetClient.Abort();
        }

        static public int getRand(int max, int min = 0)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        // 게임 진행시 false , 게임 종료시 true 
        private bool IsGameEnd()
        {
            if (WhoWin() != Team.None)
                return true;
            return false;
        }

        private Team WhoWin()
        {
            // 3번 승리시 -> 선
            if (roundTrack.successful == 3) return Team.Good;

            // 3번 실패시 -> 악
            if (roundTrack.fail == 3) return Team.Evil;
            // 5번 투표가 계속 거절될 시 원정대를 구성하지 못하였음 -> 악
            if (voteTrack.rejected == 5) return Team.Evil;

            // 계속 진행
            return Team.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vote vote = new Vote();
            vote.StartPosition = FormStartPosition.CenterParent;
            vote.ShowDialog();

            MessageBoxEx.Show(this, vote.getResult().ToString());
        }

        static bool first = true;
        private void memo_Enter(object sender, EventArgs e)
        {
            if (false == first)
                return;

            memo.Text = "";
            first = false;
        }

        public void gameEnd(int state)
        {
            int resultState = 0;
            if (0 == state)
            {
                resultState = 2;
                MessageBox.Show("패배하였습니다.");
            }
            else if (1 == state)
            {
                resultState = 1;
                MessageBox.Show("승리하였습니다.");
            }
            else
                MessageBox.Show("알수없는 게임 종료 코드 " + state);

            try {
                if (this.InvokeRequired)
                    this.Invoke(new SetOwnCard(gameEnd), new object[] { state });
                else
                {
                    Close();

                    Program.tcp.DataSend((int)TCPClient.AvalronOpCode.GAME_END, Program.userInfo.index + TCPClient.delimiter + resultState);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public void selectQuestTeamStart(int teamMaxNum, int failCntRequired)
        {
            this.teamMaxNum = teamMaxNum;

            if (2 == failCntRequired)
            {
                chatting.addSystemText("이번 라운드는 실패 카드가 2장 이상 나와야 실패가 됩니다.");
                chatting.addSystemText("실패 카드가 한 장만 나온다면 임무에 성공한 것으로 간주합니다.");
            }

            if(isLeader)
                enableClick = true;
            else
                enableClick = false;

            teamNumShow(teamMaxNum, teamCnt);
            questTeamClear();
        }

        public void selectQuestTeam(int index)
        {
            if (true == profile[index].team)
                MessageBox.Show("이미 선택되어 있는 원정대원입니다.");

            profile[index].SetTeam();
            teamCnt++;
            teamNumShow(teamMaxNum, teamCnt);

            if (teamCnt == teamMaxNum)
                setTeamBuildBtnEnable(true);
        }

        public void deSelectQuestTeam(int index)
        {
            if (false == profile[index].team)
                MessageBox.Show("이미 선택해제 되있는 원정대원입니다.");

            profile[index].TeamClear();

            if(teamCnt > 0)
                teamCnt--;

            teamNumShow(teamMaxNum, teamCnt);

            setTeamBuildBtnEnable(false);
        }

        public void questTeamClear()
        {
            for(int i =0; i < profile.Length; i++)
                profile[i].TeamClear();

            teamCnt = 0;
            setTeamBuildBtnEnable(false);

            clickAllClear();
        }

        public void clickAllClear()
        {
            for (int i = 0; i < profile.Length; i++)
                profile[i].clickClear();

            teamCnt = 0;
            setTeamBuildBtnEnable(false);
        }

        // index에 리더를 정합니다.
        public void SetLeader(int index)
        {
            profile[leader].LeaderClear();
            profile[index].SetLeader();
            leader = index;
            setTeamBuildBtn(false);
            isLeader = false;

            // 자신이 리더이면 원정대원을 선발합니다.
            if(myIndex == index)
            {
                isLeader = true;
                setTeamBuildBtn(true);
                //enableClick = true;       // 원정 대원 인원수가 온 후 되게 변경.
            }

            questTeamClear();
        }

        private delegate void Delegate(bool state);

        public void setTeamBuildBtn(bool state)
        {
            try
            {
                if (this.TeamBuildCompleteButton.InvokeRequired)
                {
                    TeamBuildCompleteButton.Invoke(new Delegate(setTeamBuildBtn), new object[] { state });
                }
                else
                {
                    TeamBuildCompleteButton.Visible = state;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        delegate void SetOwnCard(int card);

        public void setOwnCard(int card)
        {
            try
            {
                if (ownCard.InvokeRequired)
                {
                    ownCard.Invoke(new SetOwnCard(setOwnCard), new object[] { card });
                }
                else
                {
                    switch (card)
                    {
                        case (int)CharacterCard.Card.Merlin:
                            ownCard.BackgroundImage = Properties.Resources.MERLIN;
                            ManualBox.Text = Properties.Resources.M_MURLIN;
                            break;
                        case (int)CharacterCard.Card.Percival:
                            ownCard.BackgroundImage = Properties.Resources.PERCEVAL;
                            ManualBox.Text = Properties.Resources.M_PERCEVAL;
                            break;
                        case (int)CharacterCard.Card.ArtherServant1:
                            ownCard.BackgroundImage = Properties.Resources.ArtherServant1;
                            ManualBox.Text = Properties.Resources.M_SERVANT;
                            break;
                        case (int)CharacterCard.Card.ArtherServant2:
                            ownCard.BackgroundImage = Properties.Resources.ArtherServant2;
                            ManualBox.Text = Properties.Resources.M_SERVANT;
                            break;
                        case (int)CharacterCard.Card.ArtherServant3:
                            ownCard.BackgroundImage = Properties.Resources.ArtherServant3;
                            ManualBox.Text = Properties.Resources.M_SERVANT;
                            break;
                        case (int)CharacterCard.Card.ArtherServant4:
                            ownCard.BackgroundImage = Properties.Resources.ArtherServant4;
                            ManualBox.Text = Properties.Resources.M_SERVANT;
                            break;
                        case (int)CharacterCard.Card.ArtherServant5:
                            ownCard.BackgroundImage = Properties.Resources.ArtherServant5;
                            ManualBox.Text = Properties.Resources.M_SERVANT;
                            break;
                        case (int)CharacterCard.Card.Assassin:
                            ownCard.BackgroundImage = Properties.Resources.ASSASSIN;
                            ManualBox.Text = Properties.Resources.M_ASSASSIN;
                            break;
                        case (int)CharacterCard.Card.Mordred:
                            ownCard.BackgroundImage = Properties.Resources.MORDRED;
                            ManualBox.Text = Properties.Resources.M_MODREAD;
                            break;
                        case (int)CharacterCard.Card.Morgana:
                            ownCard.BackgroundImage = Properties.Resources.MORGANE;
                            ManualBox.Text = Properties.Resources.M_MOREGANA;
                            break;
                        case (int)CharacterCard.Card.Oberon:
                            ownCard.BackgroundImage = Properties.Resources.Oberon;
                            ManualBox.Text = Properties.Resources.M_MINIION;
                            break;
                        case (int)CharacterCard.Card.MordredMiniion1:
                            ownCard.BackgroundImage = Properties.Resources.MordredMiniion1;
                            ManualBox.Text = Properties.Resources.M_MINIION;
                            break;
                        case (int)CharacterCard.Card.MordredMiniion2:
                            ownCard.BackgroundImage = Properties.Resources.MordredMiniion2;
                            ManualBox.Text = Properties.Resources.M_MINIION;
                            break;
                        case (int)CharacterCard.Card.MordredMiniion3:
                            ownCard.BackgroundImage = Properties.Resources.MordredMiniion3;
                            ManualBox.Text = Properties.Resources.M_MINIION;
                            break;
                        case (int)CharacterCard.Card.separatrix:
                        default:
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        public void setTeamBuildBtnEnable(bool state)
        {
            try
            {
                if (this.TeamBuildCompleteButton.InvokeRequired)
                    TeamBuildCompleteButton.Invoke(new Delegate(setTeamBuildBtnEnable), new object[] { state });
                else
                    TeamBuildCompleteButton.Enabled = state;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private delegate void TeamStrDelegate(int MaxNum, int teamCnt);

        private void teamNumShow(int MaxNum, int teamCnt)
        {
            try
            {
                if (labelTeamStr.InvokeRequired)
                    labelTeamStr.Invoke(new TeamStrDelegate(teamNumShow), new object[] { MaxNum, teamCnt });
                else
                {
                    labelTeamStr.Text = "총 " + MaxNum + "중 " + teamCnt + "명 선택되었습니다.";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        // 악의 세력을 표시해줍니다.
        public void evilShow(int index)
        {
            profile[index].setEvil();
        }

        // 투표 결과를 보여줍시다.
        public void voteShow(int index, bool vote)
        {
            profile[index].voteShow(vote);
        }

        private void receiveFunction()
        {
            string getString;
            while ((Program.state % 10) == 3)
            {
                int dataleng; 
                byte[] bData;

                Program.tcp.ReciveBData(out bData, out dataleng);

                getString = Encoding.UTF8.GetString(bData);

                Spriter spriter = new Spriter(getString);
                int opCode = spriter.getJustOpCode();

                switch (opCode)
                {
                    case (int)TCPClient.AvalronOpCode.GAME_END:
                        MessageBox.Show(getString);
                        MemoryStream ms = new MemoryStream();
                        ms.Write(bData, 5, dataleng - 5);
                        ms.Position = 0;

                        if (spriter.split[0] != "0")
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            Program.state = 32;
                            AvalonServer.RoomInfo roomInfo = (AvalonServer.RoomInfo)bf.Deserialize(ms);
                            Program.room = new WaitingRoom(roomInfo);
                        }
                        else
                        {
                            MessageBox.Show("방 들어가기 에러 : " + getString);
                        }
                        Program.state = 32;
                        return;
                        //MessageBox.Show("게임 끝");
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine(getString);
                        //MessageBox.Show("avalron처리되지 않은 코드 " + getString);

                        break;
                }
            }
        }

        // 게임시작  카드 정보를 보여줌.
        public void gameStart()
        {
            string teamKor = CharacterCard.teamToString(playerInfo.getCard());
            string cardKor = CharacterCard.cardToString(playerInfo.getCard());
            chatting.addSystemText("레지스탕스 아발론에 오신걸 환영합니다.");
            chatting.addSystemText("당신은 " + teamKor + "에 속해있습니다.");
            chatting.addSystemText("당신은 [" + cardKor + "] 입니다.");
        }

        // 호수의 여인을 누가 얻었는지를 보여줍니다.
        public void ladyOfTheLakeShow(int index)
        {
            string nick = profile[index].nick;
            chatting.addSystemText(nick + "님이 호수의 여인을 획득하셨습니다.");
        }

        // 호수의 여인 페이즈시작되었을시입니다.
        public void ladyOfTheLakeStart(int index)
        {
            if (myIndex == index)
            {
                chatting.addSystemText("당신은 호수의 여인 카드를 소지하고 있습니다.");
                chatting.addSystemText("사용할 대상을 클릭해주세요");
                enableClick = true;
            }
            else
            {
                chatting.addSystemText("당신은 호수의 여인의 카드를 소지하고 있지 않습니다.");
                enableClick = false;
            }
            
        }

        public int ladyOfTheLakeIndex = -1;
        // 호수의 여인 사용시도후 실패시.
        public void ladyOfTheLakeFail()
        {
            if (ladyOfTheLakeIndex == myIndex)
                MessageBox.Show("자신에게 사용할 수 없습니다.");
            else
                MessageBox.Show("이미 소지한적 있는 사람입니다.");
        }

        // 원정 결과를 보여줍니다.
        public void questionShow(int failCnt)
        {
            roundTrack.SetResult(failCnt);
            chatting.addSystemText(teamMaxNum + "명 중 " + failCnt + "명이 실패를 눌렀습니다.");
        }

        // 자신이 호수의 여인을 사용했을시 결과
        public void ladyOfTheLakeResult(int index, int team)
        {
            string nick = profile[index].nick;
            chatting.addSystemText("호수의 여인 카드 사용!");

            string teamStr = "teamStr기본값";
            if (0 == team)
            {
                teamStr = "악의팀";
            }
            else
            {
                teamStr = "선의팀";
            }
            chatting.addSystemText(nick + "님은 " + teamStr + "입니다.");
        }

        // 타인이 사용했음을 나타내는 함수.
        public void OtherladyOfTheLakeResult(int toIndex, int fromIndex)
        {
            string fromNick = profile[fromIndex].nick;
            string toNick = profile[toIndex].nick;
            //profile[toIndex].set

            chatting.addSystemText(fromNick + "님이 " + toNick + "님에게 호수의 여인 카드를 사용했습니다.");
        }

        // 멀린의 암살 시도 페이즈로 돌입.
        public void merlinAssassinateStart()
        {
            chatting.addSystemText("선이 승리하였습니다.");
            chatting.addSystemText("하지만 악에게 마지막 기회가 있습니다.");

            if((int)CharacterCard.Card.Assassin == playerInfo.getCard())
            {
                setPhaseState(PhaseState.MyMerlinAssassinate);
                chatting.addSystemText("멀린을 암살해주세요.");
            }
            else
            {
                setPhaseState(PhaseState.OtherMerlinAssassinate);
                chatting.addSystemText("멀린 암살을 기다립니다.");
            }
        }

        // 멀린 암살 시도 결과입니다.
        public void merlinAssassinate(int success, int assassinIndex, int chosenIndex, int merlinIndex)
        {
            string merlinNick = profile[merlinIndex].nick;
            string assassinNick = profile[assassinIndex].nick;
            string chosenNick = profile[chosenIndex].nick;

            chatting.addSystemText("암살자 " + assassinNick + "님이 " + chosenNick + "님을 암살했습니다.");
            chatting.addSystemText(chosenNick + "님은 멀린이 .......");

            Random a = new Random();
            int rand = a.Next(3000, 10000);
            Thread.Sleep(rand);

            string result = "기본값...????? 에러";

            if (0 == success)
                result = "아닙니다.";
            else if (1 == success)
                result = "맞습니다.";

            chatting.addSystemText(result);
        }

        // 원정대원을 보낼지 투표.
        public void Vote(string title)
        {
            Vote vote = new Vote();
            vote.ShowDialog();

            int num = -1;

            if (vote.getResult())
                num = 1;
            else
                num = 0;

            gameClient.DataSend((int)AvalronClient.VoteOpCode.Voting, num.ToString());
        }

        // 원정대원들이 원정에 대해 성공 실패
        public void questStart()
        {
            QuestSelect questSelect = new QuestSelect(playerInfo.getCard());
            questSelect.ShowDialog();
            int num = -1;

            if (questSelect.getResult())
                num = 1;
            else
                num = 0;

            gameClient.DataSend((int)AvalronClient.VoteOpCode.Questing, num.ToString());
        }

        // 원정대 선택완료클릭시 네트워크 송신합니다.
        private void TeamBuildCompleteButton_Click(object sender, EventArgs e)
        {
            gameClient.DataSend((int)AvalronClient.TeamBuildingOpCode.TeamComplete, "");
            TeamBuildCompleteButton.Enabled = false;
        }

        public void setPhaseState(PhaseState state)
        {
            chatting.addSystemText(state.ToString() + "페이즈 입니다.");

            phaseState = state;

            switch(state)
            {
                case PhaseState.MyLadyOfTheLake:
                    return;
                case PhaseState.MyMerlinAssassinate:
                case PhaseState.MyQuest:
                    enableClick = true;
                    break;
                case PhaseState.OtherLadyOfTheLake:
                    if (isLeader)
                        enableClick = true;
                    else
                        enableClick = false;
                    break;
                case PhaseState.TeamBuilding:
                    if (isLeader)
                        enableClick = true;
                    else
                        enableClick = false;
                    break;
                default:
                    enableClick = false;
                    break;
            }

        }
    
        // 카드를 클릭하면 메뉴얼이 뜬다.
        private void ownCard_Click(object sender, EventArgs e)
        {
            ManualBox.Visible = true;
        }

        private void ManualBox_Click(object sender, EventArgs e)
        {
            ManualBox.Visible = false;
        }
    }
}
