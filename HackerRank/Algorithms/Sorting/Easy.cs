using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Algorithms.Sorting
{
    public static class Easy
    {
        //https://www.hackerrank.com/challenges/closest-numbers/problem
        static int[] closestNumbers(int[] arr)
        {
            Array.Sort(arr);
            int len = arr.Length - 1;
            int smallest = int.MaxValue;
            List<int> pairs = new List<int>();

            for (int i = 0; i < len; i++)
            {
                if (arr[i + 1] - arr[i] < smallest)
                    smallest = arr[i + 1] - arr[i];
            }

            for (int i = 0; i < len; i++)
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
