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
        VoteTrack voteTrack = new VoteTrack(5);         // 투표 바입니다.
        RoundTrack roundTrack = new RoundTrack(5);      // 
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
                //Thread.Sleep(5000);
            }
            gameClient = new AvalronClient(ips[0], 9051);

            voteTrack.SetPosition(new Point(30, 150));
            voteTrack.SetCollection(this.Controls);
            //voteTrack.Next();

            roundTrack.SetPosition(new Point(500, 150));
            roundTrack.SetCollection(this.Controls);
            roundTrack.SetResult(true);
            roundTrack.SetResult(false);

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

        private void startGame()
        {
            // 모드레드의 하수인일시 서로를 판별하자
            if (user.Team == Team.Evil)
            {
                // 서버로부터 악팀(모드레드 하수인)의 정보를 받습니다.
            }

            // 멀린은 악의 팀을 알 수 있다.
            if (user.Card == CharacterCard.Card.Merlin)
            {
                // 서버로 부터 악팀의 정보를 받습니다.
            }
            while (IsGameEnd() == false)
            {
                // 퀘스트를 진행합시다.
                do
                {
                    enableClick = false;    // 팀원 클릭을 할수 없게 만듭니다.

                    // 원정대원이 표시되어있다면 모두 해제합시다.
                    for (int i = 0; i < profile.Length; i++)
                    {
                        profile[i].TeamClear();
                        user.IsTeam = false;
                    }
                    // 대표자일시
                    if (user.Leader)
                    {
                        // 누구를 원정보낼지를 선택합시다.
                        // 서버에 전송.
                    }
                    // 서버로부터 누가 원정을 가는지를 받습니다.
                    // 받은 사람을 highlight 합니다.

                    if (false)    // 투표가 가결되었는지?
                    {
                        // 가결됨
                        // vote.rejected = 0; 으로 초기화. 다음 사람으로 넘기기
                        voteTrack.Clear();
                        break;
                    }

                    // 다음 리더 정하기
                    profile[leader].LeaderClear();
                    //NextLeader();
                    profile[leader].SetLeader();
                    // 서버 요청.
                    if (voteTrack.Next() == false)     // 5번 연속 부결시 게임 종료.
                    {
                        GameEnd();
                        return;
                    }
                } while (true);    // 원정 투표가 성립할때까지.

                // 원정 시작합니다. 원정 대원을 표시합니다.
                //SetQuestTeam(new int[10]);

                if (user.IsTeam)    // 원정 대원이면
                {
                    // 투표를 합니다.
                    Vote teamVote = new Vote();
                    teamVote.Show();

                    // 투표 결과를 전송합니다.
                    teamVote.getResult();
                }

                // 투표 결과를 서버로 부터 받아 옵니다.
                roundTrack.SetResult(true); // 매개 변수로 서버에서 받은 결과를 그대로 넣는 방법.
            }

            GameEnd();

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
            teamCnt--;
            teamNumShow(teamMaxNum, teamCnt);

            setTeamBuildBtnEnable(false);
        }

        // index에 리더를 정합니다.
        public void SetLeader(int index)
        {
            profile[leader].LeaderClear();
            profile[index].SetLeader();
            leader = index;
            setTeamBuildBtn(false);

            // 자신이 리더이면 원정대원을 선발합니다.
            if(myIndex == index)
            {
                isLeader = true;
                enableClick = true;
                setTeamBuildBtn(true);
            }
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
            chatting.addSystemText("당신의 카드는 [" + cardKor + "] 입니다.");
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
            chatting.addSystemText("님은 " + teamStr + "입니다.");
        }

        // 타인이 사용했음을 나타내는 함수.
        public void OtherladyOfTheLakeResult(int fromIndex, int toIndex)
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

        public void Vote(string title)
        {
            Vote vote = new Vote();
            vote.Show();
        }
    }
}
