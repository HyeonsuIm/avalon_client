using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalron
{
    class Command
    {
        public enum Option { All, Wisper, Err,  NULL};

        public Option Splite(string line)
        {
            if (0 == line.Length)
                return Option.NULL;
            if (line[0] != '/')
                return Option.All;

            switch (GetFirst(line))
            {
                case "/w":
                case "/W":
                case "/ㅈ":
                    if (GetCnt(line) < 2)
                        return Option.Err;
                    return Option.Wisper;
                    
                default:
                    return Option.Err;
            }
        }

        private int GetCnt(string line)
        {
            int cnt = 0;
            foreach(char c in line)
            {
                if (c == ' ')
                    cnt++;
            }
            return cnt;
        }

        private string GetFirst(string line)
        {
            int idx = line.IndexOf(" ");

            return line.Substring(0, idx);
        }
        
        private string GetRest(string line)
        {
            int idx = line.IndexOf(" ");

            return line.Substring(idx + 1, line.Length - idx);
        }
        
        public string GetNick(string line)
        {
            string[] arr = line.Split(' ');

            return arr[1];
        }

        public string GetText(string line)
        {
            int idx = 0;
            for(int i =0; i < 2; i ++)
            {
                idx = line.IndexOf(" ");
                line = line.Substring(idx + 1, line.Length - (idx + 1));
            }

            return line;
        }
    }
}
