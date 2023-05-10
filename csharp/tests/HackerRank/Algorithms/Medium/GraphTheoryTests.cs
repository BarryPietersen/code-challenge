using System.Linq;
using Xunit;
using GraphTheory = HackerRank.Algorithms.Medium.GraphTheory;

namespace HackerRank.Tests.Algorithms.Medium
{
    public class GraphTheoryTests
    {
        [Theory]
        [InlineData(new string[] 
        { 
            "32 62", 
            "42 68", 
            "12 98" 
        }, new string[] 
        {  
            "95 13",
            "97 25",
            "93 37",
            "79 27",
            "75 19",
            "49 47",
            "67 17" 
        }, 3)]
        public void quickestWayUp_ReturnsExpected(string[] inputLadders, string[] inputSnakes, int expected)
        {
            // arrange
            static int[][] parse(string[] input) => input
                .Select(x => x
                .Split(' ')
                .Select(x => int.Parse(x))
                .ToArray())
                .ToArray()!;

            var ladders = parse(inputLadders);
            var snakes = parse(inputSnakes);

            // act
            var actual = GraphTheory.quickestWayUp(ladders, snakes);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new int[] { 1, 1, 3, 2, 1, 2, 6, 8, 8 }, 2)]
        public void evenForest_ReturnsExpected(int[] from, int[] to, int expected)  
        {
            // act
            var actual = GraphTheory.evenForest(from.Length, to.Length, from.ToList(), to.ToList());

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
