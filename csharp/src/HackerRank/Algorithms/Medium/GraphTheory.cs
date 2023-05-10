using System.Collections.Generic;
using System.Linq;

namespace HackerRank.Algorithms.Medium
{
    public static class GraphTheory
    {
        // https://www.hackerrank.com/challenges/bfsshortreach/problem
        public static int[] bfs(int n, int m, int[][] edges, int s)
        {
            int current;
            var result = new int[n - 1];
            var q = new Queue<int>();
            var adjlist = new Dictionary<int, List<int>>();
            var distances = new Dictionary<int, int>(n) { { s, 0 } };

            // populate adjacency list o(n)
            foreach (var edge in edges)
            {
                if (adjlist.TryGetValue(edge[0], out var v1)) v1.Add(edge[1]);
                else
                    adjlist.Add(edge[0], new List<int> { edge[1] });

                if (adjlist.TryGetValue(edge[1], out var v2)) v2.Add(edge[0]);
                else
                    adjlist.Add(edge[1], new List<int> { edge[0] });
            }

            // edge case - make sure s (entry point)
            // is connected to any other nodes,
            // at this point enqueue s and
            // begin our bfs traversal
            if (adjlist.TryGetValue(s, out var _))
            {
                q.Enqueue(s);

                while (q.Count > 0)
                {
                    current = q.Dequeue();

                    foreach (var adjnode in adjlist[current])
                    {
                        if (!distances.ContainsKey(adjnode))
                        {
                            q.Enqueue(adjnode);
                            distances.Add(adjnode, distances[current] + 6);
                        }
                    }
                }
            }

            // finally we populate the result array from distances dictionary,
            // in order to efficiently satisfy the sorted order requirement:
            // -two for loops are used to print each side - and exclude [s]
            // -this is also to avoid checking for v == s at each iteration
            // v is the current node (vertex) we are iterating on
            int i = 0, v = 1;

            for (; v < s; i++, v++)
                result[i] = distances.TryGetValue(v, out int value) ? value : -1;

            for (v++; v <= n; i++, v++)
                result[i] = distances.TryGetValue(v, out int value) ? value : -1;

            return result;
        }

        // ========================================================================================
        // https://www.hackerrank.com/challenges/torque-and-development/problem
        public static long roadsAndLibraries(int n, int c_lib, int c_road, int[][] cities)
        {
            var cost = 0L;
            var totalnodes = 0L;
            var visited = new HashSet<int>();
            var adjlist = new Dictionary<int, List<int>>();

            // populate adjacency list o(n)
            foreach (var edge in cities)
            {
                if (adjlist.TryGetValue(edge[0], out var v1)) v1.Add(edge[1]);
                else
                    adjlist.Add(edge[0], new List<int> { edge[1] });

                if (adjlist.TryGetValue(edge[1], out var v2)) v2.Add(edge[0]);
                else
                    adjlist.Add(edge[1], new List<int> { edge[0] });
            }

            // traverse adjacency list using recursive dfs technique o(v + e)
            foreach (var kvp in adjlist)
            {
                if (!visited.Contains(kvp.Key))
                {
                    long nodecount = dfs(adjlist, visited, kvp.Key);
                    if (c_lib < c_road) cost += nodecount * c_lib;
                    else cost += c_lib + (nodecount - 1) * c_road;
                    totalnodes += nodecount;
                }
            }

            // if a node appears on its own (no adjacent nodes)
            // input (edge array) will not include it, this 
            // requires a single library placed in the city
            cost += (n - totalnodes) * c_lib;

            return cost;
        }

        private static long dfs(Dictionary<int, List<int>> adjlist, HashSet<int> visited, int node)
        {
            if (visited.Contains(node)) return 0;

            visited.Add(node);
            long count = 1;

            foreach (var adjnode in adjlist[node]) count += dfs(adjlist, visited, adjnode);

            return count;
        }

        // ========================================================================================
        // https://www.hackerrank.com/challenges/journey-to-the-moon/problem
        public static long journeyToMoon(int n, int[][] astronauts)
        {
            var pairs = 0L;      // total pairs constructed from different countries
            var astrocount = 0;  // stores the count of all discovered astronauts
            var runningsum = 0L; // used to speed the process of computing pairs
            var countries = new List<int>();
            var visited = new HashSet<int>();
            var adjlist = new Dictionary<int, List<int>>();

            // populate adjacency list o(n)
            foreach (var edge in astronauts)
            {
                if (adjlist.TryGetValue(edge[0], out var v1)) v1.Add(edge[1]);
                else
                    adjlist.Add(edge[0], new List<int> { edge[1] });

                if (adjlist.TryGetValue(edge[1], out var v2)) v2.Add(edge[0]);
                else
                    adjlist.Add(edge[1], new List<int> { edge[0] });
            }

            // traverse adjacency list using recursive dfs technique o(v + e)
            foreach (var kvp in adjlist)
            {
                if (!visited.Contains(kvp.Key))
                {
                    var count = (int)dfs(adjlist, visited, kvp.Key);
                    countries.Add(count);
                    astrocount += count;
                }
            }

            // in the case of disconnected nodes, count
            // these and add 1 to the list of countries
            n -= astrocount;

            for (var i = 0; i < n; i++) countries.Add(1);

            foreach (var population in countries)
            {
                pairs += runningsum * population;
                runningsum += population;
            }

            return pairs;
        }

