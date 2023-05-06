using System.Collections.Generic;

namespace HackerRank.DataStructures.Easy
{
    public static class Arrays
    {
        // https://www.hackerrank.com/challenges/arrays-ds/problem
        public static int[] reverseArray(int[] a)
        {
            int temp;
            int len = a.Length / 2;

            for (int i = 0, j = a.Length - 1; i < len; i++, j--)
            {
                temp = a[i];
                a[i] = a[j];
                a[j] = temp;
            }

            return a;
        }

        // https://www.hackerrank.com/challenges/2d-array/problem
        public static int hourglassSum(int[][] arr)
        {
            int hourGlass;
            int max = int.MinValue;

            for (var i = 0; i < arr.Length - 2; i++)
            {
                for (var j = 0; j < arr[i].Length - 2; j++)
                {
                    hourGlass =
                    arr[i][j] + arr[i][j + 1] + arr[i][j + 2] +
                    arr[i + 1][j + 1] + arr[i + 2][j] + arr[i +
                    2][j + 1] + arr[i + 2][j + 2];

                    if (hourGlass > max) { max = hourGlass; }
                }
            }

            return max;
        }

        // https://www.hackerrank.com/challenges/array-left-rotation/problem
        public static int[] LeftRotation(int[] a, int d, int n)
        {
            //copy i value to new index 
            //use i + shift % n 
            var shift = a.Length - d;
            var res = new int[n];

            for (var i = 0; i < n; i++) res[(i + shift) % n] = a[i];

            return res;
        }

        // https://www.hackerrank.com/challenges/sparse-arrays/problem
        public static int[] matchingStrings(string[] strings, string[] queries)
        {
            var res = new int[queries.Length];
            var queryOccurances = new Dictionary<string, int>(queries.Length);

            foreach (var qry in queries)
            {
                if (!queryOccurances.ContainsKey(qry))
                    queryOccurances.Add(qry, 0);
            }

            foreach (var str in strings)
            {
                if (queryOccurances.ContainsKey(str))
                    queryOccurances[str]++;
            }

            for (var i = 0; i < queries.Length; i++)
                res[i] = queryOccurances[queries[i]];

            return res;
        }
    }
}
