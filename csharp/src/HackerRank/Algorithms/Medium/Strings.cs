using System.Collections.Generic;
using System.Linq;

namespace HackerRank.Algorithms.Medium
{
    public static class Strings
    {
        /*
            in this challenge we are tasked with determining if:

            the input string contains an equal number of occurring characters

            OR
            
            the input string contains an equal number of occurring characters,
            except for one character, whos occurance is either one greater than the rest
                                                        or it occurs only once.
            
            returning "YES" if either conditions are met, otherwise "NO"

            complexities:
                         runtime = o(n)
                           space = o(n)
        */
        // https://www.hackerrank.com/challenges/sherlock-and-valid-string/problem
        public static string isValid(string s)
        {
            if (s.Length < 4) return "YES";

            int num;
            var removed = false;
            var chars = new Dictionary<char, int>();

            foreach (var ch in s)
            {
                if (chars.ContainsKey(ch)) chars[ch]++;
                else chars.Add(ch, 1);
            }

            if (chars.ElementAt(0).Value == chars.ElementAt(1).Value) num = chars.ElementAt(0).Value;
            else if (chars.ElementAt(1).Value == chars.ElementAt(2).Value) num = chars.ElementAt(1).Value;
            else if (chars.ElementAt(0).Value == chars.ElementAt(2).Value) num = chars.ElementAt(0).Value;
            else return "NO";

            foreach (var val in chars.Values)
            {
                if (val != num)
                {
                    if ((!removed) && (val == num + 1 || val == 1))
                    {
                        removed = true;
                    }
                    else return "NO";
                }
            }

            return "YES";
        }
    }
}