        /*
            https://www.hackerrank.com/challenges/the-quickest-way-up/problem

            scenario - a game of snakes and ladders
            task     - find the lowest number of dice rolls to travel from square 1 - 100

            rules and constraints:
            -the board is always 10 x 10.
            -we always start at node 1 and finish at 100.
            -the game is played with a cubic die of 6 faces numbered 1 to 6.
            -if a player lands on a snake or ladder, the player must move immediately to the location.
            -snakes only go down, ladders only go up (guaranteed).
        */
        public static int quickestWayUp(int[][] ladders, int[][] snakes)
        {
            int d; // dice limit variable
            int parent; // limit variable
            var q = new Queue<int>();
            var adjlist = new Dictionary<int, List<int>>(100) { { 100, new List<int>() } };
            var distances = new Dictionary<int, int>(100) { { 1, 0 }, { 100, int.MaxValue } };

            // build an adjacency list for 
            // the directed graph (board)
            for (var i = 1; i < 100; i++)
            {
                d = i + 7; // dice limit

                adjlist.Add(i, new List<int>(6));

                // add neighbours of i to adjlist
                for (var j = i + 1; j < d && j <= 100; j++)
                {
                    adjlist[i].Add(j);
                }
            }

            foreach (var snake in snakes)
            {
                parent = snake[0] - 6;
                // set all parent nodes that point to the snake head
                // to instead point to the snake tail
                for (var i = snake[0] - 1; i >= parent && i > 0; i--)
                {
                    adjlist[i][snake[0] - i - 1] = snake[1];
                }
            }

            foreach (var ladder in ladders)
            {
                parent = ladder[0] - 6;
                // set all parent nodes that point to the ladder base
                // to instead point to the ladder top
                for (var i = ladder[0] - 1; i >= parent && i > 0; i--)
                {
                    adjlist[i][ladder[0] - i - 1] = ladder[1];
                }
            }

            int current;
            q.Enqueue(1);

            // bfs traverse,
            // update distances
            while (q.Count > 0)
            {
                current = q.Dequeue();

                foreach (var v in adjlist[current])
                {
                    // v - the neighbouring vertex
                    if (!distances.ContainsKey(v))
                    {
                        q.Enqueue(v);
                        distances.Add(v, distances[current] + 1);
                    }
                    else if (distances[v] > distances[current] + 1)
                    {
                        distances[v] = distances[current] + 1;
                    }
                }
            }

            return distances[100] < int.MaxValue ? distances[100] : -1;
        }

        // https://www.hackerrank.com/challenges/even-tree/problem
        // Note: The tree in the input will be such that it can always be
        // decomposed into components containing an even number of nodes
        public static int evenForest(int t_nodes, int t_edges, List<int> t_from, List<int> t_to)
        {
            var arbitraryEntryNode = t_from.First();
            var visited = new HashSet<int> { arbitraryEntryNode };
            var adjList = new AdjacencyList<int, int>();

            for (int i = 0; i < t_from.Count; i++)
            {
                adjList.Add(t_from[i], t_to[i]);
                adjList.Add(t_to[i], t_from[i]);
            }

            dfsEvenForest(
                arbitraryEntryNode, 
                adjList, 
                visited, 
                out var subForestCount);

            return subForestCount;
        }

        private static int dfsEvenForest(
            int current,
            AdjacencyList<int, int> adjList, 
            HashSet<int> visited,
            out int subForestCount) 
        {
            var weight = 0;
            var neighbours = adjList.GetNeighbours(current);

            subForestCount = 0;

            foreach (var neighbour in neighbours!.Where(n => !visited.Contains(n)))
            {
                visited.Add(neighbour);

                var neighbourWeight = dfsEvenForest(
                    neighbour, 
                    adjList, 
                    visited, 
                    out var sub);

                subForestCount += sub;
                weight += neighbourWeight;

                if (neighbourWeight % 2 == 0)
                {
                    subForestCount++;
                }
            }

            return weight + 1;
        }

        private class AdjacencyList<TKey, TValue> where TKey : notnull
        {
            private readonly Dictionary<TKey, HashSet<TValue>> dict;

            public AdjacencyList()
            {
                dict = new();
            }

            public AdjacencyList(int capacity)
            {
                dict = new(capacity);
            }

            public bool Add(TKey key)
            {
                if (dict.ContainsKey(key))
                {
                    return false;
                }

                dict.Add(key, new());

                return true;
            }

            public bool Add(TKey key, TValue value)
            {
                if (dict.TryGetValue(key, out var set))
                {
                    return set.Add(value);
                }

                dict.Add(key, new() { value });

                return true;
            }

            public IReadOnlySet<TValue> GetNeighbours(TKey key)
            {
                return dict[key];
            }

            public bool TryGetNeighbours(TKey key, out IReadOnlySet<TValue>? neighbours) 
            {
                if (dict.TryGetValue(key, out var value))
                {
                    neighbours = value;
                    return true;
                }

                neighbours = default;
                return false;
            }
        }
    }
}
