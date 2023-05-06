using Xunit;
using Random = Other.Algorithms.Random;

namespace Other.Tests.Algorithms
{
    public class RandomTests
    {
        [Fact]
        public void TreeConstructor_WhenProperBinaryTree_ReturnsTrue() 
        {
            // arrange
            var input = new string[] { "(1,2)", "(2,4)", "(5,7)", "(7,2)", "(9,5)" };

            // act
            var actual = Random.TreeConstructor(input);

            // assert
            Assert.Equal("true", actual);
        }

        [Fact]
        public void TreeConstructor_WhenNodeHasMoreThan2Children_ReturnsFalse()
        {
            // arrange
            var input = new string[] { "(1,2)", "(3,2)", "(2,12)", "(5,2)" };

            // act
            var actual = Random.TreeConstructor(input);

            // assert
            Assert.Equal("false", actual);
        }

        [Fact]
        public void TreeConstructor_WhenInputContainsMultipleBinaryTrees_ReturnsFalse()
        {
            // arrange
            var input = new string[] { "(1,2)", "(2,4)", "(5,7)", "(100,50)", "(7,2)", "(9,5)" };

            // act
            var actual = Random.TreeConstructor(input);

            // assert
            Assert.Equal("false", actual);
        }

        [Theory]
        [InlineData("1 1 + 1 + 1 +", "4")]
        [InlineData("4 5 + 2 1 + *", "27")]
        [InlineData("2 3 - 4 + 5 6 7 * + *", "141")]
        public void ReversePolishNotation_ComputesValueCorrectly(string input, string expected) 
        {
            // act
            var actual = Random.ReversePolishNotation(input);

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
