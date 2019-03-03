using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.OtherChallenges
{
    public static class GeeksForGeeks
    {
        /*
            public static int[][] intervals = new int[6][]
            {
                new int[2] { 2, 5 },
                new int[2] { 4, 6 },
                new int[2] { 1, 4 },
                new int[2] { 7, 19 },
                new int[2] { 6, 13 },
                new int[2] { 19, 22 }
            };
        */
        // https://www.geeksforgeeks.org/merging-intervals/
        //================================================================================
        public static int[][] MergeTimeIntervals(int[][] intervals)
        {
            Stack<int[]> mergedIntevals = new Stack<int[]>();

            IOrderedEnumerable<int[]> sortedIntervals = intervals.OrderBy(i => i[0]);

            mergedIntevals.Push(sortedIntervals.First());

            foreach (int[] interval in sortedIntervals)
            {
                if ((mergedIntevals.Peek()[1] >= interval[0] &&
                     mergedIntevals.Peek()[1] <= interval[1]))
                {
                    mergedIntevals.Peek()[1] = interval[1];
                }
                else
                {
                    mergedIntevals.Push(interval);
                }
            }

            return mergedIntevals.ToArray();
        }

        /*
             n = num food buy each day = 16
             s = num days to survive   = 10
             m = num food required day = 2

              Input : n = 16 s = 10 m = 2
             Output : Yes 2
        */
        // https://www.geeksforgeeks.org/survival/
        public static bool IslandSurvive(int n, int s, int m)
        {
            //we cant survive the amount of days.
            if ((7 * m > 6 * n && s > 6) || m > n) { return false; }

            // else
            // we can survive and need to identify the
            // minimum amount of days to buy food on

            // if we can survive then we can
            // buy ceil(a/n) times where a is
            // total units of food required.
            int days = (m * s) / n;

            if (((m * s) % n) != 0) days++;

            Console.WriteLine("Yes " + days);
            return true;
        }

        // https://practice.geeksforgeeks.org/problems/equilibrium-point/0
        public static int EquilibriumPoint(int[] arr)
        {
            int l = 0;
            int r = arr.Sum() - arr[0];

            for (int i = 1; i < arr.Length - 1; i++)
            {
                l += arr[i - 1];
                r -= arr[i];

                if (l == r) return i;
            }
            return -1;
        }

        //int[][] arr = new int[][]
        //{
        //    new int[]{ 0, 3, 0, 0, 0 },
        //    new int[]{ 0, 1, 0, 0, 0 },
        //    new int[]{ 1, 1, 1, 0, 0 },
        //    new int[]{ 0, 0, 2, 4, 4 },
        //    new int[]{ 0, 0, 0, 2, 4 }
        //};
        // https://www.geeksforgeeks.org/maximum-sum-hour-glass-matrix/
        public static int MaxHourGlass(int[][] arr)
        {
            int temp;
            int max = int.MinValue;
            int len = arr.Length - 2;

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    temp =
                    arr[i][j] + arr[i][j + 1] +
                    arr[i][j + 2] + arr[i + 1][j + 1] +
                    arr[i + 2][j] + arr[i + 2][j + 1] +
                    arr[i + 2][j + 2];

                    if (temp > max) max = temp;
                }
            }
            return max;
        }

        //{1, 3, 4, 7, 9, 9, 12, 56} m = 3, mindiff = 2
        //{3, 4, 1, 9, 56, 7, 9, 12} m = 5, mindiff = 6
        //{12, 4, 7, 9, 2, 23, 25, 41, 30, 40, 28, 42, 30, 44, 48, 43, 50} m = 7, mindiff = 10
        //{2, 4, 7, 9, 12, 23, 25, 28, 30, 30, 40, 41, 42, 43, 44, 48, 50}
        // ^                    ^
        // https://www.geeksforgeeks.org/chocolate-distribution-problem/
        public static int ChocDistribution(int[] chocs, int m)
        {
            Array.Sort(chocs);

            int i = 0;
            int j = m - 1;
            int min = chocs[j] - chocs[i];

            while (j < chocs.Length)
            {
                if (chocs[j] - chocs[i] < min)
                {
                    min = chocs[j] - chocs[i];
                }

                i++; j++;
            }
            return min;
        }

        // https://www.geeksforgeeks.org/maximum-sum-bitonic-subarray/
        // the problem statement does not specify or provide exampe cases of how to handle adjacent numbers of equal value
        // this solution assumes the input array contains only distinct values, as observed in the problem statement
        // space o(1) time o(n)
        private static int MaxBitonicSubArray(int[] array)
        {
            int tempmax;
            int max = -1;
            bool isbitonic;
            int len = array.Length - 1;

            for (int i = 0; i < len; i++)
            {
                if (array[i] < array[i + 1])
                {
                    isbitonic = false;
                    tempmax = array[i];

                    while (i < len && array[i] < array[i + 1])
                    {
                        tempmax += array[++i];
                    }

                    if (i < len) isbitonic = true;

                    while (i < len && array[i] > array[i + 1])
                    {
                        tempmax += array[++i];
                    }

                    if (isbitonic && tempmax > max) max = tempmax;

                    i--;
                }
            }

            return max;
        }
    }
}
