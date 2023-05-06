using System.Collections.Generic;
using System.Linq;

namespace HackerRank.DataStructures.Medium
{
    public static class Queues
    {
        // https://www.hackerrank.com/challenges/castle-on-the-grid/problem
        public static int minimumMoves(string[] grid, int startX, int startY, int goalX, int goalY)
        {
            // swap the coordinates from hackerranks input,
            // these are not named in the traditional x y order
            swap(ref startX, ref startY);
            swap(ref goalX, ref goalY);

            if (startX == goalX && startY == goalY) return 0;

            Point current;
            IEnumerable<int> walks;
            var udlr = new int[4]; // up down left right
            var q = new Queue<Point>();
            var visited = new HashSet<string>();

            q.Enqueue(new Point(startX, startY, 1));

            while (q.Count > 0)
            {
                current = q.Dequeue();

                udlr[0] = walk(grid, q, visited, current, goalX, goalY, 0, -1); // up
                udlr[1] = walk(grid, q, visited, current, goalX, goalY, 0, 1);  // down
                udlr[2] = walk(grid, q, visited, current, goalX, goalY, -1, 0); // left
                udlr[3] = walk(grid, q, visited, current, goalX, goalY, 1, 0);  // right

                visited.Add(current.ToString());

                walks = udlr.Where(c => c > 0);

                if (walks.Any()) return walks.First();
            }

            return -1; // no path was found
        }

        private static int walk(string[] grid, Queue<Point> q, HashSet<string> visited, Point current, int goalX, int goalY, int x, int y)
        {
            if (outOfBounds(grid, visited, current.X, current.Y)) return -1;
            if (current.X == goalX && current.Y == goalY) return current.Count;

            if (x != 0) // we are currently walking horizontally
            {
                // check if we could walk up or down from here
                if (!outOfBounds(grid, visited, current.X, current.Y + 1) ||
                    !outOfBounds(grid, visited, current.X, current.Y - 1))
                {
                    q.Enqueue(new Point(current.X, current.Y, current.Count + 1));
                }
            }
            else // vertically
            {
                if (!outOfBounds(grid, visited, current.X + 1, current.Y) ||
                    !outOfBounds(grid, visited, current.X - 1, current.Y))
                {
                    q.Enqueue(new Point(current.X, current.Y, current.Count + 1));
                }
            }

            var next = new Point(current.X + x, current.Y + y, current.Count);

            return walk(grid, q, visited, next, goalX, goalY, x, y);
        }

        private static bool outOfBounds(string[] grid, HashSet<string> visited, int x, int y)
        {
            return (x < 0 || x >= grid[0].Length || y < 0 || y >= grid.Length ||
                grid[y][x] == 'X' || visited.Contains($"{x} {y}"));
        }

        private class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Count { get; set; }

            public Point(int x, int y, int count)
            {
                X = x;
                Y = y;
                Count = count;
            }

            public override string ToString() => $"{X} {Y}";
        }

        private static void swap(ref int x, ref int y)
        {
            var temp = x;
            x = y;
            y = temp;
        }
    }
}

