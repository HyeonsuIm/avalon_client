using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron.Avalron
{
    class Server
    {
        int leaderIDNum;

        void Start()
        {
            GetInformation();
            if(0 != SystemCheck())
            {
                MessageBox.Show("서버준비에 에러가 발생했습니다.");
            }

            Thread ChatThread = new Thread(Chat);
            ChatThread.Start();

            if(IsEnd())
            {

            }
        }

        void Chat()
        {
            while(true)
            {
                
            }
        }

        void GetInformation()
        {
            // 접속하는 사람의 인수는?
            // 접속하는 사람의 IP, Nick, ID, ID일련번호 에 대한 정보 받기
        }

        int SystemCheck()
        {
            // 사용자가 모두 접속하였는가?
            // 사용자와 서버는 모두 통신이 가능한가?
            return 0;
        }

        bool IsEnd()
        {
            return false;
        }
    }

    class PInformation
    {
        string Address;
        string Nick;
        string ID;
        int IDNum;
        Avalron.PersonCard Card;
        bool IsLeader;

    }
}
