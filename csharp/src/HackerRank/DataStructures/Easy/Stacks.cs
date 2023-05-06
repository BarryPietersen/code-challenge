using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerRank.DataStructures.Easy
{
    public static class Stacks
    {
        // https://www.hackerrank.com/challenges/maximum-element/problem
        public static void MaximumElement()
        {
            int n;
            int tmp;
            int[] query;
            n = int.Parse(Console.ReadLine());
            var stk = new Stack<int>();
            var sorted = new SortedDictionary<int, int>(new Comp());

            for (var i = 0; i < n; i++)
            {
                query = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                if (query[0] == 1)
                {
                    stk.Push(query[1]);

                    if (sorted.ContainsKey(query[1]))
                    {
                        sorted[query[1]]++;
                    }
                    else
                        sorted.Add(query[1], 1);
                }
                else if (query[0] == 2)
                {
                    tmp = stk.Pop();
                    sorted[tmp]--;

                    if (sorted[tmp] == 0)
                    {
                        sorted.Remove(tmp);
                    }
                }
                else
                    Console.WriteLine(sorted.First().Key);
            }

        }

        private class Comp : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                if (x < y) return 1;
                else if (x > y) return -1;
                else return 0;
            }
        }
    }
}
