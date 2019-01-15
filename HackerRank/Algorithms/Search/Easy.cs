using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Algorithms.Search
{
    public static class Easy
    {
        // https://www.hackerrank.com/challenges/missing-numbers/problem
        static int[] missingNumbers(int[] arr, int[] brr)
        {
            Dictionary<int, int> b = new Dictionary<int, int>();

            List<int> missing = new List<int>();

            foreach (int num in brr)
            {
                if (b.ContainsKey(num)) b[num]++;
                else b.Add(num, 1);
            }

            foreach (int num in arr) b[num]--;

            return b.Where(kvp => kvp.Value > 0)
                    .Select(kvp => kvp.Key)
                    .OrderBy(k => k)
                    .ToArray();
        }

        // https://www.hackerrank.com/challenges/icecream-parlor/problem
        static int[] icecreamParlor(int m, int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
                for (int j = i + 1; j < arr.Length; j++)
                    if (arr[i] + arr[j] == m) return new int[] { i + 1, j + 1 };

            return new int[] { -1, -1 };
        }

        // https://www.hackerrank.com/challenges/sherlock-and-array/problem
        static string balancedSums(List<int> arr)
        {
            int left = 0;
            int right = arr.Sum();
            int len = arr.Count;

            for (int i = 0; i < len; i++)
            {
                right -= arr[i];

                if (i != 0) { left += arr[i - 1]; }

                if (left == right) { return "YES"; }
            }

            return "NO";
        }
    }
}
