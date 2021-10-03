using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    abstract class Parser
    {
        public char all { get; set; }

        protected unsafe string Parse(string str, string Rules)
        {
            var ArrayOfRules = Rules.Split(',');
            var str0 = "";
            for (int m = 0; m < ArrayOfRules.Length; m++)
            {
                str0 = "";
                var Rule = ArrayOfRules[m];
                fixed (char* fr = Rule)
                {
                    var n = -1;    
                    for (int i = 0; i < str.Length; i++) 
                    {
                        if (Rule.Length - 1 == n) n = -1;
                        if (*fr == str[i]) n++;
                        if (n != -1)
                        {
                            if (*(fr + n) != all) n++;
                            else if (*(fr + n + 1) == str[i]) n++;
                        }
                        else str0 += str[i];
                    }
                    str = str0;
                }
            }
            return str0;
        }

        protected unsafe string Parse(string str, string Rules, string zamena)
        {
            var ArrayOfRules = Rules.Split(',');
            var str0 = "";
            for (int m = 0; m < ArrayOfRules.Length; m++)
            {
                str0 = "";
                var Rule = ArrayOfRules[m];
                fixed (char* fr = Rule)
                {
                    var n = -1;
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (Rule.Length - 1 == n) { n = -1; str0 += zamena; }
                        if (*fr == str[i]) n++;
                        if (n != -1)
                        {
                            if (*(fr + n) != all) n++;
                            else if (*(fr + n + 1) == str[i]) n++;
                        }
                        else str0 += str[i];
                    }
                    str = str0;
                }
            }
            return str0;
        }
    }
}