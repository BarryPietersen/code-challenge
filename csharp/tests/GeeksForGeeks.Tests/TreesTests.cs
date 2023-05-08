using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GeeksForGeeks.Tests
{
    public class TreesTests
    {
        [Theory]
        [InlineData("2 1 10 N N 3 N N 6 4 9 N 5", new int[] { 1, 2, 10, 9 })]
        [InlineData("7 5 8 2 6 N 56 1 3 N N 10 57 N N N 4 9 51 N N N N N N 13 52 12 45 N 55 11 N 20 49 54 N N N 15 44 48 50 53 N 14 18 23 N 46 N N N N N N N 16 19 21 30 N 47 N 17 N N N 22 28 34 N N N N N N 24 29", new int[] { 1, 2, 5, 7, 8, 56, 57, 55 })]
        public void topView_RetunsExpected(string input, int[] expected)
        {
            var node = BuildTree(input);
            var actual = Trees.topView(node);

            AssertEqual(expected.ToList(), actual);
        }

        [Theory]
        [InlineData("1 3 2", new int[] { 3, 1, 2 })]
        [InlineData("14 14 3 N 8 8 12 N 6 17 3 N 1 11 10 N 6 6 13 N 10 17 7 N 11 7", new int[] { 7, 6, 7, 13, 11, 10 })]
        public void bottomView_RetunsExpected(string input, int[] expected)
        {
            var node = BuildTree(input);
            var actual = Trees.bottomView(node);

            AssertEqual(expected.ToList(), actual);
        }

        private static void AssertEqual<T>(List<T> expected, List<T> actual) 
        {
            if (expected == actual)
            {
                return;
            }

            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }

        private static Node BuildTree(string str)
        {
            var tokens = str.Split(' ');
            var root = new Node(int.Parse(tokens[0]));
            var queue = new Queue<Node>();
            queue.Enqueue(root);

            var i = 1;
            while (queue.TryDequeue(out var currNode) && i < tokens.Length)
            {
                var currVal = tokens[i++];

                if (currVal != "N")
                {
                    currNode.left = new Node(int.Parse(currVal));
                    queue.Enqueue(currNode.left);
                }

                if (i >= tokens.Length)
                {
                    break;
                }

                currVal = tokens[i++];

                if (currVal != "N")
                {
                    currNode.right = new Node(int.Parse(currVal));
                    queue.Enqueue(currNode.right);
                }
            }

            return root;
        }
    }
}
