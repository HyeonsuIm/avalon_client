using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron.Avalron
{
    public class PlayerInfo
    {
        int card;
        int team;
        int loc;
        UserInfo info;

        public void setCard(int card)
        {
            this.card = card;
        }
        public void setTeam(int team)
        {
            this.team = team;
        }
    }
}
