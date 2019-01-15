using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.DataStructures.Trees
{
    public static class Easy
    {
        public class Node
        {
            public int data;
            public Node left;
            public Node right;

            public Node(int data)
            {
                this.data = data;
            }
        }

        public static void postOrder(Node root)
        {
            if (root == null) return;

            postOrder(root.left);
            postOrder(root.right);

            Console.WriteLine($" {root.data}");
        }

        public static void inOrder(Node root)
        {
            if (root == null) return;

            postOrder(root.left);
            Console.WriteLine($" {root.data}");
            postOrder(root.right);
        }

        public static int height(Node root)
        {
            if (root == null) return 0;
            return 1 + Math.Max(height(root.left), height(root.right));
        }

        /*
             1
            / \
               2
                \
                 5
                /  \
               3    6
                \
                 4        
        */

        public static void levelOrder(Node root)
        {
            if (root == null) return;
            Queue<Node> level = new Queue<Node>();
            level.Enqueue(root);
            Node temp;

            while (level.Count > 0)
            {
                temp = level.Dequeue();

                Console.WriteLine(temp.data + " ");

                if (temp.left != null)
                {
                    level.Enqueue(temp.left);
                }
                if (temp.right != null)
                {
                    level.Enqueue(temp.right);
                }
            }
        }

        //private static Node insert(Node root, int data)
        //{
        //    if (root == null)
        //    {
        //        Node node = new Node(data);
        //        root = node;
        //    }
        //    else if (root.data > data) root.left = insert(root.left, data);
        //    else if (root.data < data) root.right = insert(root.right, data);

        //    return root;
        //}
    }
    
}
