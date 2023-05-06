using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerRank.DataStructures.Easy
{
    public static class Queues
    {
        // https://www.hackerrank.com/challenges/jesse-and-cookies/problem
        public static int cookies(int k, int[] A)
        {
            if (A.Length == 0) return -1;
            if (A.Length == 1) return A[0] >= k ? 0 : -1;

            int cookie1;        // 1st min at each iteration
            int cookie2;        // 2nd min at each iteration
            var ops = 0;
            var q1 = new Queue<int>(A.OrderBy(n => n));
            var q2 = new Queue<int>();

            q2.Enqueue(q1.Dequeue());

            while (q1.Count > 1 && (q1.Peek() < k || q2.Peek() < k))
            {
                // select the two smallest cookies from the queues
                cookie1 = q1.Peek() < q2.Peek() ? q1.Dequeue() : q2.Dequeue();

                if (q2.Count == 0)
                    cookie2 = q1.Dequeue();
                else
                    cookie2 = q1.Peek() < q2.Peek() ? q1.Dequeue() : q2.Dequeue();

                // perform the operation on two cookies
                // and enqueue result to 'second' queue
                q2.Enqueue(cookie1 + (cookie2 * 2));
                ops++;

                // always ensure both queues have
                // at least one cookie each, also
                // the reason for swapping references
                // when this condition evaluates true
                // is to simplify the enqueuing step,
                // and the seperation of values that
                // have been operated on and values
                // that are still potentially original form
                if (q1.Count == 0)
                {
                    var tmp = q1;
                    q1 = q2;
                    q2 = tmp;
                    q2.Enqueue(q1.Dequeue());
                }
                else if (q1.Count == 1 && q2.Count > 1)
                {
                    q1.Enqueue(Math.Min(q1.Peek(), q2.Peek()));
                    q1.Enqueue(Math.Max(q1.Peek(), q2.Peek()));
                    q1.Dequeue();
                    q2.Dequeue();
                }
            }

            if (q1.Peek() >= k && q2.Peek() >= k) return ops;

            // check if the last two cookies in the queues can be operated on to satisfy being greater than k
            return Math.Min(q1.Peek(), q2.Peek()) + (2 * Math.Max(q1.Peek(), q2.Peek())) >= k ? ops + 1 : -1;
        }
    }
}
