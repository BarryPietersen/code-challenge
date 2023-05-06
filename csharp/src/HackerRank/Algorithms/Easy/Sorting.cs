using System;
using System.Collections.Generic;

namespace HackerRank.Algorithms.Easy
{
    public static class Sorting
    {
        //https://www.hackerrank.com/challenges/closest-numbers/problem
        public static int[] closestNumbers(int[] arr)
        {
            Array.Sort(arr);
            var len = arr.Length - 1;
            var smallest = int.MaxValue;
            var pairs = new List<int>();

            for (var i = 0; i < len; i++)
            {
                if (arr[i + 1] - arr[i] < smallest)
                    smallest = arr[i + 1] - arr[i];
            }

            for (var i = 0; i < len; i++)
            {
                if (arr[i + 1] - arr[i] == smallest)
                {
                    pairs.Add(arr[i]);
                    pairs.Add(arr[i + 1]);
                }
            }

            return pairs.ToArray();
        }
    }
}

