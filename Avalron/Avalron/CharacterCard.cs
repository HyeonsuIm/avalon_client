using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron.Avalron
{
    public class CharacterCard
    {
        public enum Card
        {
            Merlin, Percival, ArtherServant1, ArtherServant2, ArtherServant3, ArtherServant4, ArtherServant5, separatrix,
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
                case 1:
                    GoodTeamNum = 1;
                    EvilTeamNum = 0;
                    break;
                case 2:
                    GoodTeamNum = 2;
                    EvilTeamNum = 0;
                    break;
                case 3:
                    GoodTeamNum = 2;
                    EvilTeamNum = 1;
                    break;
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
                    //throw new Exception("Character Card : 잘못된 사람의 수입니다." + max_num);
                    break;
            }
        }

        public void TeamSetting(ref PlayerInfo[] memberList)
        {
            int memberCount = GoodTeamNum + EvilTeamNum;
            int[] cardList = new int[memberCount];
            cardSetting(ref cardList, memberCount);

            for(int i=0;i< memberCount; i++)
            {
                memberList[i].setCard(cardList[i]);
                // 1 : 선
                // 2 : 악
                if (cardList[i] < 7)
                    memberList[i].setTeam(1);
                else
                    memberList[i].setTeam(2);
            }
            // 위치대로 할당 => 팀 자동으로 나뉨
            // 7초과는 악, 7미만은 선
            
        }

        void cardSetting(ref int[] cardList, int memberCount)
        {
            cardList = new int[10];
            cardList[0] = (int)Card.Merlin;
            cardList[1] = (int)Card.Mordred;
            cardList[2] = (int)Card.Assassin;
            cardList[3] = (int)Card.Percival;
            cardList[4] = (int)Card.ArtherServant1;
            cardList[5] = (int)Card.ArtherServant2;
            cardList[6] = (int)Card.Morgana;
            cardList[7] = (int)Card.ArtherServant3;
            cardList[8] = (int)Card.ArtherServant4;
            cardList[9] = (int)Card.MordredMiniion1;

            shuffle(ref cardList, memberCount);
            
        }

        void shuffle(ref int[] cardList, int memberCount) {

            Random r = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < r.Next(10, 50); i++)
            {
                int swap1 = r.Next(0, memberCount);
                int swap2 = r.Next(0, memberCount);
                if (swap1 == swap2)
                    continue;
                else
                {
                    int temp = cardList[swap1];
                    cardList[swap1] = cardList[swap2];
                    cardList[swap2] = temp;
                }
            }
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

        public static string cardToString(int cardNum)
        {
            switch(cardNum)
            {
                case (int)Card.Merlin:
                    return "멀린";
                case (int)Card.Percival:
                    return "퍼시벌";
                case (int)Card.ArtherServant1:
                case (int)Card.ArtherServant2:
                case (int)Card.ArtherServant3:
                case (int)Card.ArtherServant4:
                case (int)Card.ArtherServant5:
                    return "아서왕의 종복";
                case (int)Card.Assassin:
                    return "암살자";
                case (int)Card.Mordred:
                    return "모드레드";
                case (int)Card.Morgana:
                    return "모르가나";
                case (int)Card.Oberon:
                    return "오베론";
                case (int)Card.MordredMiniion1:
                case (int)Card.MordredMiniion2:
                case (int)Card.MordredMiniion3:
                    return "모드레드의 하수인";
                case (int)Card.separatrix:
                default:
                    return "잘못된 카드";
            }
        }

        // 선의 세력인지 악의 세력인지를 반환합니다.
        public static string teamToString(int team)
        {
            if (team < (int)Card.separatrix)
                return "선의 세력";
            else if (team > (int)Card.separatrix)
                return "악의 세력";

            return "기본값....???";
        }
    }

}
