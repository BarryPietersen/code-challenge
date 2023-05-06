using System;
using System.Linq;

namespace HackerRank.Algorithms.Easy
{
    public static class Greedy
    {
        // https://www.hackerrank.com/challenges/marcs-cakewalk/problem
        public static long marcsCakewalk(int[] calorie)
        {
            var miles = 0L;

            calorie = calorie
                .OrderByDescending(c => c)
                .ToArray();

            for (var i = 0; i < calorie.Length; i++)
                miles += (long)Math.Pow(2, i) * calorie[i];

            return miles;
        }

        // https://www.hackerrank.com/challenges/luck-balance/problem
        public static int luckBalance(int k, int[][] contests)
        {
            var totalLuck = 0;
            var lostLuck = 0;

            var imp = contests
                .Where(c => c[1] == 1)
                .Select(c => c[0])
                .ToArray();

            Array.Sort(imp);

            for (var i = 0; i < contests.Length; i++) totalLuck += contests[i][0];

            for (var i = 0; i < imp.Length - k; i++) lostLuck += imp[i];

            return totalLuck - (lostLuck * 2);
        }

        // https://www.hackerrank.com/challenges/mark-and-toys/problem
        public static int maximumToys(int[] prices, int k)
        {
            var max = 0;
            Array.Sort(prices);

            for (var i = 0; i < prices.Length; i++)
            {
                k -= prices[i];
                if (k > 0) max++;
                else break;
            }
            return max;
        }

        // https://www.hackerrank.com/challenges/jim-and-the-orders/problem
        public static int[] jimOrders(int[][] orders)
        {
            var a = new int[orders.Length][];
            for (var i = 0; i < orders.Length; i++)
                a[i] = new int[] { i + 1, orders[i][0], orders[i][0] + orders[i][1] };

            return a.OrderBy(o => o[2]).Select(on => on[0]).ToArray();
        }

        // https://www.hackerrank.com/challenges/two-arrays/problem
        public static string twoArrays(int k, int[] A, int[] B)
        {
            var l = 0;
            var r = B.Length - 1;

            Array.Sort(A);
            Array.Sort(B);

            while (l < r &&
                   A[l] + B[r] >= k &&
                   A[l] + B[r] >= k)
            { 
                l++;
                r--;
            }

            return l >= r ? "YES" : "NO";
        }

        // https://www.hackerrank.com/challenges/grid-challenge/problem
        public static string gridChallenge(string[] grid)
        {
            if (grid == null) throw new ArgumentNullException(nameof(grid));
            if (grid.Length < 2) return "YES";

            // sort the first row
            grid[0] = string.Join("", grid[0].OrderBy(ch => ch));

            // c  - column idx
            // c1 - column idx + 1
            // r  - row idx
            for (int c = 0, c1 = 1; c1 < grid.Length; c++, c1++)
            {
                // sort the next row and then compare column values
                grid[c1] = string.Join("", grid[c1].OrderBy(ch => ch));

                for (int r = 0; r < grid[c].Length; r++)
                {
                    if (grid[c][r] > grid[c1][r])
                    {
                        return "NO";
                    }
                }
            }

            return "YES";
        }

        // https://www.hackerrank.com/challenges/priyanka-and-toys/problem
        public static int toys(int[] w)
        {
            Array.Sort(w);
            var count = 1;
            var min = w[0];

            for (var i = 1; i < w.Length; i++)
            {
                if (w[i] - min > 4)
                {
                    count++;
                    min = w[i];
                }
            }

            return count;
        }

        // https://www.hackerrank.com/challenges/minimum-absolute-difference-in-an-array/problem
        public static int minimumAbsoluteDifference(int[] arr)
        {
            Array.Sort(arr);
            var min = int.MaxValue;

            for (var i = 1; i < arr.Length; i++)
            {
                if (arr[i] - arr[i - 1] < min)
                {
                    min = arr[i] - arr[i - 1];
                }
            }

            return min;
        }
    }
}
