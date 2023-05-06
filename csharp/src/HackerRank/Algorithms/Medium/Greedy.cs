using System;

namespace HackerRank.Algorithms.Medium
{
    public static class Greedy
    {
        // https://www.hackerrank.com/challenges/angry-children/problem
        public static int maxMin(int k, int[] arr)
        {
            Array.Sort(arr);
            var l = 0;
            var r = k - 1;
            var min = arr[r++] - arr[l++];
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
