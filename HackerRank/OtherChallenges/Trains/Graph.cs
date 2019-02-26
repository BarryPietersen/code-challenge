using System;
using System.Collections.Generic;

namespace TrainNetwork
{
    /*
        Approach:
            represent the one way tracks in Kiwiland using an adjacency list data structure,
            perform a variety of queries on the adjacency list using iterative and recursive
            graph traversal techniques such as bfs, dfs
    */

    /// <summary>
    /// Represents a train network in Kiwiland, provides the functionality to perform a variety of queries against the network
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// An adjacency list representing the conceptual graph, the storage data structure: space o(v + e)
        /// </summary>
        public Dictionary<char, Dictionary<char, int>> AdjacencyList { get; }

        /// <summary>
        /// Create a new instance of a Graph with an initialized adjacency list
        /// </summary>
        /// <param name="adjacencyList">The adjacency list queried by members of the Graph</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Graph(Dictionary<char, Dictionary<char, int>> adjacencyList)
        {
            this.AdjacencyList = adjacencyList ?? throw new ArgumentNullException(nameof(adjacencyList));
        }

        /// <summary>
        /// Calculate the distance of the 'strictly consecutive' route between all stations in the array,
        /// space o(1)
        /// time  o(n)
        /// </summary>
        /// <param name="stations">An array of characters indicating the 'strict' path to travel</param>
        /// <returns>An integer denoting the total distance traveled or -1 if no such route exists</returns>
        public int CalculateRouteDistance(char[] stations)
        {
            if (stations == null || stations.Length < 1 || !AdjacencyList.ContainsKey(stations[0])) return -1;

            int distance = 0;

            for (int i = 0; i < stations.Length - 1; i++)
            {
                if (AdjacencyList[stations[i]].ContainsKey(stations[i + 1]))
                {
                    distance += AdjacencyList[stations[i]][stations[i + 1]];
                }
                else
                    return -1;
            }

            return distance;
        }

        /// <summary>
        /// Calculate the length of the shortest route (in terms of distance to travel) from source to destination,
        /// space o(v + e)
        /// time  o(v + e)
        /// </summary>
        /// <param name="source">The entry point to the graph</param>
        /// <param name="destination">The end point, destination node</param>
        /// <returns>An integer denoting the shortest distance between source and destination, or -1 if no such route exists</returns>
        public int CalculateShortestRoute(char source, char destination)
        {
            if (!AdjacencyList.ContainsKey(source) || !AdjacencyList.ContainsKey(destination)) return -1;

            int dist;
            char current;
            Queue<char> q = new Queue<char>();
            Dictionary<char, int> distances = new Dictionary<char, int>();

            q.Enqueue(source);

            while (q.Count > 0)
            {
                current = q.Dequeue();

                foreach (var adjnode in AdjacencyList[current])
                {
                    dist = (distances.ContainsKey(current) ? distances[current] : 0) + adjnode.Value;

                    if (!distances.ContainsKey(adjnode.Key))
                    {
                        q.Enqueue(adjnode.Key);
                        distances.Add(adjnode.Key, dist);
                    }
                    else if (distances[adjnode.Key] > dist)
                    {
                        // we have just discovered a new shortest path,
                        // enqueue adjacent and explore it once again
                        // with the new shorter value
                        q.Enqueue(adjnode.Key);
                        distances[adjnode.Key] = dist;
                    }
                }
            }

            // check if there was a route found between source and destination
            return distances.ContainsKey(destination) ? distances[destination] : -1;
        }

        /// <summary>
        /// Count the number of trips from source to destination within the limit indicated by the 'stops' parameter
        /// </summary>
        /// <param name="source">The entry point to the graph</param>
        /// <param name="destination">The end point, destination node</param>
        /// <param name="stops">An integer denoting the maximum number of stops</param>
        /// <returns>An integer denoting the number of counted trips from source to destination, or 0 if no such route exists</returns>
        public int CountDistinctRoutesWithinMaxStops(char source, char destination, int stops)
        {
            if (stops < 2 || !AdjacencyList.ContainsKey(source) || !AdjacencyList.ContainsKey(destination)) return 0;

            return (source == destination ? -1 : 0) + dfsMaxStops(source, destination, stops);
        }

        private int dfsMaxStops(char source, char destination, int depth)
        {
            if (depth == -1) return 0;

            int count = (source == destination) ? 1 : 0;

            foreach (var adjnode in AdjacencyList[source]) count += dfsMaxStops(adjnode.Key, destination, depth - 1);

            return count;
        }

        /// <summary>
        /// Count the number of trips from source to destination with exactly number of stops indicated by the 'stops' parameter.
        /// </summary>
        /// <param name="source">The entry point to the graph</param>
        /// <param name="destination">The end point, destination node</param>
        /// <param name="stops">An integer denoting the exact number of stops accumulated during a trip</param>
        /// <returns>An integer denoting the number of counted trips from source to destination, or 0 if no such route exists</returns>
        public int CountDistinctRoutesWithExactStops(char source, char destination, int stops)
        {
            if (stops < 2 || !AdjacencyList.ContainsKey(source) || !AdjacencyList.ContainsKey(destination)) return 0;

            int count = 0;

            foreach (var adjnode in AdjacencyList[source]) count += dfsExactStops(adjnode.Key, destination, 1, stops);

            return count;
        }

        private int dfsExactStops(char source, char destination, int depth, int stop)
        {
            if (depth == stop) return source == destination ? 1 : 0;

            int count = 0;

            foreach (var adjnode in AdjacencyList[source]) count += dfsExactStops(adjnode.Key, destination, depth + 1, stop);

            return count;
        }

        /// <summary>
        /// Count the number of trips from source to destination with a distance strictly 'LESS' than the 'distance' parameter
        /// </summary>
        /// <param name="source">The entry point to the graph</param>
        /// <param name="destination">The end point, destination node</param>
        /// <param name="distance">An integer denoting the maximum distance (weights) of a trip</param>
        /// <returns>An integer denoting the number of counted trips from source to destination, or 0 if no such route exists</returns>
        public int CountDistinctRoutesWithinMaxDistance(char source, char destination, int distance)
        {
            if (distance < 1 || !AdjacencyList.ContainsKey(source) || !AdjacencyList.ContainsKey(destination)) return 0;

            int count = 0;

            foreach (var adjnode in AdjacencyList[source]) count += dfsMaxDistance(adjnode.Key, destination, distance - adjnode.Value);

            return count;
        }

        private int dfsMaxDistance(char source, char destination, int distance)
        {
            if (distance <= 0) return 0;

            int count = (source == destination) ? 1 : 0;

            foreach (var adjnode in AdjacencyList[source]) count += dfsMaxDistance(adjnode.Key, destination, distance - adjnode.Value);

            return count;
        }
    }
}
