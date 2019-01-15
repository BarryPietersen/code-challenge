using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Algorithms.GraphTheory
{
    public static class Hard
    {
        /*
            https://www.hackerrank.com/challenges/dijkstrashortreach/problem

            used Dictionary<int, Dictionary<int, int>> to store the adjacency list. 
            this helps with the duplicate edges appearing in the input matrix.
            using this we can easily manage our adjacency list to only ever
            store one edge between verticies (which has the min weight)

            in order to pass test case 7 using C#:
            - optimise the IO operations as much as possible
            - make changes to hackerranks boilerplate input codes
            - use custom int parser, test 7 timing out befor input reads finish

        */
        public static int[] shortestReach(int n, int[][] edges, int s)
        {
            int[] result = new int[n - 1];
            Queue<int> q = new Queue<int>();
            Dictionary<int, int> distances = new Dictionary<int, int>(n) { { s, 0 } };
            Dictionary<int, Dictionary<int, int>> adjlist = new Dictionary<int, Dictionary<int, int>>();

            // populate the adjacency list,
            // avoid storing duplicate edges
            // between verticies
            foreach (var edge in edges)
            {
                if (adjlist.ContainsKey(edge[0]))
                {
                    if (!adjlist[edge[0]].ContainsKey(edge[1]))
                    {
                        adjlist[edge[0]].Add(edge[1], edge[2]);
                    }
                    else if (adjlist[edge[0]][edge[1]] > edge[2])
                    {
                        adjlist[edge[0]][edge[1]] = edge[2];
                    }
                }
                else
                    adjlist.Add(edge[0], new Dictionary<int, int> { { edge[1], edge[2] } });

                if (adjlist.ContainsKey(edge[1]))
                {
                    if (!adjlist[edge[1]].ContainsKey(edge[0]))
                    {
                        adjlist[edge[1]].Add(edge[0], edge[2]);
                    }
                    else if (adjlist[edge[1]][edge[0]] > edge[2])
                    {
                        adjlist[edge[1]][edge[0]] = edge[2];
                    }
                }
                else
                    adjlist.Add(edge[1], new Dictionary<int, int> { { edge[0], edge[2] } });
            }

            // edge case - make sure s (entry point)
            // is connected to any other nodes,
            // at this point enqueue s and
            // begin our bfs traversal
            if (adjlist.ContainsKey(s))
            {
                int dist;
                int current;
                q.Enqueue(s);

                while (q.Count > 0)
                {
                    current = q.Dequeue();
                    foreach (var adjnode in adjlist[current])
                    {
                        dist = distances[current] + adjnode.Value;

                        if (!distances.ContainsKey(adjnode.Key))
                        {
                            q.Enqueue(adjnode.Key);
                            distances.Add(adjnode.Key, dist);
                        }
                        else if (distances[adjnode.Key] > dist)
                        {
                            // we have just discovered a new shortest path
                            // enqueue adjacent and explore it once again
                            // with the new shorter value
                            q.Enqueue(adjnode.Key);
                            distances[adjnode.Key] = dist;
                        }
                    }
                }
            }

            int i = 0, v = 1;

            for (; v < s; i++, v++)
                result[i] = distances.ContainsKey(v) ? distances[v] : -1;

            for (v++; v <= n; i++, v++)
                result[i] = distances.ContainsKey(v) ? distances[v] : -1;

            return result;
        }

        // the non negative int parser,
        // only way to pass test case 7
        // using c# in dijkstrashortreach problem
        public static int intParser(string s)
        {
            int y = 0;
            for (int i = 0; i < s.Length; i++)
            {
                y = y * 10 + (s[i] - '0');
            }
            return y;
        }
    }
}
