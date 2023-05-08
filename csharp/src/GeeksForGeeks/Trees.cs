using System;
using System.Collections.Generic;

namespace GeeksForGeeks
{
    public static class Trees
    {
        /*
            Input:
                      10
                    /   \
                  20    30
                 /   \ 
                40    60

            Output: 4
        */
        // https://practice.geeksforgeeks.org/problems/diameter-of-binary-tree/1
        public static int diameter(Node root)
        {
            var result = dfsDiameter(root);

            return Math.Max(result.Item1, result.Item2);
        }

        private static (int, int) dfsDiameter(Node? current) 
        {
            if (current == null)
            {
                return (0, 0);
            }

            var (maxDepthLeft, maxPerimeterLeft) = dfsDiameter(current.left);
            var (maxDepthRight, maxPerimeterRight) = dfsDiameter(current.right);

            return (
                Math.Max(maxDepthLeft, maxDepthRight) + 1,
                Math.Max(maxDepthLeft + maxDepthRight + 1, Math.Max(maxPerimeterLeft, maxPerimeterRight))
            );
        }

        /*
            Input: 
                      2
                    /   \
                   1     10
                        /
                       3
                        \
                         6
	                   /   \
                      4     9
                       \
	                    5

            Output: 1 2 10 9

            Input:

                          7
                       /     \
                     5         8
                   /   \         \
                 2       6         56
                /  \             /    \ 
               1     3        10        57
                     \      /    \
                       4  9        51
                                /     \
	                          13        52
                            /    \         \
                          12       45       55
                          /           \  
                         11             49  
                                      /
                                   53 

            Output: 1 2 5 7 8 56 57 55

        */
        // https://practice.geeksforgeeks.org/problems/top-view-of-binary-tree/1
        public static List<int> topView(Node root)
        {
            var maxLeft = 0;
            var maxRight = 0;
            var head = new List<int>();
            var tail = new List<int> { root.data };
            var q = new Queue<QItem>(new[] { new QItem { Node = root, X = 0 } });

            while (q.TryDequeue(out var item))
            {
                if (item.Node is null) 
                {
                    continue; 
                }

                q.Enqueue(new QItem { Node = item.Node.left, X = item.X - 1 });
                q.Enqueue(new QItem { Node = item.Node.right, X = item.X + 1 });

                if (item.X < maxLeft)
                {
                    maxLeft = item.X;
                    head.Add(item.Node.data);
                }
                else if (item.X > maxRight)
                {
                    maxRight = item.X;
                    tail.Add(item.Node.data);
                }
            }

            head.Reverse();
            head.AddRange(tail);

            return head;
        }

        private class QItem 
        {
            public Node? Node { get; set; }
            public int X { get; set; }
        }
    }

    public class Node
    {
        public int data;
        public Node left;
        public Node right;

        public Node(int key)
        {
            this.data = key;
            this.left = null;
            this.right = null;
        }
    }
}
