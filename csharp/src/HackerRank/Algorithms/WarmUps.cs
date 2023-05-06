using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerRank.Algorithms
{
    public static class WarmUps
    {
        public static int solveMeFirst(int a, int b)
        {
            return a + b;
        }

        public static int simpleArraySum(int n, int[] ar)
        {
            int sum = 0;
            foreach (int num in ar) sum += num;
            return sum;
        }

        // https://www.hackerrank.com/challenges/a-very-big-sum/problem
        public static long aVeryBigSum(int n, long[] ar)
        {
            long sum = 0;
            foreach (long num in ar) sum += num;
            return sum;
        }

        // https://www.hackerrank.com/challenges/compare-the-triplets/problem
        public static List<int> compareTriplets(List<int> a, List<int> b)
        {
            int bob = 0;
            int alice = 0;

            for (int i = 0; i < 3; i++)
            {
                if (a.ElementAt(i) > b.ElementAt(i)) alice++;
                else if (a.ElementAt(i) < b.ElementAt(i)) bob++;
            }

            return new List<int>(2) { alice, bob };
        }

        // https://www.hackerrank.com/challenges/diagonal-difference/problem
        public static int diagonalDifference(int[][] arr)
        {
            int d1 = 0;
            int d2 = 0;
            int j = arr.Length - 1;

            for (int i = 0; i < arr.Length; i++)
            {
                d1 += arr[i][i];
                d2 += arr[i][j--];
            }

            return Math.Abs(d1 - d2);
        }

        // https://www.hackerrank.com/challenges/plus-minus/problem
        public static void plusMinus(int[] arr)
        {
            int n = arr.Length;
            decimal pos = 0;
            decimal neg = 0;
            decimal zero = 0;

            for (int i = 0; i < n; i++)
            {
                if (arr[i] > 0) pos++;
                else if (arr[i] < 0) neg++;
                else zero++;
            }

            pos /= n;
            neg /= n;
            zero /= n;

            Console.WriteLine($@"{Math.Round(pos, 6)}\r\n
                            {Math.Round(neg, 6)}\r\n
                            {Math.Round(zero, 6)}");
        }

        // https://www.hackerrank.com/challenges/staircase/problem
        public static void staircase(int n)
        {
            /*
                     #
                    ##
                   ###
                  ####
                 #####
                ######
            */
            string[] stairs = new string[n];

            for (int i = 1; i <= n; i++)
            {
                stairs[i - 1] = new string(' ', n - i) + new string('#', i);
            }

            foreach (string item in stairs)
            {
                Console.WriteLine(item);
            }
        }

        // https://www.hackerrank.com/challenges/mini-max-sum/problem
        public static void miniMaxMin(int[] arr)
        {
            long sum = 0;
            long max = 0;
            long min = arr[0];
            foreach (long num in arr)
            {
                sum += num;
                if (num > max) max = num;
                else if (num < min) min = num;
            }

            Console.Write($"{sum - max} {sum - min}");
        }

        // https://www.hackerrank.com/challenges/birthday-cake-candles/problem
        public static int birthdayCakeCandles(int[] ar)
        {
            int greatest = 0;
            int count = 0;
            foreach (int candle in ar)
            {
                if (candle > greatest)
                {
                    count = 1;
                    greatest = candle;
                }
                else if (candle == greatest) count++;
            }

            return count;
        }

        // https://www.hackerrank.com/challenges/time-conversion/problem
        public static string timeConversion(string s)
        {
            // Complete this function

            if (s[8] == 'A')
            {
                s = s.Remove(8);
                string hr = s[0].ToString() + s[1].ToString();
                return hr == "12" ? "00" + s.Remove(0, 2) : s;
            }
            else
            {
                s = s.Remove(8);
                int hr = Convert.ToInt32($"{s[0].ToString() + s[1].ToString()}");
                hr += 12;
                s = s.Remove(0, 2);
                return hr == 24 ? "12" + s : hr.ToString() + s;
            }
        }
    }
}
