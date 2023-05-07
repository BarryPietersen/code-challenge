using System;

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
    }

    public class Node 
    {
        public int data { get; set; }
        public Node? left { get; set; }
        public Node? right { get; set; }

        public Node(int key)
        {
            data = key;
            left = null;
            right = null;
        }
    }
}
