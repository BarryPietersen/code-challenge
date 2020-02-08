using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerRank
{
    public static class Other
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

        /*
            calculate up to the mth term for an n bonacci sequence

            input: m n
            output: nbonnaci sequence upto m

            3 4
            1
            -> [ 0 1 ] 1 2 4 8 15 29 36
                       ^
            5 4
            4
            -> [ 0 1 1 2 ] 4 8 15 29 36
                           ^

            8 4
            29
            -> 0 1 1 [ 2 4 8 15 ] 29 36
                                   ^
        */
        public static int[] NBonacciSequence(int m, int n)
        {
            if (m < 2) return new[] { 0 };

            int[] bonacci = new int[m];
            bonacci[1] = 1;
            int head = 0;

            int i = 2;
            for (; i <= n && i < m; i++)
            {
                head += bonacci[i - 1];
                bonacci[i] = head;
            }
            for (; i < m; i++)
            {
                head += bonacci[i - 1];
                bonacci[i] = head;
                head -= bonacci[i - n];
            }

            return bonacci;
        }


        public static int NBonacciNumber(int m, int n)
        {
            if (m < 2) return 0;

            int head = 1;
            int prev = 0;
            int tail = 0;
            int temp;

            /*
                8 4
                29
                -> 0 1 1 [ 2 4 8 15 ] 29 36
                                       ^
            */

            int i = 3;
            for (; i <= n && i < m; i++)
            {
                temp = head;
                head += prev;
                prev = temp;
            }
            // 0 1 1 2 4 8 15 29 56 108
            //         t          T
            // h = 2
            // p = 1
            // t = 0
            for (; i <= m; i++)
            {
                temp = head;
                head += head - tail;
                tail += prev * 2 - temp;
                prev = temp;
            }

            return head;
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

        // https://www.hackerrank.com/challenges/sherlock-and-moving-tiles/problem
        public static double[] movingTiles(int l, int s1, int s2, double[] queries)
        {
            // velocity - dist traveled apart in 1 sec
            int v = Math.Abs(s1 - s2);

            for (int i = 0; i < queries.Length; i++)
                queries[i] = Math.Sqrt(Math.Pow((l - Math.Sqrt(queries[i])), 2) * 2) / v;

            return queries;
        }

        // count pairs of numbers in an array that sum to k
        // pairs are considered only if:
        // - they are a distinct combination of numbers (to the already considered pairs)
        // - a value cannot be added with itself, only if it occures twice it can
        /*
            nums = [2 4 8 6 10 0 5 5 2 5 -3, 13]
            k    = 10

            output: 5

            explanation: 2, 8
                         4, 6
                         10, 0
                         5, 5
                         -3, 13

            note: we did not consider the second occurance of 2, 8 or 5, 5
        */
        public static int summingPairs(int[] numbers, long k)
        {
            int count = 0;
            long complement;
            HashSet<string> pairs = new HashSet<string>();
            HashSet<long> complements = new HashSet<long>();

            foreach (var num in numbers)
            {
                complement = k - num;

                if (complements.Contains(complement))
                {
                    if (!pairs.Contains($"{num} {complement}") ||
                        !pairs.Contains($"{complement} {num}"))
                    {
                        pairs.Add($"{num} {complement}");
                        count++;
                    }
                }
                else
                    complements.Add(num);
            }

            return count;
        }

        /*
            count groups within a disconnected graph

            approach:
                traverse an undirected disconnected graph using dfs
                counting the number of graphs within the 'forest'

            time:  o(v + e)
            space: o(v + e) 

            input:
                assume the string array represents a matrix,
                each string only contains characters '1' or '0'
                indicating if it is connected to the current i
        */
        public static int countGroups(string[] forest)
        {
            int groups = 0;
            HashSet<int> visited = new HashSet<int>();
            Dictionary<int, HashSet<int>> adjlist = new Dictionary<int, HashSet<int>>();

            // populate our adjacency list o(v + e)
            for (int i = 0; i < forest.Length; i++)
            {
                if (!adjlist.ContainsKey(i))
                {
                    adjlist.Add(i, new HashSet<int>());
                }

                for (int j = 1; j < forest.Length; j++)
                {
                    if (forest[i][j] == '1')
                    {
                        adjlist[i].Add(j);

                        if (!adjlist.ContainsKey(j))
                        {
                            adjlist.Add(j, new HashSet<int>());
                            adjlist[j].Add(i);
                        }
                        else if (!adjlist[j].Contains(i))
                        {
                            adjlist[j].Add(i);
                        }
                    }
                }
            }

            // iterate over adjacency list calling recursive dfs methed
            foreach (var kvp in adjlist)
            {
                if (!visited.Contains(kvp.Key))
                {
                    // increment groups counter each time
                    // we encounter a node that was not
                    // part of another group
                    groups++;
                    dfs(adjlist, visited, kvp.Key);
                }
            }

            return groups;
        }

        // explore a node using recursive depth first search
        private static void dfs(Dictionary<int, HashSet<int>> adjlist, HashSet<int> visited, int node)
        {
            if (visited.Contains(node)) return;

            visited.Add(node);

            foreach (var adjnode in adjlist[node]) dfs(adjlist, visited, adjnode);
        }

        /*
            scenario: a game of boggle
            challenge: write a program that returns a list of all the found words in a boggle matrix

            input:
                    a list of valid words
                    a character matrix

            output:
                    a list of found words

            char[,] board = { 
                { 'c', 'h', 'd', 'r' },
                { 'a', 'i', 'e', 'd' },
                { 'r', 'n', 'v', 'e' },
                { 'e', 'l', 'f', 'r' },
                { 'b', 'e', 'u', 'p' },
                { 'w', 'l', 'i', 'y' },
                { 'k', 'e', 'v', 'e' },
                { 'w', 'i', 'i', 'g' },
                { 'd', 'd', 'o', 'n' },
            };

            List<string> validWords = new List<string>
            {
                "flew",
                "believe",
                "kind",
                "care",
                "random",
                "kid",
                "achieve",
                "kidding"
            };
        */
        public static List<string> SolveBoggle(char[,] board, List<string> validWords)
        {
            Node root = BuildTrie(validWords);
            List<string> found = new List<string>();
            HashSet<string> visited = new HashSet<string>();

            for (int r = 0; r < board.Length; r++)
            {
                for (int c = 0; c < board.GetLength(0); c++)
                {
                    dfsExplore(r, c, board, root, found, "", visited);
                }
            }

            return found;
        }

        private static void dfsExplore(int r, int c, char[,] board, Node node, List<string> found, string word, HashSet<string> visited)
        {
            if (OOB(r, c, board) || !node.Children.ContainsKey(board[r, c]) || visited.Contains($"{r},{c}")) return;

            word += board[r, c];
            visited.Add($"{r},{c}");
            node = node.Children[board[r, c]];

            if (node.IsWord) found.Add(word);

            dfsExplore(r + 1, c, board, node, found, word, visited);
            dfsExplore(r - 1, c, board, node, found, word, visited);
            dfsExplore(r, c + 1, board, node, found, word, visited);
            dfsExplore(r, c - 1, board, node, found, word, visited);
            dfsExplore(r + 1, c + 1, board, node, found, word, visited);
            dfsExplore(r - 1, c - 1, board, node, found, word, visited);
            dfsExplore(r + 1, c - 1, board, node, found, word, visited);
            dfsExplore(r - 1, c + 1, board, node, found, word, visited);

            visited.Remove($"{r},{c}");
        }

        // https://en.wikipedia.org/wiki/Trie
        private static Node BuildTrie(List<string> validWords)
        {
            Node root = new Node('*');
            Node current = root;

            foreach (var word in validWords)
            {
                foreach (var ch in word)
                {
                    if (!current.Children.ContainsKey(ch))
                    {
                        current.Children.Add(ch, new Node(ch));
                    }

                    current = current.Children[ch];
                }

                current.IsWord = true;
                current = root;
            }

            return root;
        }

        class Node
        {
            public char Value { get; set; }
            public bool IsWord { get; set; }
            public Dictionary<char, Node> Children { get; set; }

            public Node(char value)
            {
                Value = value;
                Children = new Dictionary<char, Node>();
            }
        }

        private static bool OOB(int r, int c, char[,] board)
        {
            int x = board.GetLength(1) - 1;
            int y = board.GetLength(0) - 1;

            return !(0 <= r && r <= y &&
                     0 <= c && c <= x);
        }

        /*
             scenario: a sorting problem
            challenge: given an array A and an integer K,
                       sort A into 3 segments of A[i] < k, == k and > k
                       in o(n) space and better than o(n log n) time

             input: an array A
                    an integer K

            output: sort A into segements based on K

            note:
             - the order within each segment does not matter.
             - there will be at least 1 and at most 3 segments.
             - this could be solved by simply sorting A, achieving o(n log n)

            Examples
                     input: K = 3
                            A = 4, 5, 7, 3, 2
                    output:     2, 3, 4, 7, 5

                     input: K = 4
                            A = 8, 2, 1, 6, 9, 7, 200, -4,  5
                    output:    -4, 2, 1, 8, 6, 9,   7, 200, 5        
        */
        public static void SegmentSort(List<int> list, int k)
        {
            int lti; // less than index
            int eqi; // equal to index
            int gti; // greater than index

            // counts
            int lt = 0;
            int eq = 0;
            int gt = 0; // not needed

            int temp;
            int curIdx = 0;

            list.ForEach(x =>
            {
                if (x < k) lt++;
                else if (x == k) eq++;
                else gt++;
            });

            lti = 0;
            eqi = lt;
            gti = lt + eq;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[curIdx] < k)
                {
                    // swap
                    temp = list[lti];
                    list[lti] = list[curIdx];
                    list[curIdx] = temp;
                    lti++;
                }
                else if (list[curIdx] == k)
                {
                    temp = list[eqi];
                    list[eqi] = list[curIdx];
                    list[curIdx] = temp;
                    eqi++;
                }
                else
                {
                    temp = list[gti];
                    list[gti] = list[curIdx];
                    list[curIdx] = temp;
                    gti++;
                }

                // set next curIdx, to compare and position
                if (lti < lt) curIdx = lti;
                else if (eqi < lt + eq) curIdx = eqi;
                else curIdx = gti;
            }
        }
    }
}
