using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron
{
    class Spriter
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

        public Spriter(string line)
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

        // split의 배열 개수 - 1 입니다.
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