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
            int count = data[node - 1];

            if (adjlist.ContainsKey(node))
            {
                foreach (var adjnode in adjlist[node])
                {
                    if (!visited.Contains(adjnode))
                    {
                        visited.Add(adjnode);
                        count += dfsSumSubtree(adjlist, visited, data, adjnode, ref min, ref sum);
                    }
                }
            }

            min = Math.Min(Math.Abs(count - (sum - count)), min);

            return count;
        }
        //===================================================================================
    }
}
