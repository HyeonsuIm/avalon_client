using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron.Avalron
{
    class AvalronUserInfo : UserInfo
    {
        bool Host = false;              // 호스트, 방장(서버)인지 여부
        bool Leader = false;            // 원정대장인지 여부
        bool IsTeam = false;            // 원정대에 속해있는지의 여부.
        bool Vote = false;              // 그 라운드의 투표 결과를 저장.
        Avalron.Team Team = Avalron.Team.None;  // 자신이 속한 팀이 어딘지 나타냅니다.
        Avalron.PersonCard personCard;
        int idNumber;                   // id의 일련번호

        public AvalronUserInfo(string id, string nick) : base(id, nick)
        {

        }

        public Avalron.Team team
        {
            get
            {
                return Team;
            }
        }

        public bool leader
        {
            get
            {
                return Leader;
            }
            set
            {
                Leader = value;
            }
        }

        public bool isTeam
        {
            get
            {
                return IsTeam;
            }
            set
            {
                IsTeam = value;
            }
        }
    }
}
