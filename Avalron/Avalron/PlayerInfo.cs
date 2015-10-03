using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvalonServer;

namespace Avalron.Avalron
{
    public class PlayerInfo
    {
        int card;
        int team;
        int loc;
        TcpUserInfo user;

        public void setUser(TcpUserInfo userInfo)
        {
            user = userInfo;
        }
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
