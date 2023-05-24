using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Other.Algorithms
{
    public static class Random
    {
        // codewars https://www.codewars.com/kata/dubstep/train/csharp
        public static string SongDecoder(string input)
        {
            return string.Join(" ", new Regex("WUB")
                         .Replace(input, " ")
                         .Split(' ')
                         .Where(s => !string.IsNullOrEmpty(s)));
        }

        // codewars https://www.codewars.com/kata/54e6533c92449cc251001667/train/csharp
        public static IEnumerable<T> UniqueInOrder<T>(IEnumerable<T> iterable)
        {
            var result = new List<T>();

            if (!iterable.Any()) 
            { 
                return result;
            }

            result.Add(iterable.First());

            foreach (var item in iterable)
            {
                if (!result.Last()!.Equals(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        /*
            calculate the overlapping area of two rectangles on a 2d plane

            a white board interview question sometimes asked by large tech companies.
            https://www.youtube.com/watch?v=zGv3hOORxh0

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

            var mid = target / 2;
            var pow = Math.Pow(mid, n);

            while (Math.Abs(target - pow) > epsilon)
            {
                if (pow > target) { mid -= mid / 2; }
                else { mid += mid / 2; }

                pow = Math.Pow(mid, n);
            }

            return mid;
        }
        //================================================================================

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
        //================================================================================
        public static List<string> SolveBoggle(char[,] board, List<string> validWords)
        {
            var root = BuildTrie(validWords);
            var found = new List<string>();
            var visited = new HashSet<string>();

            for (var r = 0; r < board.Length; r++)
            {
                for (var c = 0; c < board.GetLength(0); c++)
                {
                    DfsExplore(r, c, board, root, found, "", visited);
                }
            }

            return found;
        }

        private static void DfsExplore(int r, int c, char[,] board, Node node, List<string> found, string word, HashSet<string> visited)
        {
            if (OOB(r, c, board) || !node.Children.ContainsKey(board[r, c]) || visited.Contains($"{r},{c}")) return;

            word += board[r, c];
            visited.Add($"{r},{c}");
            node = node.Children[board[r, c]];

            if (node.IsWord) found.Add(word);

            DfsExplore(r + 1, c, board, node, found, word, visited);
            DfsExplore(r - 1, c, board, node, found, word, visited);
            DfsExplore(r, c + 1, board, node, found, word, visited);
            DfsExplore(r, c - 1, board, node, found, word, visited);
            DfsExplore(r + 1, c + 1, board, node, found, word, visited);
            DfsExplore(r - 1, c - 1, board, node, found, word, visited);
            DfsExplore(r + 1, c - 1, board, node, found, word, visited);
            DfsExplore(r - 1, c + 1, board, node, found, word, visited);

            visited.Remove($"{r},{c}");
        }

        // https://en.wikipedia.org/wiki/Trie
        private static Node BuildTrie(List<string> validWords)
        {
            var root = new Node('*');
            var current = root;

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
            var x = board.GetLength(1) - 1;
            var y = board.GetLength(0) - 1;

            return !(0 <= r && r <= y &&
                     0 <= c && c <= x);
        }
        //================================================================================

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

            var bonacci = new int[m];
            bonacci[1] = 1;
            var head = 0;

            var i = 2;
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

            var mid = arr.Length / 2;

            var left = new int[mid];
            var rght = new int[mid + arr.Length % 2];

            Array.Copy(arr, 0, left, 0, mid);
            Array.Copy(arr, mid, rght, 0, mid + arr.Length % 2);

            return merge(MergeSort(left), MergeSort(rght));
        }

        private static int[] merge(int[] left, int[] rght)
        {
            int l = 0, r = 0;
            var res = new int[left.Length + rght.Length];

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
        public static bool MatchStringNoPunctuation(string a, string b)
        {
            var aI = 0;
            var bI = 0;

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
            var primes = new HashSet<double>();
            var len = num / 2;

            for (var i = 2; i <= len; i++)
                if (_isPrime(i)) primes.Add(i);

            foreach (var p in primes)
            {
                if (primes.Contains(num / p))
                    return true;
            }

            return false;
        }

        private static bool _isPrime(int num)
        {
            var root = Math.Sqrt(num);

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
            var sb = new StringBuilder(a.Length + b.Length);

            if (b.Length > a.Length)
            {
                var temp = b;
                b = a;
                a = temp;
            }

            var ai = 0;
            var bi = 0;

            while (bi < b.Length)
            {
                sb.Append(a[ai++]);
                sb.Append(b[bi++]);
            }

            sb.Append(a.AsSpan(ai));

            return sb.ToString();
        }

        // fibonacci using recursive, memoization techniques
        //================================================================================
        public static int NthFibRecursive(int n)
        {
            var found = new Dictionary<int, int>();
            return fib(n, found);
        }

        private static int fib(int n, Dictionary<int, int> found)
        {
            if (found.TryGetValue(n, out var value)) return value;
            if (n <= 2) return 1;

            found.Add(n, fib(n - 1, found) +
                         fib(n - 2, found));

            return found[n];
        }

        // an iterative solution for finding the nth fib value
        public static int NthFibIterative(int n)
        {
            int temp;
            var l = 0;
            var r = 1;

            for (var i = 1; i < n; i++)
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

            foreach (var ch in password)
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
            var lft = 0;
            var rht = sorted.Length - 1;

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
            var l = 0;
            var r = sorted.Length - 1;

            return BinarySearchRecursive(sorted, n, l, r);
        }

        private static bool BinarySearchRecursive(int[] sorted, int n, int l, int r)
        {
            if (l > r) return false;

            var m = (l + r) / 2;

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

            var n = 1;
            var level = 1;
            var sb = new StringBuilder();

            while (level <= height)
            {
                for (var i = 1; i < level; i++)
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
            var tl = 0;
            var br = matrix.GetLength(0) - 1;
            var m = matrix.GetLength(0) / 2;
            int temp1, temp2;

            while (m > 0)
            {
                for (var i = 0; i < br - tl; i++)
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
            var count = 0;
            long complement;
            var pairs = new HashSet<string>();
            var complements = new HashSet<long>();

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
            var groups = 0;
            var visited = new HashSet<int>();
            var adjlist = new Dictionary<int, HashSet<int>>();

            // populate our adjacency list o(v + e)
            for (var i = 0; i < forest.Length; i++)
            {
                if (!adjlist.ContainsKey(i))
                {
                    adjlist.Add(i, new HashSet<int>());
                }

                for (var j = 1; j < forest.Length; j++)
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

        /*
            Coderbyte:
            Have the method read str which will be an arithmetic expression composed of only integers and the operators: +,-,* and / 
            the input expression will be in postfix notation (Reverse Polish notation), example: (1 + 2) * 3 would be 1 2 + 3 * in postfix notation. 
            The method should determine the answer for the given postfix expression. 
            For example: if str is "2 12 + 7 /" then the method should return 2.
        */
        public static string ReversePolishNotation(string str)
        {
            /*
                Input: "1 1 + 1 + 1 +"
                Output: 4
            
                Input: "4 5 + 2 1 + *"
                Output: 27
             
                Input: "2 3 - 4 + 5 6 7 * + *"   ==   (2 - 3 + 4) * (5 + 6 * 7)
                Output: 141
            */

            var inputs = str.Split(' ');
            var operands = new Stack<int>();

            foreach (var input in inputs)
            {
                if (int.TryParse(input, out var operand))
                {
                    operands.Push(operand);
                }
                else
                {
                    var right = operands.Pop();
                    var left = operands.Pop();
                    var next = ComputeValue(left, right, input.First());

                    operands.Push(next);
                }
            }

            return operands.Pop().ToString();
        }

        private static int ComputeValue(int left, int right, char @operator)
        {
            return @operator switch
            {
                '+' => left + right,
                '-' => left - right,
                '*' => left * right,
                '/' => left / right,
                _ => throw new ArgumentException($"unsupported arithmetic operator {@operator}"),
            };
        }

        /*
              4     8
             /       \
            2         10
           / \
          1   7
              /
             5
              \
               9
        */
        // Coderbyte: determine if the input strArr is a single valid binary tree.
        // input format: an array of strings "(i1,i2)" where i1 is a child node and i2 is its parent. values are integer
        // sampele input: [ "(4,5)", "(3,5)", "(1,3)", "(5,10)" ] 
        // runtime: o(n)
        // space: o(nlogn)
        public static string TreeConstructor(string[] strArr)
        {
            // parse input
            var edges = strArr.Select(s =>
                s.Split(",")
                 .Select(s => int.Parse(s.Trim('(', ')')))
                 .ToList())
                 .ToList();

            var allChildNodes = new HashSet<int>();
            var potentialRootNodes = new HashSet<int>();
            var nodeChildrenCount = new Dictionary<int, int>();

            foreach (var edge in edges)
            {
                var child = edge.First();
                var parent = edge.Last();

                allChildNodes.Add(child);
                potentialRootNodes.Remove(child);

                if (!nodeChildrenCount.TryGetValue(parent, out var childrenCount))
                {
                    nodeChildrenCount.Add(parent, 1);

                    if (!allChildNodes.Contains(parent))
                    {
                        potentialRootNodes.Add(parent);
                    }
                }
                else if (childrenCount == 1)
                {
                    nodeChildrenCount[parent]++;
                }
                else
                {
                    // node has more than 2 children
                    return "false";
                }
            }

            // if there is more than 1 root node present at this point,
            // then strArr contains multiple trees and is not proper
            return potentialRootNodes.Count == 1 ? "true" : "false";
        }

        // https://leetcode.com/problems/generate-parentheses
        public static List<string> GenerateParenthesis(int n) 
        {
            var len = n * 2;
            var results = new List<string>();
            var q = new Queue<QItem>();

            q.Enqueue(new QItem("(", 1, 0));

            while (q.TryDequeue(out var current))
            {
                if (current.Value.Length == len && 
                    current.OpenCount == current.ClosedCount)
                {
                    results.Add(current.Value);
                }
                else if (current.Value.Length < len)
                {
                    q.Enqueue(current with { Value = $"{current.Value}(", OpenCount = current.OpenCount + 1 });

                    if (current.OpenCount > current.ClosedCount)
                    {
                        q.Enqueue(current with { Value = $"{current.Value})", ClosedCount = current.ClosedCount + 1 });
                    }
                }
            }

            return results;
        }

        private record QItem(
            string Value, 
            int OpenCount, 
            int ClosedCount);

        // https://leetcode.com/problems/zigzag-conversion
        public static string ZigZag(string s, int numRows) 
        {
            // CODINGCHALLENGESAREFUN = 22
            // CCNEOGHEGRFDNALEAUILSN
            // 4

            /*
                C     C     N     E
                O   G H   E G   R F   S
                D N   A L   E A   U S
                I     L     S     N


                C   N   A   N   A   U
                O I G H L E G S R F N
                D   C   L   E   E

                CNANAUOIGHLEGSRFNDCLEE

                CDNCALNEAEU
                OIGHLEGSRFN

                CCNEOGHEGRFDNALEAUILSN
            */

            var diag = numRows <= 2 ? 0 : numRows - 2;
            var blockLen = numRows + diag;
            var remainder = s.Length % blockLen;
            var numCols =
                (s.Length / blockLen * (diag + 1)) + 
                (remainder > 0 ? Math.Max(remainder - numRows, 0) + 1: 0);

            var matrix = new string[numRows, numCols];

            var idx = 0;
            var col = 0;
            var diag_r = Math.Max(numRows - 2, 0);
            while (idx < s.Length)
            {
                for (var r = 0; r < numRows && idx < s.Length; r++)
                {
                    matrix[r, col] = s[idx++].ToString();
                }

                for (var r = diag_r; r > 0 && idx < s.Length; r--)
                {
                    matrix[r, ++col] = s[idx++].ToString();
                }

                col++;
            }

            var result = "";

            foreach (var item in matrix)
            {
                result += item;
            }

            return result;
        }

        // https://leetcode.com/problems/top-k-frequent-elements/
        public static int[] TopKFrequent(int[] nums, int k)
        {
            var result = new int[k];
            var tail = default(FrequencyNode);
            var head = new FrequencyNode { Value = nums[0], Frequency = 1 };
            var nodeLookup = new Dictionary<int, FrequencyNode>() { { nums[0], head } };

            foreach (var num in nums.Skip(1))
            {
                if (nodeLookup.TryGetValue(num, out var node))
                {
                    var newTailCandidate = node.Greater;

                    node.Frequency++;
                    node.Sort();
                    head = node.Greater is null ? node : head;
                    tail = 
                        newTailCandidate is not null && 
                        newTailCandidate.Less is null ? newTailCandidate : tail;
                }
                else
                {
                    var nextTail = new FrequencyNode { Value = num, Frequency = 1, Greater = tail ?? head };
                    nextTail.Greater.Less = nextTail;       
                    tail = nextTail;
                    nodeLookup.Add(num, nextTail);
                }
            }

            var current = head;
            for (int i = 0; i < k && current is not null; i++)
            {
                result[i] = current.Value;
                current = current.Less;
            }

            return result;
        }

        /*
              4     8
             /       \
            2         10
           / \
          1   7
              /
             5
              \
               9
        */
        private sealed class FrequencyNode 
        {
            public int Value { get; set; }
            public int Frequency { get; set; }
            public FrequencyNode? Greater { get; set; }
            public FrequencyNode? Less { get; set; }

            public void Sort() 
            {
                if (Greater?.Frequency < Frequency)
                {
                    SwapUp(Greater);
                    Sort();
                }
            }

            private void SwapUp(FrequencyNode other)
            {
                var nextGreater = other.Greater;

                if (other.Greater is not null)
                {
                    other.Greater.Less = this;
                }

                other.Greater = this;
                other.Less = Less;

                if (other.Less is not null)
                {
                    other.Less.Greater = other;
                }
                
                Less = other;
                Greater = nextGreater;
            }
        }
    }
}
