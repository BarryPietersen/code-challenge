using System.Collections.Generic;

namespace HackerRank.Mathematics
{
    public static class Combinatorics
    {
        // https://www.hackerrank.com/challenges/sherlock-and-pairs/problem
        static long SherlockPairs(int[] a)
        {
            var result = 0L;
            var groups = new Dictionary<long, long>();

            foreach (var i in a)
            {
                if (!groups.ContainsKey(i))
                {
                    groups.Add(i, 1);
                }
                else
                {
                    groups[i]++;
                }
            }

            foreach (var kvp in groups)
            {
                result += kvp.Value * (kvp.Value - 1);
            }

            return result;
        }
    }
}
