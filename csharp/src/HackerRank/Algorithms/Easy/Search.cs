using System.Collections.Generic;
using System.Linq;

namespace HackerRank.Algorithms.Easy
{
    public static class Search
    {
        // https://www.hackerrank.com/challenges/missing-numbers/problem
        public static int[] missingNumbers(int[] arr, int[] brr)
        {
            var b = new Dictionary<int, int>();
            var missing = new List<int>();

            foreach (var num in brr)
            {
                if (b.ContainsKey(num)) b[num]++;
                else b.Add(num, 1);
            }

            foreach (var num in arr) b[num]--;

            return b.Where(kvp => kvp.Value > 0)
                    .Select(kvp => kvp.Key)
                    .OrderBy(k => k)
                    .ToArray();
        }

        // https://www.hackerrank.com/challenges/icecream-parlor/problem
        public static int[] icecreamParlor(int m, int[] arr)
        {
            for (var i = 0; i < arr.Length - 1; i++)
                for (var j = i + 1; j < arr.Length; j++)
                    if (arr[i] + arr[j] == m) return new int[] { i + 1, j + 1 };

            return new int[] { -1, -1 };
        }

        // https://www.hackerrank.com/challenges/sherlock-and-array/problem
        public static string balancedSums(List<int> arr)
        {
            var left = 0;
            var right = arr.Sum();
            var len = arr.Count;

            for (var i = 0; i < len; i++)
            {
                right -= arr[i];

                if (i != 0) { left += arr[i - 1]; }

                if (left == right) { return "YES"; }
            }

            return "NO";
        }
    }
}
