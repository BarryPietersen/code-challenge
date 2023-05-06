using System.Collections.Generic;

namespace HackerRank.Algorithms.Medium
{
    public static class Constructive
    {
        // https://www.hackerrank.com/challenges/an-interesting-game-1/problem
        public static string gamingArray(List<int> arr)
        {
            var n = arr.Count;
            var turns = 0;
            var max = 0;

            for (var i = 0; i < n; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                    turns++;
                }
            }

            return turns % 2 == 1 ? "BOB" : "ANDY";
        }
    }
}

