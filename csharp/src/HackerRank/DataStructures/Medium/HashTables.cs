using System.Collections.Generic;

namespace HackerRank.DataStructures.Medium
{
    public static class HashTables
    {
        // https://www.hackerrank.com/challenges/frequency-queries/problem
        public static List<int> freqQuery(List<int[]> queries)
        {
            var present = new List<int>();
            var totals = new Dictionary<int, int>();
            var frequency = new Dictionary<int, int>();

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
                    {
                        frequency.Add(query[1], 1);
                    }

                    if (totals.ContainsKey(frequency[query[1]]))
                    {
                        totals[frequency[query[1]]]++;
                    }
                    else
                    {
                        totals.Add(frequency[query[1]], 1);
                    }
                }
                else if (query[0] == 2)
                {
                    if (frequency.ContainsKey(query[1]) && frequency[query[1]] > 0)
                    {
                        totals[frequency[query[1]]]--;
                        frequency[query[1]]--;

                        if (totals.ContainsKey(frequency[query[1]]))
                        {
                            totals[frequency[query[1]]]++;
                        }
                        else
                        {
                            totals.Add(frequency[query[1]], 1);
                        }
                    }
                }
                else
                {
                    present.Add(totals.TryGetValue(query[1], out int value) && value > 0 ? 1 : 0);
                }
            }

            return present;
        }
    }
}
