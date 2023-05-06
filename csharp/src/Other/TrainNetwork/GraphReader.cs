using System;
using System.Collections.Generic;
using System.IO;

namespace Other.TrainNetwork
{
    public class GraphReader
    {
        /// <summary>
        /// Opens a file stream and reads values into the Graph
        /// </summary>
        /// <param name="path">The file path to the text file</param>
        /// <returns>A new Graph with the vaules read from the file</returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static Graph ReadFromTextFile(string path)
        {
            string edge;
            int weight;
            char source;
            char desination;

            var adjlist = new Dictionary<char, Dictionary<char, int>>();

            using (var sw = new StreamReader(path))
            {
                while (!sw.EndOfStream)
                {
                    edge = sw.ReadLine()!;

                    source = edge[0];
                    desination = edge[1];
                    weight = Convert.ToInt32(edge.Substring(2));

                    // add a new edge to the network with the weight
                    if (adjlist.TryGetValue(source, out var value))
                    {
                        value.Add(desination, weight);
                    }
                    // or add a new station to the adjacency list with a new edge and weight
                    else
                    {
                        adjlist.Add(source, new Dictionary<char, int>()
                        {
                            { desination, weight }
                        });
                    }
                }
            }

            return new Graph(adjlist);
        }
    }
}
