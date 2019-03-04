using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Algorithms.Search
{
    public static class Medium
    {
        // https://www.hackerrank.com/challenges/pairs/problem
        static int pairs(int k, int[] arr)
        {
            Array.Sort(arr);
            int pairs = 0;
            int diff;

            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    diff = arr[j] - arr[i];

                    if (diff == k) pairs++;
                    else if (diff > k) j = arr.Length;
                }
            }

            return pairs;
        }

        /*
            connected cell test case = 29

            0 1 0 0 0 0 1 1 0
            1 1 0 0 1 0 0 0 1
            0 0 0 0 1 0 1 0 0
            0 1 1 1 0 1 0 1 1
            0 1 1 1 0 0 1 1 0
            0 1 0 1 1 0 1 1 0
            0 1 0 0 1 1 0 1 1
            1 0 1 1 1 1 0 0 0  
        */
        // https://www.hackerrank.com/challenges/connected-cell-in-a-grid/problem
        //===================================================================================
        static int connectedCell(int[][] matrix)
        {
            int max = 0;

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    if (matrix[i][j] == 1)
                    {
                        int cellCount = countCells(matrix, i, j);
                        if (cellCount > max) max = cellCount;
                    }
                }
            }

            return max;
        }

        private static int countCells(int[][] matrix, int y, int x)
        {
            if (y < matrix.Length && y >= 0 &&
                x < matrix[0].Length && x >= 0 &&
                matrix[y][x] == 1)
            {
                matrix[y][x] = 0;

                return 1 +
                countCells(matrix, y - 1, x) +
                countCells(matrix, y, x + 1) +
                countCells(matrix, y + 1, x) +
                countCells(matrix, y, x - 1) +
                countCells(matrix, y - 1, x - 1) +
                countCells(matrix, y - 1, x + 1) +
                countCells(matrix, y + 1, x + 1) +
                countCells(matrix, y + 1, x - 1);
            }
            return 0;
        }
        //===================================================================================

        // https://www.hackerrank.com/challenges/gridland-metro/problem
        static long gridlandMetro(long n, long m, long k, long[][] arr)
        {
            long cells = n * m;

            Dictionary<long, List<long[]>> tracks = new Dictionary<long, List<long[]>>();

            foreach (long[] t in arr)
            {
                if (tracks.ContainsKey(t[0])) tracks[t[0]].Add(new long[] { t[1], t[2] });
                else
                    tracks.Add(t[0], new List<long[]>() { new long[] { t[1], t[2] } });
            }

            foreach (var kvp in tracks)
            {
                // there is only one track on the row
                if (kvp.Value.Count == 1) cells -= kvp.Value[0][1] - kvp.Value[0][0] + 1;

                // there are multiple tracks on the row
                // -we need to determine if overlaps exist
                else
                {
                    IOrderedEnumerable<long[]> row = kvp.Value.OrderBy(a => a[0]);
                    Stack<long[]> mergedTracks = new Stack<long[]>();

                    mergedTracks.Push(row.First());

                    foreach (var track in row)
                    {
                        if (mergedTracks.Peek()[1] >= track[0])
                        {
                            if (mergedTracks.Peek()[1] < track[1])
                            {
                                mergedTracks.Peek()[1] = track[1];
                            }
                        }
                        else
                            mergedTracks.Push(track);
                    }

                    foreach (var track in mergedTracks) cells -= track[1] - track[0] + 1;
                }
            }

            return cells;
        }

        /*
            approach:
                - sum and store the input data list 'node values'
                - use an adjacency list to represent an undirected graph
                - using dfs traversal to compute the sums of all subtrees in o(n)
                  by returning the sum of child to parent while keeping track of visited nodes
                  aswell as checking for the minimal difference condition

            space: o(v + e)
             time: o(n)
        */
        // https://www.hackerrank.com/challenges/cut-the-tree/problem
        //===================================================================================
        public static int cutTheTree(List<int> data, List<List<int>> edges)
        {
            int sum = data.Sum();
            int min = int.MaxValue;

            HashSet<int> visited = new HashSet<int>();
            Dictionary<int, HashSet<int>> adjlist = new Dictionary<int, HashSet<int>>();

            // populate the adjacency list for an undirected graph
            for (int i = 0; i < edges.Count; i++)
            {
                if (!adjlist.ContainsKey(edges[i][0]))
                {
                    adjlist.Add(edges[i][0], new HashSet<int>());
                    adjlist[edges[i][0]].Add(edges[i][1]);
                }
                else
                    adjlist[edges[i][0]].Add(edges[i][1]);

                if (!adjlist.ContainsKey(edges[i][1]))
                {
                    adjlist.Add(edges[i][1], new HashSet<int>());
                    adjlist[edges[i][1]].Add(edges[i][0]);
                }
                else
                    adjlist[edges[i][1]].Add(edges[i][0]);
            }

            // call recursive dfs algorithm, keeping track of the current 'minimal diff subtree'
            dfsSumSubtree(adjlist, visited, data, 1, ref min, ref sum);

            return min;
        }

        private static int dfsSumSubtree(Dictionary<int, HashSet<int>> adjlist, HashSet<int> visited, List<int> data, int node, ref int min, ref int sum)
        {
            visited.Add(node);
            int count = data[node - 1];

            foreach (var adjnode in adjlist[node])
            {
                if (!visited.Contains(adjnode))
                {
                    visited.Add(adjnode);
                    count += dfsSumSubtree(adjlist, visited, data, adjnode, ref min, ref sum);
                }
            }

            min = Math.Min(Math.Abs(count - (sum - count)), min);

            return count;
        }
        //===================================================================================

        // https://www.hackerrank.com/challenges/count-luck/problem
        public static string countLuck(string[] matrix, int k)
        {
            Point root = FindEntryPoint(matrix, 'M');

            HashSet<string> visited = new HashSet<string>() { root.ToString() };

            return dfsCountLuck(matrix, visited, root, 0) == k ? "Impressed": "Oops!";          
        }

        private static int dfsCountLuck(string[] matrix, HashSet<string> visited, Point point, int decisions)
        {
            if (matrix[point.Y][point.X] == '*') return decisions;

            visited.Add(point.ToString());

            // left right up down
            Point l = new Point(point.X - 1, point.Y);
            Point r = new Point(point.X + 1, point.Y);
            Point u = new Point(point.X, point.Y - 1);
            Point d = new Point(point.X, point.Y + 1);

            List<Point> children = new List<Point>();

            if (!OutOfBounds(matrix, l) && !visited.Contains(l.ToString())) children.Add(l);
            if (!OutOfBounds(matrix, r) && !visited.Contains(r.ToString())) children.Add(r);
            if (!OutOfBounds(matrix, u) && !visited.Contains(u.ToString())) children.Add(u);
            if (!OutOfBounds(matrix, d) && !visited.Contains(d.ToString())) children.Add(d);

            if (children.Count > 1) decisions++;

            foreach (var child in children)
            {
                int temp = dfsCountLuck(matrix, visited, child, decisions);
                if (temp > 0) return temp; //stop searching, we found the path
            }

            return 0;
        }

        private static bool OutOfBounds(string[] matrix, Point point)
        {
            return (point.X < 0 || point.X >= matrix[0].Length ||
                    point.Y < 0 || point.Y >= matrix.Length || matrix[point.Y][point.X] == 'X');
        }

        private static Point FindEntryPoint(string[] matrix, char token)
        {
            for (int y = 0; y < matrix.Length; y++)
            {
                for (int x = 0; x < matrix[0].Length; x++)
                {
                    if (matrix[y][x] == token)
                    {
                        return new Point(x, y);
                    }
                }
            }

            throw new Exception($"the entry point marked '{token}' was not found in {nameof(matrix)}");
        }

        private class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override string ToString() => $"{X} {Y}";
        }
    }
}
