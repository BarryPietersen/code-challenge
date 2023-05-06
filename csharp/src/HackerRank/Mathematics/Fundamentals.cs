using System;

namespace HackerRank.Mathematics
{
    public static class Fundamentals
    {
        // https://www.hackerrank.com/challenges/sherlock-and-moving-tiles/problem
        public static double[] movingTiles(int l, int s1, int s2, double[] queries)
        {
            // velocity - dist traveled apart in 1 sec
            int v = Math.Abs(s1 - s2);

            for (int i = 0; i < queries.Length; i++)
                queries[i] = Math.Sqrt(Math.Pow((l - Math.Sqrt(queries[i])), 2) * 2) / v;

            return queries;
        }
    }
}
