using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron
{
    /// <summary>
    /// 1. 매개변수 갯수 셀래
    /// 2. op코드 반환
    /// 3. 매개변수를 배열로 반환
    /// </summary>
    class Spliter 
    {
        public string FullOpCode
        {
            get; set;
        }
        public string[] split
        {
            get;
        }
        const char delimiter = '\u0001';
        int cnt = 0;

        public Spliter(string line)
        {
            if (line.Length < 5)
            {
                throw new Exception("받은 데이터가 너무 적습니다.");
            }
            FullOpCode = line.Substring(0, 5);
            line = line.Remove(0, 5);
            //line.Insert(4, delimiter.ToString());
            //split[0] = line.Substring(5);
            split = line.Split(delimiter);
            cnt = split.Length;
        }

        public bool IsValid()
        {
            string temp = split[0][3] + "" + split[0][4];
            if (cnt == (Convert.ToInt32(temp) + 1))
                return true;
            else
                return false;
        }

        public int getForm()
        {
            return split[0][0];
        }

        public int getCnt()
        {
            return cnt - 1;
        }

        public int getJustOpCode()
        {
            string temp = FullOpCode.Substring(0, 3); 
            return Convert.ToInt32(temp);
        }

        public string[] getSplit()
        {
            return split;
        }
    }
}