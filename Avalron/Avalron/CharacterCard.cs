using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron.Avalron
{
    class CharacterCard
    {
        public enum Card
        {
            Merlin, Percival, ArtherServant1, Artherservant2, Artherservant3, Artherservant4, Artherservant5, separatrix,
            Assassin, Mordred, Morgana, Oberon, MordredMiniion1, MordredMiniion2, MordredMiniion3
        };
        public enum GoodTeam
        {
            Merlin, Percival, ArtherServant1, ArtherServant2, ArtherServant3, ArtherServant4, ArtherServant5
        }
        public enum EvilTeam
        {
            Assassin, Mordred, Morgana, Oberon, MordredMiniion1, MordredMiniion2, MordredMiniion3
        }

        int GoodTeamNum;
        int EvilTeamNum;
        int[] PersonIndex;

        public CharacterCard(int max_num)
        {
            DetermineTeamNum(max_num);

        }

        private void DetermineTeamNum(int max_num)
        {
            switch (max_num)
            {
                case 5:
                    GoodTeamNum = 3;
                    EvilTeamNum = 2;
                    break;
                case 6:
                    GoodTeamNum = 4;
                    EvilTeamNum = 2;
                    break;
                case 7:
                    GoodTeamNum = 4;
                    EvilTeamNum = 3;
                    break;
                case 8:
                    GoodTeamNum = 5;
                    EvilTeamNum = 3;
                    break;
                case 9:
                    GoodTeamNum = 6;
                    EvilTeamNum = 3;
                    break;
                case 10:
                    GoodTeamNum = 6;
                    EvilTeamNum = 4;
                    break;

                default:
                    throw new Exception("Character Card : 잘못된 사람의 수입니다." + max_num);
            }
        }

        private void TeamSetting()
        {

        }

        private void GetTeam(int TeamMax, Avalron.Team team)
        {
            int[] teamCard;
            if (team == Avalron.Team.None)
                throw new Exception("팀이 선택되지 않았습니다.");
            if (team == Avalron.Team.Evil)
                teamCard = new int[Enum.GetNames(typeof(EvilTeam)).Length];
            else
                teamCard = new int[Enum.GetNames(typeof(GoodTeam)).Length];

            int Cnt = 0;
            int temp = -1;
            do
            {
                temp = GetCard(team);
                teamCard[Cnt] = temp;
                Cnt++;

                if (Cnt > TeamMax)
                    break;

            } while (IsExist(teamCard, temp, Cnt) == false);
        }

        private int GetCard(Avalron.Team team)
        {
            if (team == Avalron.Team.Evil)
                return Avalron.getRand(Enum.GetNames(typeof(EvilTeam)).Length);
            else
                return Avalron.getRand(Enum.GetNames(typeof(GoodTeam)).Length);
        }

        private bool IsExist(int[] teamCard, int temp, int Cnt)
        {
            for(int i =0; i< Cnt; i++)
            {
                if (temp == teamCard[i])
                    return true;
            }
            return false;
        }
    }

    class Character
    {
        int PersonIndex;
        CharacterCard.Card card;
    }
}
