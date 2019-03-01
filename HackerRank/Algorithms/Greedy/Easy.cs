using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Algorithms.Greedy
{
    public static class Easy
    {
        // https://www.hackerrank.com/challenges/marcs-cakewalk/problem
        static long marcsCakewalk(int[] calorie)
        {
            long miles = 0;

            calorie = calorie.OrderByDescending(c => c).ToArray();

            for (int i = 0; i < calorie.Length; i++)
                miles += (long)Math.Pow(2, i) * calorie[i];

            return miles;
        }

        // https://www.hackerrank.com/challenges/luck-balance/problem
        static int luckBalance(int k, int[][] contests)
        {
            int totalLuck = 0;
            int lostLuck = 0;

            int[] imp = contests.Where(c => c[1] == 1).Select(c => c[0]).ToArray();
            Array.Sort(imp);

            for (int i = 0; i < contests.Length; i++) totalLuck += contests[i][0];

            for (int i = 0; i < imp.Length - k; i++) lostLuck += imp[i];

            return totalLuck - (lostLuck * 2);
        }

        // https://www.hackerrank.com/challenges/mark-and-toys/problem
        static int maximumToys(int[] prices, int k)
        {
            int max = 0;
            Array.Sort(prices);

            for (int i = 0; i < prices.Length; i++)
            {
                k -= prices[i];
                if (k > 0) max++;
                else break;
            }
            return max;
        }

        // https://www.hackerrank.com/challenges/jim-and-the-orders/problem
        static int[] jimOrders(int[][] orders)
        {
            int[][] a = new int[orders.Length][];
            for (int i = 0; i < orders.Length; i++)
                a[i] = new int[] { i + 1, orders[i][0], orders[i][0] + orders[i][1] };

            return a.OrderBy(o => o[2]).Select(on => on[0]).ToArray();
        }

        // https://www.hackerrank.com/challenges/two-arrays/problem
        static string twoArrays(int k, int[] A, int[] B)
        {
            int l = 0;
            int r = B.Length - 1;

            Array.Sort(A);
            Array.Sort(B);

            while (l < r && A[l] + B[r] >= k && A[l] + B[r] >= k) { l++; r--; }

            return l >= r ? "YES" : "NO";
        }

        // https://www.hackerrank.com/challenges/grid-challenge/problem
        static string gridChallenge(string[] grid)
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
			int count = 1;
			int min = w[0];

			for(int i = 1; i < w.Length; i++)
			{
				if(w[i] - min > 4)
				{
					count++;
					min = w[i];
				}
			}

			return count;
		}
    }
}
