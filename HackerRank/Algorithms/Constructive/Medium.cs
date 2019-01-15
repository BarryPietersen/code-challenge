using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Algorithms.Constructive
{
    public static class Medium
    {
        // https://www.hackerrank.com/challenges/an-interesting-game-1/problem
        public static string gamingArray(List<int> arr)
        {
            int n = arr.Count;
            int turns = 0;
            int max = 0;

            for (int i = 0; i < n; i++)
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
