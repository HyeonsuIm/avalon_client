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

namespace Avalron.Avalron
{
    public partial class Avalron : Form
    {
        public enum Team { None, Evil, Good };
        static public AvalronClient gameClient;
        Profile[] profile;
        VoteTrack voteTrack = new VoteTrack(5);
        RoundTrack roundTrack = new RoundTrack(5);
        Chatting chatting;
        String ServerAddress = "203.255.3.72";
        Thread GetClient;
        bool closing = false;
        int maxnum;
        AvalronUserInfo user = new AvalronUserInfo(Program.userInfo.nick, Program.userInfo.index);
        bool isServer = true;
        int leader = 0;
        bool EnableClick = false;
        static public int ClickCnt = 0;

        public Avalron(int max_num)
        {
            InitializeComponent();

            if (max_num > 10 || max_num < 6)
                throw new Exception("max_num 에러입니다." + max_num);
            profile = new Profile[max_num];
            maxnum = max_num;

            for (int i = 0; i < profile.Length; i++)
            {
                profile[i] = new Profile(this.Controls, i);
                //profile[i].SetTeam();
            }
            chatting = new Chatting(Controls);

            if (isServer)
            {
                Server server = new Server();
            }

            gameClient = new AvalronClient();

            voteTrack.SetPosition(new Point(30, 150));
            voteTrack.SetCollection(this.Controls);
            //voteTrack.Next();

            roundTrack.SetPosition(new Point(500, 150));
            roundTrack.SetCollection(this.Controls);
            roundTrack.SetResult(true);
            roundTrack.SetResult(false);

            GetClient = new Thread(new ThreadStart(chatting.RunGetChat));
            GetClient.Start();

            memo.Text = "메모장입니다. 자유롭게 작성하세요. 저장기능 x ..;; ㅋㅋㅋ";
            // 서버에서 현재 인원수, 방정보 받아옴.
            // 서버에서 타인의 유저정보를 가져옴. (Nick, 순서 등)
            // 서버에서 자신의 유저정보를 가져옴.
            // 서버에서 누가 먼저 시작하는지를 받아옴.
            // 쓰레드를 통하여 게임을 시작함.
            //Thread game = new Thread(new ThreadStart(startGame));
            //game.Start();
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

        public bool enableClick
        {
            get
            {
                return EnableClick;
            }
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
            if (roundTrack.successful == 3)  return Team.Good;

            // 3번 실패시 -> 악
            if (roundTrack.fail == 3) return Team.Evil;
            // 5번 투표가 계속 거절될 시 원정대를 구성하지 못하였음 -> 악
            if (voteTrack.rejected == 5) return Team.Evil;

            // 계속 진행
            return Team.None;
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Chatting.closing = true;
            GetClient.Join();
            Close();
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
            if(user.Team == Team.Evil)
            {
                // 서버로부터 악팀(모드레드 하수인)의 정보를 받습니다.
            } 

            // 멀린은 악의 팀을 알 수 있다.
            if(user.Card == CharacterCard.Card.Merlin)
            {
                // 서버로 부터 악팀의 정보를 받습니다.
            }
            while (IsGameEnd() == false)
            {
                // 퀘스트를 진행합시다.
                do
                {
                    EnableClick = false;    // 팀원 클릭을 할수 없게 만듭니다.

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
                    NextLeader();
                    profile[leader].SetLeader();
                    // 서버 요청.
                    if (voteTrack.Next() == false)     // 5번 연속 부결시 게임 종료.
                    {
                        GameEnd();
                        return;
                    }
                } while (true);    // 원정 투표가 성립할때까지.

                // 원정 시작합니다. 원정 대원을 표시합니다.
                SetQuestTeam(new int[10]);

                if(user.IsTeam)    // 원정 대원이면
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
            if(WhoWin() == user.Team)
            {
                // 선의 승리입니다.
                MessageBoxEx.Show("멀린 암살시도 중");
                // 서버로부터 멀린이 암살되었는지 받아옴. -> 암살시 패배.
                if(true)
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
                if(true)
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

        private void SetQuestTeam(int[] index)
        {
            foreach(int element in index)
            {
                if(element == user.index)
                {
                    // 자신이 원정대원임을 표시.
                    user.IsTeam = true;
                }

                for(int i = 0; i < profile.Length; i++)
                {
                    if(element == profile[i].index)
                    {
                        profile[i].SetTeam();
                        break;
                    }
                }
            }
        }

        private void NextLeader()
        {
            leader++;
            if (leader > maxnum)
                leader = 0; 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
