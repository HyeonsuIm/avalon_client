using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron.Login
{
    class LoginClient : TCPClient
    {
        public enum OpCode : int { LOGIN_REQUEST = 10, ID_CHECK, NICK_CHECK, EMAIL_CHECK, REGISTER, FIND_ID, FIND_PW };
        string[] ArrData;

        // 회원가입 될시 0, 실패시 에러코드 반환
        public int Register(string ID, string PW, string Nick, string Email)
        {
            DataSend((int)OpCode.REGISTER, ID + delimiter + PW + delimiter + Nick + delimiter + Email);
            string line;
            while (getString(out line) == false) ;
            Spliter Spliter = new Spliter(line);
            ArrData = Spliter.getSplit();

            IsValidOp(14);

            return Convert.ToInt32(ArrData[0]);
        }
        // 중복 있을시 true, 없을시 false
        public bool IDCheck(string ID)
        {
            ArrData = null;
            DataSend((int)OpCode.ID_CHECK, ID);
            string line;
            while (getString(out line) == false) ;
            Spliter Spliter = new Spliter(line);
            ArrData = Spliter.getSplit();
            IsValidOp(11);

            return GetBool(ArrData[0]);
        }
        // 중복 있을시 true, 없을시 false
        public bool NickCheck(string Nick)
        {
            ArrData = null;
            DataSend((int)OpCode.NICK_CHECK, Nick);

            IsValidOp(12);

            return GetBool(ArrData[0]);
        }
        // 찾을시 true, 못찾을시 false
        public bool EMailCheck(string Email)
        {
            ArrData = null;
            DataSend((int)OpCode.EMAIL_CHECK, Email);

            IsValidOp(13);

            return GetBool(ArrData[0]);
        }

        // 찾은 ID 반환, 못찾을 시 NULL
        public string FindID(string Email)
        {
            ArrData = null;
            DataSend((int)OpCode.FIND_ID, Email);

            IsValidOp(15);

            return ArrData[0];
        }

        // 찾으면 true, 못찾을시 false
        public bool FindPW(string ID, string Email)
        {
            ArrData = null;
            DataSend((int)OpCode.FIND_PW, ID + delimiter + Email);

            IsValidOp(16);

            return GetBool(ArrData[0]);
        }

        /// <summary>
        /// string으로 받은 값이 1, 0인지 파악후 0이면 false 그외는 true
        /// </summary>
        /// <param name="ArrData_0"></param>
        /// <returns></returns>
        private bool GetBool(string ArrData_0)
        {
            int result = 1;
            try
            {
                result = Convert.ToInt32(ArrData[0]);
                if (result != 0 && result != 1)
                    throw new Exception("boolean 값이 아닙니다.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return true;
            }

            if (result == 0)
                return false;
            return true;
        }
    }
}
