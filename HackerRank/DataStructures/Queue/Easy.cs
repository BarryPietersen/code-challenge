using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.DataStructures.Queue
{
    public class Easy
    {
        // https://www.hackerrank.com/challenges/jesse-and-cookies/problem
        public static int cookies(int k, int[] A)
        {
            if (A.Length == 0) return -1;
            if (A.Length == 1) return A[0] >= k ? 0 : -1;

            int tmp1;
            int tmp2;
            int ops = 0;
            Queue<int> tmpq;
            Queue<int> q1 = new Queue<int>(A.OrderBy(n => n));
            Queue<int> q2 = new Queue<int>();
            q2.Enqueue(q1.Dequeue());

            while (q1.Count > 1 && (q1.Peek() < k || q2.Peek() < k))
            {
                ops++;

                tmp1 = q1.Peek() < q2.Peek() ? q1.Dequeue() : q2.Dequeue();

                if (q2.Count == 0) tmp2 = q1.Dequeue();
                else
                    tmp2 = q1.Peek() < q2.Peek() ? q1.Dequeue() : q2.Dequeue();

                q2.Enqueue(tmp1 + (tmp2 * 2));

                if (q1.Count == 0)
                {
                    tmpq = q1;
                    q1 = q2;
                    q2 = tmpq;
                    q2.Enqueue(q1.Dequeue());
                }
                else if (q1.Count == 1 && q2.Count > 1)
                {
                    int n = q1.Dequeue();
                    q1.Enqueue(Math.Min(n, q2.Peek()));
                    q1.Enqueue(Math.Max(n, q2.Peek()));
                    q2.Dequeue();
                }
            }

            if (q1.Count > 1 || q1.Peek() >= k && q2.Peek() >= k) return ops;

            return Math.Min(q1.Peek(), q2.Peek()) + (2 * Math.Max(q1.Peek(), q2.Peek())) >= k ? ops + 1 : -1;
        }
    }
}
