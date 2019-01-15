using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Algorithms.Greedy
{
    public static class Medium
    {
        // https://www.hackerrank.com/challenges/angry-children/problem
        static int maxMin(int k, int[] arr)
        {
            Array.Sort(arr);
            int l = 0;
            int r = k - 1;
            int min = arr[r++] - arr[l++];
            int unfair;

            while (r < arr.Length)
            {
                unfair = arr[r++] - arr[l++];
                if (unfair < min) min = unfair;
            }

            return min;
        }
    }
}
