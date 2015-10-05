using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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
        public Chatting chatting;
        Thread GetClient;
        bool closing = false;
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

            voteTrack.SetPosition(new Point(30, 150));
            voteTrack.SetCollection(this.Controls);
            //voteTrack.Next();

            roundTrack.SetPosition(new Point(500, 150));
            roundTrack.SetCollection(this.Controls);

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

        private void memo_Enter(object sender, EventArgs e)
        {
            memo.Text = "";
        }

        private void GameEnd()
        {
            if (WhoWin() == user.Team)
            {
                // 선의 승리입니다.
                MessageBoxEx.Show("멀린 암살시도 중");
                // 서버로부터 멀린이 암살되었는지 받아옴. -> 암살시 패배.
                if (true)
                {
                    //암살되면
                    MessageBoxEx.Show("악의 승리입니다.");
                }
                else
                {
                    MessageBoxEx.Show("선의 승리입니다.");
                }
                return;
            }
            // 악의 승리입니다.
            // 자신이 악의 세력일 경우 // 멀린 암살 투표시작
            // 암살자가 멀린을 암살합니다. -> 여기서 자신이 악의 세력인지를 서버가 알려줘야 하는가? 지목은 어떤식으로?
            if (user.Card == CharacterCard.Card.Assassin)
            {
                // 암살 성공시 승리
                Vote assassinVote = new Vote();
                assassinVote.Show();

                // 투표 결과를 전송합니다.
                assassinVote.getResult();

                // 서버로 부터 맞췄는지를 보여줍니다.
                if (true)
                {
                    // 악 세력의 승리
                    MessageBoxEx.Show("악의 승리입니다.");
                }
                else
                {
                    // 선 세력의 승리
                    MessageBoxEx.Show("선의 승리입니다.");
                }
            }

        }

        public void gameEnd(int state)
        {
            if (0 == state)
                MessageBox.Show("패배하였습니다.");
            else if (1 == state)
                MessageBox.Show("승리하였습니다.");
            else
                MessageBox.Show("알수없는 게임 종료 코드 " + state);
        }

        public void selectQuestTeamStart(int teamMaxNum)
        {
            this.teamMaxNum = teamMaxNum;

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
                            break;
                        case (int)CharacterCard.Card.Percival:
                            ownCard.BackgroundImage = Properties.Resources.PERCEVAL;
                            break;
                        case (int)CharacterCard.Card.ArtherServant1:
                            ownCard.BackgroundImage = Properties.Resources.ArtherServant1;
                            break;
                        case (int)CharacterCard.Card.ArtherServant2:
                            ownCard.BackgroundImage = Properties.Resources.ArtherServant2;
                            break;
                        case (int)CharacterCard.Card.ArtherServant3:
                            ownCard.BackgroundImage = Properties.Resources.ArtherServant3;
                            break;
                        case (int)CharacterCard.Card.ArtherServant4:
                            ownCard.BackgroundImage = Properties.Resources.ArtherServant4;
                            break;
                        case (int)CharacterCard.Card.ArtherServant5:
                            ownCard.BackgroundImage = Properties.Resources.ArtherServant5;
                            break;
                        case (int)CharacterCard.Card.Assassin:
                            ownCard.BackgroundImage = Properties.Resources.ASSASSIN;
                            break;
                        case (int)CharacterCard.Card.Mordred:
                            ownCard.BackgroundImage = Properties.Resources.MORDRED;
                            break;
                        case (int)CharacterCard.Card.Morgana:
                            ownCard.BackgroundImage = Properties.Resources.MORGANE;
                            break;
                        case (int)CharacterCard.Card.Oberon:
                            ownCard.BackgroundImage = Properties.Resources.Oberon;
                            break;
                        case (int)CharacterCard.Card.MordredMiniion1:
                            ownCard.BackgroundImage = Properties.Resources.MordredMiniion1;
                            break;
                        case (int)CharacterCard.Card.MordredMiniion2:
                            ownCard.BackgroundImage = Properties.Resources.MordredMiniion2;
                            break;
                        case (int)CharacterCard.Card.MordredMiniion3:
                            ownCard.BackgroundImage = Properties.Resources.MordredMiniion3;
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

        // 멀린 또는 모르가나를 표시합니다.
        public void MerlinOrMorganaShow(int index)
        {
            profile[index].setMerlinOrMorgana();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void receiveFunction()
        {
            string getString;
            while ((Program.state % 10) == 3)
            {
                getString = Program.tcp.ReciveData() + "\n";

                Spriter spriter = new Spriter(getString);
                int opCode = spriter.getJustOpCode();

                switch (opCode)
                {
                    case (int)TCPClient.AvalronOpCode.GAME_END:
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

            chatting.addSystemText(fromNick + "님이 " + toNick + "님에게 호수의 여인 카드를 사용했습니다.");
        }

        // 멀린 암살 시도 결과입니다.
        public void merlinAssassinate(int index, int success)
        {
            string result = "멀린 기본값";
            string nick = "예상 멀린 닉";

            nick = profile[index].nick;

            if (0 == success)
                result = "아니었습니다.";
            else if (1 == success)
                result = "맞습니다.";

            chatting.addSystemText(nick + "님은 멀린이" + result);
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
