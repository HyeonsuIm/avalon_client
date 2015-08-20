using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron.Avalron
{
    class AvalronUserInfo : UserInfo
    {
        public Avalron.Team Team    // 자신이 속한 팀이 어딘지 나타냅니다.
        {
            get; set;
        }

        public bool Leader          // 원정대장인지 여부
        {
            get; set;
        }

        public bool IsTeam          // 원정대에 속해있는지의 여부.
        {
            get; set;
        }

        public CharacterCard.Card Card
        {
            get; set;
        }
        bool Host = false;              // 호스트, 방장(서버)인지 여부
        bool Vote = false;              // 그 라운드의 투표 결과를 저장.

        public AvalronUserInfo(string nick, int index) : base(nick, index)
        {
            Host = false;
            Leader = false;
            IsTeam = false;
            Vote = false;
            Team = Avalron.Team.None;
        }
    }
}
