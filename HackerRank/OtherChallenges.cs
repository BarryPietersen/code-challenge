using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HackerRank
{
    public static class OtherChallenges
    {
        /*
            calculate the overlapping area of two rectangles on a 2d plane

            a white board interview question sometimes asked by large tech companies.
            the problem was introduced to me by Irfan Baqui, through his youtube channel
            https://www.youtube.com/channel/UCYvQTh9aUgPZmVH0wNHFa1A

            first successful implementaion was developed before solutions in the video,
            the additional use of the Math class results in much cleaner code -
            this hint was observed in the video.

            the input assumes:
                              bx and by - the x,y of the lower left corner of a rectangle
                              tx and ty - the x,y of the upper right corner of a rectangle
        */
        //================================================================================
        public static int Overlap(int bx1, int by1, int tx1, int ty1,
                                  int bx2, int by2, int tx2, int ty2)
        {
            // check if either x or y coordinates are out of bounds
            // such that, any rectangle overlap is not possible
            if (bx1 >= tx2 || bx2 >= tx1 ||
                by1 >= ty2 || by2 >= ty1) { return 0; }

            return Math.Abs(Math.Max(bx1, bx2) - Math.Min(tx1, tx2)) *
                   Math.Abs(Math.Max(by1, by2) - Math.Min(ty1, ty2));
        }
        //================================================================================

        // find the nth root of a target value within the
        // degree of accuracy indicated by the epsilon parameter
        //================================================================================
        public static double FindNthRoot(double target, int n, float epsilon = 0.00001f)
        {
            if (n <= 1) { /*optionally handle this condition*/ };

            double mid = target / 2;
            double pow = Math.Pow(mid, n);

            while (Math.Abs(target - pow) > epsilon)
            {
                if (pow > target) { mid -= mid / 2; }
                else { mid += mid / 2; }

                pow = Math.Pow(mid, n);
            }

            return mid;
        }
         
        // problem set on solo learn app
        public static int[] NBonacciNumbers(int m, int n)
        {
            int[] bonacci = new int[m];
            bonacci[n - 1] = 1;
            bonacci[n] = 1;
            int sum = 1;

            for (int i = n + 1; i < m; i++)
            {
                sum -= bonacci[i - n - 1];
                sum += bonacci[i - 1];
                bonacci[i] = sum;
            }

            return bonacci;
        }

        // a merge sort implementation
        //int[] unsorted = { 5, 3, 6, 4, 4, 2, 8, 3, 5, 10, 18 };
        //================================================================================
        public static int[] MergeSort(int[] arr)
        {
            if (arr.Length < 2) return arr;

            int mid = arr.Length / 2;

            int[] left = new int[mid];
            int[] rght = new int[mid + arr.Length % 2];

            Array.Copy(arr, 0, left, 0, mid);
            Array.Copy(arr, mid, rght, 0, mid + arr.Length % 2);

            return merge(MergeSort(left), MergeSort(rght));
        }

        private static int[] merge(int[] left, int[] rght)
        {
            int l = 0, r = 0;
            int[] res = new int[left.Length + rght.Length];

            while (l < left.Length && r < rght.Length)
            {
                res[l + r] = left[l] < rght[r] ? left[l++] : rght[r++];
            }

            if (l < left.Length)
                Array.Copy(left, l, res, l + r, left.Length - l);
            else
                Array.Copy(rght, r, res, l + r, rght.Length - r);

            return res;
        }
        //================================================================================

        // traverse the two strings and check for character equivalence, ignoring any punctuation.
        // this challenge was part of an interview screening process, issued to one of my colleagues.
        // the original implementation would fail some test cases, has been debugged and now passes
        public static bool MatchStringNoPunctuation(string a, string b)
        {
            int aI = 0;
            int bI = 0;

            while (aI < a.Length && bI < b.Length)
            {
                if (char.ToLower(a[aI]) == char.ToLower(b[bI])) { aI++; bI++; }
                else if (char.IsPunctuation(a[aI]) || char.IsWhiteSpace(a[aI])) aI++;
                else if (char.IsPunctuation(b[bI]) || char.IsWhiteSpace(b[bI])) bI++;
                else return false;
            }

            if (aI < a.Length)
            {
                foreach (var ch in a.Substring(aI)) { if (char.IsLetter(ch)) return false; }
                return true;
            }
            else if (bI < b.Length)
            {
                foreach (var ch in b.Substring(bI)) { if (char.IsLetter(ch)) return false; }
                return true;
            }

            return (aI >= a.Length - 1 && bI >= b.Length - 1);
        }

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

        // problem set on solo learn app -
        // a number is said to be semi prime if there exists:
        // two primes (not necessarily distinct) whos product is equal to the number
        //================================================================================
        public static bool IsSemiPrime(int num)
        {
            HashSet<double> primes = new HashSet<double>();
            double len = num / 2;

            for (int i = 2; i <= len; i++)
                if (_isPrime(i)) primes.Add(i);

            foreach (double p in primes)
            {
                if (primes.Contains(num / p))
                    return true;
            }

            return false;
        }

        private static bool _isPrime(int num)
        {
            double root = Math.Sqrt(num);

            for (int i = 2; i <= root; i++)
            {
                if (num % i == 0)
                    return false;
            }

            return true;
        }
        //================================================================================

        //================================================================================
        // given two strings, merge the strings in and alternating fashion character by character.
        // - if one string is larger we must append the remaining characters to the new string
        // a "aceg"
        // b "bdf"
        // = "abcdefg"
        //
        // a "abcdefgh"
        // b "xyz"
        // = "axbyczdefgh"

        // assumption -
        // to give the resulting string the best chance at being balanced evenly we 
        // begin the merge on the larger string
        public static string MergeStrings(string a, string b)
        {
            StringBuilder sb = new StringBuilder(a.Length + b.Length);

            if (b.Length > a.Length)
            {
                string temp = b;
                b = a;
                a = temp;
            }

            int ai = 0;
            int bi = 0;

            while (bi < b.Length)
            {
                sb.Append(a[ai++]);
                sb.Append(b[bi++]);
            }

            sb.Append(a.Substring(ai));

            return sb.ToString();
        }

        // https://www.hackerrank.com/challenges/frequency-queries/problem
        static List<int> freqQuery(List<int[]> queries)
        {
            List<int> present = new List<int>();
            Dictionary<int, int> totals = new Dictionary<int, int>();
            Dictionary<int, int> frequency = new Dictionary<int, int>();

            foreach (var query in queries)
            {
                if (query[0] == 1)
                {
                    if (frequency.ContainsKey(query[1]))
                    {
                        totals[frequency[query[1]]]--;
                        frequency[query[1]]++;
                    }
                    else
                        frequency.Add(query[1], 1);

                    if (totals.ContainsKey(frequency[query[1]])) totals[frequency[query[1]]]++;
                    else
                        totals.Add(frequency[query[1]], 1);
                }
                else if (query[0] == 2)
                {
                    if (frequency.ContainsKey(query[1]) && frequency[query[1]] > 0)
                    {
                        totals[frequency[query[1]]]--;
                        frequency[query[1]]--;

                        if (totals.ContainsKey(frequency[query[1]])) totals[frequency[query[1]]]++;
                        else
                            totals.Add(frequency[query[1]], 1);
                    }
                }
                else present.Add(totals.ContainsKey(query[1]) && totals[query[1]] > 0 ? 1 : 0);
            }

            return present;
        }

        // https://www.hackerrank.com/challenges/ctci-ransom-note/problem
        static void checkMagazine(string[] magazine, string[] note)
        {
            Dictionary<string, int> words = new Dictionary<string, int>();

            foreach (string word in note)
                if (!words.ContainsKey(word)) words.Add(word, 1);
                else words[word]++;

            foreach (string word in magazine)
                if (words.ContainsKey(word))
                {
                    if (words[word] > 1) words[word]--;
                    else words.Remove(word);
                }

            Console.WriteLine(words.Count() == 0 ? "Yes" : "No");
        }

        //================================================================================
        // https://www.hackerrank.com/challenges/jim-and-the-skyscrapers/problem
        static long solve(int[] arr)
        {
            long total = 0;
            Stack<int> stk = new Stack<int>();

            for (int i = 0; i < arr.Length; i++)
            {
                if (stk.Count == 0) { stk.Push(arr[i]); continue; }

                if (arr[i] > stk.Peek())
                {
                    // at this point we calculate and remove all elements
                    // less than the current a[i] on the stack
                    while (stk.Count > 0 && arr[i] > stk.Peek())
                    {
                        total += CalculatePaths(stk);
                    }
                }

                stk.Push(arr[i]);
            }

            while (stk.Count > 0) total += CalculatePaths(stk);

            return total;
        }

        private static long CalculatePaths(Stack<int> stk)
        {
            if (stk.Count == 0) return 0;

            long reps = 0;
            long count = 0;
            long temp = stk.Pop();

            while (stk.Count > 0 && temp == stk.Peek())
            {
                reps++;
                count += reps;
                temp = stk.Pop();
            }

            return count * 2;
        }
        //================================================================================

        // fibonacci using recursive, memoization techniques
        //================================================================================
        public static int NthFibRecursive(int n)
        {
            Dictionary<int, int> found = new Dictionary<int, int>();
            return fib(n, found);
        }

        private static int fib(int n, Dictionary<int, int> found)
        {
            if (found.ContainsKey(n)) return found[n];
            if (n <= 2) return 1;

            found.Add(n, fib(n - 1, found) +
                         fib(n - 2, found));

            return found[n];
        }

        // an iterative solution for finding the nth fib value
        public static int NthFibIterative(int n)
        {
            int temp;
            int l = 0;
            int r = 1;

            for (int i = 1; i < n; i++)
            {
                temp = l + r;
                l = r;
                r = temp;
            }

            return r;
        }
        //================================================================================

        //test password strength here, using ascii values
        public static bool TestPasswordStrength(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;

            int up = 0, lo = 0, dig = 0, sp = 0;

            foreach (char ch in password)
            {
                if (ch > 96 && ch < 123) lo++;
                else if (ch > 64 && ch < 91) up++;
                else if (ch > 47 && ch < 58) dig++;
                // special characters
                else if ((ch > 32 && ch < 48) ||
                         (ch > 57 && ch < 65) ||
                         (ch > 90 && ch < 97) ||
                         (ch > 122 && ch < 127)) sp++;
            }
            return up > 0 && lo > 1 && dig > 1 && sp > 1;
        }

        // int[] sorted = { 1, 2, 4, 5, 7, 8, 9, 10, 14, 20 };
        public static bool BinarySearchIterative(int[] sorted, int n)
        {
            int mid;
            int lft = 0;
            int rht = sorted.Length - 1;

            while (lft <= rht)
            {
                mid = (lft + rht) / 2;

                if (sorted[mid] > n) rht = mid - 1;
                else if (sorted[mid] < n) lft = mid + 1;
                else return true;
            }

            return false;
        }

        public static bool BinarySearchRecursive(int[] sorted, int n)
        {
            int l = 0;
            int r = sorted.Length - 1;

            return BinarySearchRecursive(sorted, n, l, r);
        }

        private static bool BinarySearchRecursive(int[] sorted, int n, int l, int r)
        {
            if (l > r) return false;

            int m = (l + r) / 2;

            if (sorted[m] > n) return BinarySearchRecursive(sorted, n, l, m - 1);
            if (sorted[m] < n) return BinarySearchRecursive(sorted, n, m + 1, r);

            return true;
        }

        /*
            1
            2 3
            4 5 6
            7 8 9 10
            11 12 13 14 15

            floyds triangle
        */
        public static string FloydsTriangle(int height)
        {
            if (height < 1) return "";

            int n = 1;
            int level = 1;
            StringBuilder sb = new StringBuilder();

            while (level <= height)
            {
                for (int i = 1; i < level; i++)
                {
                    sb.Append($"{n++} ");
                }

                level++;
                sb.Append($"{n++}\n");
            }

            return sb.ToString();
        }

        // codewars https://www.codewars.com/kata/dubstep/train/csharp
        public static string SongDecoder(string input)
        {
            return string.Join(" ", new Regex("WUB").Replace(input, " ").Split(' ').Where(s => !string.IsNullOrEmpty(s)));
        }

        // codewars https://www.codewars.com/kata/54e6533c92449cc251001667/train/csharp
        public static IEnumerable<T> UniqueInOrder<T>(IEnumerable<T> iterable)
        {
            List<T> result = new List<T>();

            if (iterable.Count() == 0) return result;

            result.Add(iterable.First());

            foreach (var item in iterable)
                if (!result.Last().Equals(item))
                    result.Add(item);

            return result;
        }

        /*
            rotate a matrix 90 degrees clockwise (in place)

            1 2 3
            4 5 6
            7 8 9

            1  2  3  4 
            5  6  7  8
            9  10 11 12
            13 14 15 16

            1  2  3  4  5
            6  7  8  9  10
            11 12 13 14 15
            16 17 18 19 20
            21 22 23 24 25

            int[,] matrix_3x3 = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            int[,] matrix_4x4 = new int[4, 4] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 16 } };
            int[,] matrix_5x5 = new int[5, 5] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 }, { 21, 22, 23, 24, 25 } };
            int[,] matrix_6x6 = new int[6, 6] { { 1, 2, 3, 4, 5, 6 }, { 7, 8, 9, 10, 11, 12 }, { 13, 14, 15, 16, 17, 18 }, { 19, 20, 21, 22, 23, 24 }, { 25, 26, 27, 28, 29, 30 }, { 31, 32, 33, 34, 35, 36 } };
        */

        public static int[,] RotateMatrix(int[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new Exception("the dimensions of the matrix are not equal.");

            // tl - top and left index
            // br - bottom and right index
            int tl = 0;
            int br = matrix.GetLength(0) - 1;
            int m = matrix.GetLength(0) / 2;
            int temp1, temp2;

            while (m > 0)
            {
                for (int i = 0; i < br - tl; i++)
                {
                    temp1 = matrix[tl + i, br];
                    matrix[tl + i, br] = matrix[tl, tl + i];
                    temp2 = matrix[br, br - i];
                    matrix[br, br - i] = temp1;
                    temp1 = matrix[br - i, tl];
                    matrix[br - i, tl] = temp2;
                    matrix[tl, tl + i] = temp1;
                }

                tl++; br--; m--;
            }

            return matrix;
        }
    }
}
