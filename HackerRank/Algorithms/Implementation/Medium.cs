using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace HackerRank.Algorithms.Implementation
{
    // method signatures match hackerranks.
    // all solutions pass 100% test cases

    public static class Medium
    {
        // https://www.hackerrank.com/challenges/climbing-the-leaderboard/problem
        //=================================================================================
        private static int[] getRanks(int[] scores, int[] alice)
        {
            scores = scores.Distinct().ToArray();

            for (int i = 0; i < alice.Length; i++)
                alice[i] = binarySearch(scores, alice[i], 0, scores.Length - 1);

            return alice;
        }

        static int binarySearch(int[] scores, int score, int start, int end)
        {
            int mid = (end - start) / 2;

            if (mid == 0)
            {
                if (score >= scores[start]) return ++start;
                if (score >= scores[end]) return ++end;
                return end + 2;
            }

            mid += start;

            if (score == scores[mid]) return ++mid;

            return score > scores[mid] ?
                binarySearch(scores, score, start, --mid) :
                binarySearch(scores, score, ++mid, end);
        }
        //=================================================================================

        // https://www.hackerrank.com/challenges/extra-long-factorials/problem
        static BigInteger extraLongFactorials(int n)
        {
            int len = n - 1;
            BigInteger fac = new BigInteger(n);

            for (int i = 1; i < len; i++)
            {
                fac *= n - i;
            }
            return fac;
        }

        // https://www.hackerrank.com/challenges/queens-attack-2/problem
        //=================================================================================
        static int queensAttack(int n, int k, int r_q, int c_q, int[][] obstacles)
        {
            int count = 0;
            HashSet<Tuple<int, int>> obst = new HashSet<Tuple<int, int>>();

            foreach (var arr in obstacles)
                obst.Add(Tuple.Create(arr[0], arr[1]));

            count += ValidateQueen(n, r_q, c_q, obst, -1, 0);
            count += ValidateQueen(n, r_q, c_q, obst, -1, 1);
            count += ValidateQueen(n, r_q, c_q, obst, 0, 1);
            count += ValidateQueen(n, r_q, c_q, obst, 1, 1);
            count += ValidateQueen(n, r_q, c_q, obst, 1, 0);
            count += ValidateQueen(n, r_q, c_q, obst, 1, -1);
            count += ValidateQueen(n, r_q, c_q, obst, 0, -1);
            count += ValidateQueen(n, r_q, c_q, obst, -1, -1);

            return count;
        }

        static int ValidateQueen(int n, int r_q, int c_q, HashSet<Tuple<int, int>> obst, int row, int col)
        {
            int count = 0;
            r_q += row;
            c_q += col;

            while ((r_q <= n && r_q >= 1) && (c_q <= n && c_q >= 1))
            {
                if (!obst.Contains(Tuple.Create(r_q, c_q))) count++;
                else break;
                r_q += row;
                c_q += col;
            }
            return count;
        }
        //=================================================================================

        // https://www.hackerrank.com/challenges/the-time-in-words/problem
        static string timeInWords(int h, int m)
        {  
            Dictionary<int, string> words = new Dictionary<int, string>();
            words.Add(0, "o' clock");
            words.Add(1, "one");
            words.Add(2, "two");
            words.Add(3, "three");
            words.Add(4, "four");
            words.Add(5, "five");
            words.Add(6, "six");
            words.Add(7, "seven");
            words.Add(8, "eight");
            words.Add(9, "nine");
            words.Add(10, "ten");
            words.Add(11, "eleven");
            words.Add(12, "twelve");
            words.Add(13, "thirteen");
            words.Add(14, "fourteen");
            words.Add(15, "fifteen");
            words.Add(16, "sixteen");
            words.Add(17, "seventeen");
            words.Add(18, "eighteen");
            words.Add(19, "nineteen");
            words.Add(20, "twenty");
            words.Add(21, "twenty one");
            words.Add(22, "twenty two");
            words.Add(23, "twenty three");
            words.Add(24, "twenty four");
            words.Add(25, "twenty five");
            words.Add(26, "twenty six");
            words.Add(27, "twenty seven");
            words.Add(28, "twenty eight");
            words.Add(29, "twenty nine");
            words.Add(30, "half past");
            words.Add(45, "quarter to");

            if (m == 0)
            {
                return $"{words[h]} {words[m]}";
            }
            else if (m == 15)
            {
                return $"quarter past {words[h]}";
            }
            else if (m < 30)
            {
                string w = m == 1 ? "minute" : "minutes";
                return $"{words[m]} {w} past {words[h]}";
            }
            else if (m == 30)
            {
                return $"{words[m]} {words[h]}";
            }
            else if (m == 45)
            {
                return $"{words[m]} {words[h % 12 + 1]}";
            }
            else
            {
                return $"{words[60 - m]} minutes to {words[h % 12 + 1]}";
            }
        }

        // surface area of 1 cube = 6
        // surface area of 1 column = (h * 6) - ((h - 1) * 2)
        // hidden surface area (two cols meet) = (h of smaller) * 2
        // https://www.hackerrank.com/challenges/3d-surface-area/problem
        static int surfaceArea(int[][] A)
        {
            int totalSurface = 0;
            int x = A[0].Length - 1;
            int y = A.Length - 1;

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    totalSurface += (A[i][j] * 6) - ((A[i][j] - 1) * 2) -
                    (A[i][j] > A[i][j + 1] ? A[i][j + 1] * 2 : A[i][j] * 2) -
                    (A[i][j] > A[i + 1][j] ? A[i + 1][j] * 2 : A[i][j] * 2);

                    if (i == 0)
                    {
                        totalSurface += (A[y][j] * 6) - ((A[y][j] - 1) * 2) -
                        (A[y][j] > A[y][j + 1] ? A[y][j + 1] * 2 : A[y][j] * 2);
                    }
                }

                totalSurface += (A[i][x] * 6) - ((A[i][x] - 1) * 2) -
                (A[i][x] > A[i + 1][x] ? A[i + 1][x] * 2 : A[i][x] * 2);
            }

            if (y < 1)
            {
                for (int i = 0; i < x; i++)
                {
                    totalSurface += (A[y][i] * 6) - ((A[y][i] - 1) * 2) -
                    (A[y][i] > A[y][i + 1] ? A[y][i + 1] * 2 : A[y][i] * 2);
                }
            }

            return totalSurface += (A[y][x] * 6) - ((A[y][x] - 1) * 2);
        }

        /*
        ==========================================================================================================
            a  b  c  d   e   f   g   h   i   j   k   l   m   n   o   p   q   r   s   t   u   v   w   x   y   z
            97 98 99 100 101 102 103 104 105 106 107 108 109 110 111 112 113 114 115 116 117 118 119 120 121 122

            Sample Input

            ab      = 97 98
            ba      = 98 97

            bb      = 98 98
                    no answer

            hefg    = 104 101 102 103
            hegf    = 104 101 103 102

            dhck    = 100 104 99  107
            dhkc    = 100 104 107 99

            dkhc    = 100 107 104 99
            hcdk    = 104 99  100 107
        */
        // https://www.hackerrank.com/challenges/bigger-is-greater/problem
        // search from right to left for the first occurance of the condition a[i] > a[i-1]
        // search the entire sub array starting at index i for 'j' the smallest greater value than a[i-1]
        // swap the two values a[i-1] and a[j]
        // sort the sub array starting from index i

        // **if the outer loop condition fails it means we have iterated the entire string and that the
        //   string is already the largest lexicographical permutation it can possibly be - 'no answer'
        static string biggerIsGreater(string w)
        {
            char[] arr = w.ToCharArray();

            for (int i = arr.Length - 1; i > 0; i--)
            {
                // is possible
                if (arr[i] > arr[i - 1])
                {
                    int smallestgreateridx = i;

                    // find smallest greater
                    for (int j = i; j < arr.Length; j++)
                    {
                        if (arr[j] < arr[smallestgreateridx] && arr[j] > arr[i - 1])
                        {
                            smallestgreateridx = j;
                        }
                    }

                    // swap
                    char temp = arr[i - 1];
                    arr[i - 1] = arr[smallestgreateridx];
                    arr[smallestgreateridx] = temp;

                    // sort
                    Array.Sort(arr, i, arr.Length - i);
                    return new string(arr);
                }
            }

            return "no answer";
        }

        //https://www.hackerrank.com/challenges/the-grid-search/problem
        static string gridSearch(string[] G, string[] P)
        {
            int h = G.Length - P.Length;
            int w = G[0].Length - P[0].Length;

            for (int i = 0; i <= h; i++)
            {
                for (int j = 0; j <= w; j++)
                {
                    if (G[i][j] == P[0][0])
                    {
                        int k;

                        for (k = 0; k < P.Length; k++)
                        {
                            if (G[i + k].Substring(j, P[0].Length) != P[k])
                                break;
                        }

                        if (k == P.Length) return "YES";
                    }
                }
            }

            return "NO";
        }
    }
}
