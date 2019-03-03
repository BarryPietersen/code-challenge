using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace HackerRank.OtherChallenges
{
    public static class CodeWars
    {
        // codewars https://www.codewars.com/kata/dubstep/train/csharp
        public static string SongDecoder(string input)
        {
            return string.Join(" ", new Regex("WUB").Replace(input, " ").Split(' ').Where(s => !string.IsNullOrEmpty(s)));
        }

        // codewars https://www.codewars.com/kata/54e6533c92449cc251001667/train/csharp
        public static IEnumerable<T> UniqueInOrder<T>(IEnumerable<T> iterable)
        {
            List<T> result = new List<T>();

            if (iterable.Count() == 0) return result;

            result.Add(iterable.First());

            foreach (var item in iterable)
                if (!result.Last().Equals(item))
                    result.Add(item);

            return result;
        }
    }
}
