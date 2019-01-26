using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Algorithms.Implementation
{
    // method signatures match hackerranks.
    // all solutions pass 100% test cases

    public static class Easy
    {
        // https://www.hackerrank.com/challenges/grading/problem
        public static int[] gradingStudents(int[] grades)
        {
            int temp;

            for (int i = 0; i < grades.Length; i++)
            {
                temp = grades[i] % 5;
                if (grades[i] >= 38 && temp > 2)
                    grades[i] += (5 - temp);
            }

            return grades;
        }

        // https://www.hackerrank.com/challenges/apple-and-orange/problem
        public static void countApplesAndOranges(int s, int t, int a, int b, int[] apples, int[] oranges)
        {
            int goodApples = 0;
            int goodOranges = 0;

            foreach (int apple in apples)
            {
                int location = apple + a;
                if (location >= s && location <= t)
                {
                    goodApples++;
                }
            }

            foreach (int orange in oranges)
            {
                int location = orange + b;
                if (location >= s && location <= t)
                {
                    goodOranges++;
                }
            }

            Console.WriteLine(goodApples);
            Console.WriteLine(goodOranges);
        }

        // https://www.hackerrank.com/challenges/kangaroo/problem
        public static string kangaroo(int x1, int v1, int x2, int v2)
        {
            // input constraint guarantees x1 < x2
            if (v1 <= v2) return "NO";

            while (x1 < x2)
            {
                x1 += v1;
                x2 += v2;
            }

            return x1 == x2 ? "YES" : "NO";
        }

        // https://www.hackerrank.com/challenges/between-two-sets/problem
        public static int getTotalX(int[] a, int[] b, int n, int m)
        {
            int count = 0;
            int between = 0;
            int aMax = a.Max();
            int bMin = b.Min();
            for (int i = aMax; i <= bMin; i++)
            {
                //2 4| 5 6 7 8 9 10 11 12 13 14 15 |16 32 96
                //       ^

                count = 0;
                foreach (var num in a)
                {
                    if (i % num == 0) count++;
                    else break;
                }
                foreach (var num in b)
                {
                    if (num % i == 0) count++;
                    else break;
                }
                if (count == n + m) between++;
            }

            return between;
        }

        // https://www.hackerrank.com/challenges/breaking-best-and-worst-records/problem
        public static int[] breakingRecords(int[] scores)
        {
            int maxCount = 0;
            int minCount = 0;
            int max = scores[0];
            int min = scores[0];

            for (int i = 1; i < scores.Length; i++)
            {
                if (scores[i] > max)
                {
                    max = scores[i];
                    maxCount++;
                }
                else if (scores[i] < min)
                {
                    min = scores[i];
                    minCount++;
                }
            }
            return new int[] { maxCount, minCount };
        }

        // https://www.hackerrank.com/challenges/the-birthday-bar/problem
        public static int birthday(int n, int[] s2, int d, int m, List<int> s)
        {
            int count = 0;
            int daySum = 0;

            for (int i = 0; i < m; i++) daySum += s[i];

            if (daySum == d) count++;

            for (int i = m; i < s.Count; i++)
            {
                // add the value of (current i - first i occurring in last segment)
                daySum += (s[i] - s[i - m]);
                if (daySum == d) count++;
            }

            return count;
        }

        // https://www.hackerrank.com/challenges/divisible-sum-pairs/problem
        public static int divisibleSumPairs(int n, int k, int[] ar)
        {
            int pairs = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if ((ar[i] + ar[j]) % k == 0)
                    {
                        pairs++;
                    }
                }
            }

            return pairs;
        }

        // https://www.hackerrank.com/challenges/migratory-birds/problem
        public static int migratoryBirds(List<int> arr)
        {
            int[] types = new int[6];

            foreach (int type in arr) types[type]++;
            return Array.IndexOf(types, types.Max(), 1);
        }

        // https://www.hackerrank.com/challenges/bon-appetit/problem
        public static string bonAppetit(int n, int k, int b, int[] ar, List<int> bill)
        {
            int annatotal = bill.Sum() - bill[k];

            if (annatotal / 2 < b) return (b - (annatotal / 2)).ToString();
            else return "Bon Appetit";
        }

        // https://www.hackerrank.com/challenges/sock-merchant/problem
        public static int SockMerchant(int n, int[] ar)
        {
            int pairs = 0;
            HashSet<int> socks = new HashSet<int>();

            foreach (int sock in ar)
            {
                if (socks.Contains(sock))
                {
                    socks.Remove(sock);
                    pairs++;
                }
                else socks.Add(sock);
            }

            return pairs;
        }

        // https://www.hackerrank.com/challenges/drawing-book/problem
        public static int pageCount(int n, int p)
        {
            if (p == 1 || p == n) return 0;

            if (p <= (n / 2)) return p / 2;

            if (n % 2 == 1) return (n - p) / 2;

            if ((n - p) % 2 == 1) return (n - p) / 2 + 1;

            return (n - p) / 2;
        }

        // https://www.hackerrank.com/challenges/counting-valleys/problem
        public static int countingValleys(int n, string s)
        {
			int sea = 0;
            int numValley = 0;

            for (int i = 0; i < n; i++)
            {
                sea += s[i] == 'U' ? 1 : -1;
                numValley += (sea == 0 && s[i] == 'U') ? 1 : 0;
            }
            return numValley;
        }

        // https://www.hackerrank.com/challenges/electronics-shop/problem
        public static int getMoneySpent(int[] keyboards, int[] drives, int b)
        {
            int temp;
            int high = 0;
            Array.Sort(drives);
            Array.Sort(keyboards);

            if (keyboards[0] + drives[0] > b) return -1;

            foreach (int key in keyboards)
            {
                foreach (int usb in drives)
                {
                    temp = key + usb;

                    if (temp < b && temp > high) high = temp;
                    else if (temp == b) return temp;
                    else if (temp > b) break;
                }
            }

            return high;
        }

        // https://www.hackerrank.com/challenges/cats-and-a-mouse/problem
        public static string CatAndMouse(int q, int[] tokens, int x, int y, int z)
        {
            int a = x < z ? z - x : x - z;
            int b = y < z ? z - y : y - z;

            if (a > b) return "Cat B";
            else if (b > a) return "Cat A";
            else return "Mouse C";
        }

        // https://www.hackerrank.com/challenges/picking-numbers/problem
        public static int pickingNumbers(List<int> a)
        {
            int max = 0;
            Dictionary<int, int> values = new Dictionary<int, int>();

            foreach (int num in a)
            {
                if (values.ContainsKey(num)) values[num]++;
                else values.Add(num, 1);
            }

            foreach (int k in values.Keys)
            {
                if (values.ContainsKey(k + 1))
                    max = Math.Max(max, values[k] + values[k + 1]);

                // for an odd case when there is no integer partner,
                // rather a single integer that is repeated the most
                else max = Math.Max(max, values[k]);
            }

            return max;
        }

        // https://www.hackerrank.com/challenges/the-hurdle-race/problem
        public static int hurdleRace(int k, int[] height)
        {
            int max = height.Max();
            return k > max ? 0 : max - k;
        }

        // https://www.hackerrank.com/challenges/designer-pdf-viewer/problem
        public static int designerPdfViewer(int[] h, string word)
        {
            int maxheight = 0;

            // find the greatest character value in word.
            // yeild an index by subtracting the ascii
            // value of 'a' from the current ch
            foreach (char ch in word)
            {
                if (h[ch - 97] > maxheight) maxheight = h[ch - 97];
            }

            return word.Length * maxheight;
        }

        // https://www.hackerrank.com/challenges/utopian-tree/problem
        public static int utopianTree(int n)
        {
            int height = 1;

            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 1) height++;
                else height *= 2;
            }
            return height;
        }

        // https://www.hackerrank.com/challenges/angry-professor/problem
        public static string angryProfessor(int k, int[] a)
        {
            int ontime = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] <= 0) ontime++;
            }
            return ontime < k ? "YES" : "NO";
        }

        // https://www.hackerrank.com/challenges/beautiful-days-at-the-movies/problem
        //================================================================================
        public static int beautifulDays(int i, int j, int k)
        {
            int rev;
            int beaut = 0;

            for (; i <= j; i++)
            {
                rev = reverseNumber(i);
                if ((i - rev) % k == 0) beaut++;
            }

            return beaut;
        }

        public static int reverseNumber(int num)
        {
            string str = new string(num.ToString().Reverse().ToArray());
            return int.Parse(str);
        }
        //================================================================================
         
        // https://www.hackerrank.com/challenges/strange-advertising/problem
        public static int viralAdvertising(int n)
        {
            int liked = 0;
            int shared = 5;

            for (int i = 0; i < n; i++)
            {
                shared /= 2;
                liked += shared;
                shared *= 3;
            }
            return liked;
        }

        // https://www.hackerrank.com/challenges/save-the-prisoner/problem
        public static long SaveThePrisoner(long n, long m, long s)
        {       
            long a = (m + s - 1) % n;
            return a == 0 ? n : a;
        }

        // https://www.hackerrank.com/challenges/circular-array-rotation/problem
        public static int[] circularArrayRotation(int[] a, int k, int[] queries)
        {
            int rotator = k % a.Length;
            int[] result = new int[queries.Length];

            for (int i = 0; i < queries.Length; i++)
            {
                if (queries[i] - rotator >= 0)
                    result[i] = a[queries[i] - rotator];
                else
                    result[i] = a[queries[i] - rotator + a.Length];
            }

            return result;
        }

        // https://www.hackerrank.com/challenges/jumping-on-the-clouds-revisited/problem
        public static int jumpingOnClouds(int[] c, int k)
        {
            int n = c.Length;
            int e = 100;
            int idx = 0;
            do
            {
                idx = (idx + k) % n;
                e -= c[idx] == 0 ? 1 : 3;
            }
            while (idx != 0);

            return e;
        }

        // https://www.hackerrank.com/challenges/find-digits/problem
        public static int findDigits(int n)
        {
            int count = 0;
            string nstr = n.ToString();
            byte[] digits = new byte[nstr.Length];

            for (byte i = 0; i < digits.Length; i++)
                digits[i] = Convert.ToByte(nstr[i].ToString());

            foreach (byte digit in digits)
                if (digit != 0 && n % digit == 0) count++;

            return count;
        }

        // https://www.hackerrank.com/challenges/sherlock-and-squares/problem
        public static int squares(int a, int b)
        {
            return (int)(Math.Floor(Math.Sqrt(b)) - Math.Ceiling(Math.Sqrt(a))) + 1;
        }

        // https://www.hackerrank.com/challenges/library-fine/problem
        public static int libraryFine(int d1, int m1, int y1, int d2, int m2, int y2)
        {
            if (y1 > y2) return 10000;

            else if (y1 == y2)
            {
                if (m1 > m2) return 500 * (m1 - m2);

                else if (m1 == m2 && d1 > d2) return 15 * (d1 - d2);
            }

            return 0;
        }

        /*
            sticks-length         length-of-cut   sticks-cut
            1 2 3 4 3 3 2 1         1               8
            _ 1 2 3 2 2 1 _         1               6
            _ _ 1 2 1 1 _ _         1               4
            _ _ _ 1 _ _ _ _         1               1
            _ _ _ _ _ _ _ _       DONE            DONE


            1 1 2 2 3 3 3 4
                          ^
            2 2 4 4 5 8 8 = 6 4 2 2
                      ^

            3 5 8 8 10 12 14 14 = 8 7 6 4 3 2
                             ^              
        */
        // https://www.hackerrank.com/challenges/cut-the-sticks/problem
        public static int[] cutTheSticks(int[] arr)
        {
            int min = 0;
            int removed = 0;
            Array.Sort(arr);
            List<int> cuts = new List<int>();

            for (int i = 0; i < arr.Length; i++)
            {
                while (i < arr.Length && arr[i] == min)
                {
                    removed++; i++;
                }

                if (i < arr.Length)
                {
                    min = arr[i];
                    cuts.Add(arr.Length - removed);
                    removed++;
                }
            }

            return cuts.ToArray();
        }

        // https://www.hackerrank.com/challenges/repeated-string/problem
        public static long repeatedString(string s, long n)
        {
            // count the number of 'a' occurances in s
            long a = s.Where(c => c == 'a').LongCount();

            // multiply a by the number of times s occures in n 
            a *= n / s.Length;

            // mod n by the length of s, yeild the remainder
            n %= s.Length;

            // search the remaining substring
            return a + s.Substring(0, (int)n).Where(c => c == 'a').Count();
        }

        // https://www.hackerrank.com/challenges/jumping-on-the-clouds/problem
        public static int jumpingOnClouds(int[] c)
        {
            // 0 0 1 0 0 1 0 = 4
            //           ^

            // 0 0 0 1|0 0 = 2
            //         ^

            int i;
            int jumps = 0;
            int len = c.Length - 2;

            for (i = 0; i < len; i++)
            {
                if (c[i + 2] == 0) i++;
                jumps++;
            }

            // check if the last iteration would have been c[n - 2]
            return i == len ? ++jumps : jumps;
        }

        // https://www.hackerrank.com/challenges/equality-in-a-array/problem
        public static int equalizeArray(int[] arr)
        {
            int temp = 1;
            int high = 1;
            Array.Sort(arr);
            int n = arr.Length - 1;

            for (int i = 0; i < n; i++)
            {
                if (arr[i] == arr[i + 1]) temp++;
                else
                {
                    if (temp > high) high = temp;
                    temp = 1;
                }
            }

            return temp > high ? ++n - temp : ++n - high;
        }

        // a simple brute force solution
        // https://www.hackerrank.com/challenges/acm-icpc-team/problem
        public static int[] acmTeam(string[] topic)
        {
            int temp = 0;
            int high = 0;
            int teams = 0;
            int len = topic.Length;
            int m = topic[0].Length;

            for (int i = 0; i < len; i++)
            {
                for (int j = i + 1; j < len; j++)
                {
                    for (int k = 0; k < m; k++)
                    {
                        if (topic[i][k] == '1' || topic[j][k] == '1')
                        {
                            temp++;
                        }
                    }

                    if (temp == high) teams++;
                    else if (temp > high)
                    {
                        high = temp;
                        teams = 1;
                    }

                    temp = 0;
                }
            }

            return new int[] { high, teams };
        }

        // https://www.hackerrank.com/challenges/taum-and-bday/problem
        // in some test cases, hackerranks input stream exceeds the
        // limits of data types specified in their method signature.

        // input stream contains numbers larger than or calculate to numbers 
        // larger than the int data type specified in their method signature. 
        // this methods parameter and return data types have been changed
        // to avoid int overflow and type exceptions
        public static long taumBday(long b, long w, long bc, long wc, long z)
        {
            if (bc > wc)
                 return b * bc > (b * wc) + (b * z) ? (b + w) * wc + b * z : b * bc + w * wc;
            else return w * wc > (w * bc) + (w * z) ? (b + w) * bc + w * z : b * bc + w * wc;
        }

        // https://www.hackerrank.com/challenges/kaprekar-numbers/problem
        public static int[] kaprekarNumbers(int p, int q)
        {
            string left;
            string right;
            string strsq;
            List<int> kapNum = new List<int>();

            // handle the odd cases concerning a single digit
            if (p < 10)
            {
                if (p == 1) kapNum.Add(1);
                if (p <= 9 && q >= 9) kapNum.Add(9);
                p = 11;
            }

            for (int i = p; i <= q; i++)
            {
                strsq = ((long)i * i).ToString();

                left = strsq.Substring(0, strsq.Length / 2);
                right = strsq.Substring(strsq.Length / 2);

                if ((int.Parse(left) + int.Parse(right)) == i) kapNum.Add(i);                
            }

            if (kapNum.Count == 0) Console.WriteLine("INVALID RANGE");

            return kapNum.ToArray();
        }

        // https://www.hackerrank.com/challenges/beautiful-triplets/problem
        static int beautifulTriplets(int d, int[] arr)
        {
            int beautiful = 0;
            Dictionary<int, List<int>> vals = new Dictionary<int, List<int>>(arr.Length);

            for (int i = 0; i < arr.Length; i++)
            {
                if (vals.ContainsKey(arr[i])) vals[arr[i]].Add(i);
                else
                    vals.Add(arr[i], new List<int>() { i });
            }

            for (int i = 0; i < arr.Length; i++)
            {
                if (vals.ContainsKey(arr[i] + d))
                {
                    beautiful +=
                    vals[arr[i] + d]
                    .Where(j => j > i && vals.ContainsKey(arr[j] + d))
                    .Where(j => vals[arr[j] + d].Where(k => k > j).Count() > 1).Count();
                }
            }

            return beautiful;

            //int beautiful = 0;
            ////    . .    .     .
            ////1 6 7 7 8 10 12 13 14 19
            ////  ^ ^ ^
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    for (int j = i + 1; j < arr.Length; j++)
            //    {
            //        if (arr[j] - arr[i] == d)
            //        {
            //            for (int k = j + 1; k < arr.Length; k++)
            //            {
            //                if (arr[k] - arr[j] == d) beautiful++;
            //            }
            //        }
            //    }
            //}
            //return beautiful;
        }

        // https://www.hackerrank.com/challenges/minimum-distances/problem
        static int minimumDistances(int[] a)
        {
            int min = a.Length;
            bool hasPair = false;

            for (int i = 0; i < a.Length - 1; i++)
            {
                for (int j = i + 1; j < a.Length; j++)
                {
                    if (a[i] == a[j] && j - i < min)
                    {
                        min = j - i;
                        hasPair = true;
                    }
                }
            }
            return hasPair ? min : -1;
        }

        // https://www.hackerrank.com/challenges/halloween-sale/problem
        static int howManyGames(int p, int d, int m, int s)
        {
            int total = 0;

            while (s >= p)
            {
                s -= p;
                p -= d;

                if (p < m)
                { p = m; }

                total++;
            }

            return total;
        }

        // https://www.hackerrank.com/challenges/chocolate-feast/problem
        static int chocolateFeast(int n, int c, int m)
        {
            int chocs = n / c;
            int wrappers = chocs;

            while (wrappers >= m)
            {
                wrappers -= m;
                wrappers++;
                chocs++;
            }
            return chocs;
        }

        // https://www.hackerrank.com/challenges/service-lane/problem
        static int[] serviceLane(int n, int[][] cases, int[] width)
        {
            int min = 3;
            int[] sizes = new int[cases.Length];

            for (int i = 0; i < cases.Length; i++, min = 3)
            {
                for (int j = cases[i][0]; j <= cases[i][1]; j++)
                {
                    if (width[j] < min) min = width[j];
                }
                sizes[i] = min;
            }
            return sizes;
        }

        // https://www.hackerrank.com/challenges/lisa-workbook/problem
        static int workbook(int n, int k, int[] arr)
        {
            int pagecount = 1;
            int special = 0; // tally up the special problems
            int exerciseL; // keep track of the exercise min bound for the current page
            int exerciseR; // keep track of the exercise max bound for the current page
            int lastex; // store the exercise count of the last page in the chapter
            int pages; // number of pages in current chapter

            for (int i = 0; i < n; i++)
            {
                pages = (arr[i] / k) + (arr[i] % k > 0 ? 1 : 0);
                exerciseL = 1;
                exerciseR = k;

                for (int j = 1; j < pages; j++)
                {
                    //flip through the pages of the chapter
                    if (pagecount >= exerciseL && pagecount <= exerciseR) special++;//found special

                    exerciseL += k;
                    exerciseR += k;
                    pagecount++;
                }

                if (pages == 1) lastex = arr[i];
                else lastex = exerciseL + (arr[i] % k == 0 ? k : arr[i] % k) - 1;

                if (pagecount >= exerciseL && pagecount <= lastex) special++;
                pagecount++;
            }

            return special;
        }

        // https://www.hackerrank.com/challenges/flatland-space-stations/problem
        static int flatlandSpaceStations(int n, int[] c)
        {
            int end;
            int temp;
            Array.Sort(c);
            int middles = 0;

            for (int i = 0; i < c.Length - 1; i++)
            {
                temp = (c[i + 1] - c[i]);
                if (temp > middles) middles = temp;
            }

            if (c[0] > (n - c[c.Length - 1] - 1)) end = c[0];
            else end = (n - c[c.Length - 1] - 1);

            middles /= 2;

            return middles > end ? middles : end;
        }

        // https://www.hackerrank.com/challenges/fair-rations/problem
        static string fairRations(int[] B)
        {

            if (B.Sum() % 2 == 1) return "NO";

            int distribs = 0;
            bool isEven = false;

            while (!isEven)
            {
                isEven = true;
                for (int i = 0; i < B.Length; i++)
                {
                    if (B[i] % 2 == 1)
                    {
                        B[i] += 1;
                        B[i + 1] += 1;
                        distribs += 2;
                        isEven = false;
                    }
                }
            }
            return distribs.ToString();
        }

        // https://www.hackerrank.com/challenges/manasa-and-stones/problem
        static int[] stones(int n, int a, int b)
        {
            if (a == b) return new int[] { a * (n - 1) };

            int[] end1 = new int[n];

            int s = Math.Min(a, b);
            int l = Math.Max(a, b);

            int end = s * (n - 1);
            int next = l - s;

            for (int i = 0; i < n; i++)
            {
                end1[i] = end;
                end += next;
            }

            return end1;
        }

        // https://www.hackerrank.com/challenges/cavity-map/problem
        static string[] cavityMap(string[] grid)
        {
            int n = grid.Length;
            int len = n - 1;

            for (int i = 1; i < len; i++)
            {
                for (int j = 1; j < len; j++)
                {
                    if (grid[i][j] > grid[i][j - 1] && grid[i][j] > grid[i][j + 1] &&
                        grid[i][j] > grid[i - 1][j] && grid[i][j] > grid[i + 1][j])
                    {
                        char[] a = grid[i].ToCharArray();
                        a[j] = 'X';
                        grid[i] = string.Join("", a);
                    }
                }
            }

            return grid;
        }

        // https://www.hackerrank.com/challenges/happy-ladybugs/problem
        static string happyLadybugs(string b)
        {
            if (b.Length < 2) return "NO";

            string result = "YES";
            Dictionary<char, int> distinct = new Dictionary<char, int>();
            bool isSad = false;

            distinct.Add(b[0], 1);
            if (!distinct.ContainsKey(b[b.Length - 1])) distinct.Add(b[b.Length - 1], 1);
            else distinct[b[b.Length - 1]]++;

            for (int i = 1; i < b.Length - 1; i++)
            {
                if (!distinct.ContainsKey(b[i])) distinct.Add(b[i], 1);
                else
                    distinct[b[i]] += 1;

                if (!isSad && b[i - 1] != b[i] && b[i + 1] != b[i]) isSad = true;
            }

            foreach (var kvp in distinct)
            {
                if (kvp.Value < 2 && kvp.Key != '_')
                {
                    result = "NO";
                    break;
                }
            }

            if (isSad && !distinct.ContainsKey('_')) result = "NO";
            return result;           
        }
		
		// https://www.hackerrank.com/challenges/strange-code/problem
    	static long strangeCounter(long t)
		{
			long time = 1;
			long value = 3;

			while(t >= time)
			{
				time += value;
				value *= 2;
			}
			
			value /= 2;
			time -= value;

			return time + value - t;
    	}

        // https://www.hackerrank.com/challenges/append-and-delete/problem
        static string appendAndDelete(string s, string t, int k)
        {
            int i = 0;

            // iterate through both strings until OOB
            // or found first non matching characters
            while (i < s.Length && i < t.Length)
            {
                if (s[i] == t[i]) { i++; }
                else break;
            }

            // calculate:
            // ops - the minimum number of operations
            // to be performed on s to match t
            int ops = (s.Length - i) + (t.Length - i);

            // check two failing conditions:
            // * if minimum number ops required is greater than k - fail
            //   or
            // * check odds and evens && if this could not be  
            //   corrected by deleting on an empty string - fail
            if (ops > k || (ops % 2 != k % 2 && k <= s.Length + t.Length))
            {
                return "No";
            }

            return "Yes";
        }

        // https://www.hackerrank.com/challenges/permutation-equation/problem
        public static int[] permutationEquation(int[] p)
        {
            Dictionary<int, int> indexes = new Dictionary<int, int>(p.Length);

            for (int i = 0; i < p.Length; i++) indexes.Add(p[p[i] - 1], i + 1);

            for (int i = 1; i <= p.Length; i++) p[i - 1] = indexes[i];

            return p;
        }
    }      
}
