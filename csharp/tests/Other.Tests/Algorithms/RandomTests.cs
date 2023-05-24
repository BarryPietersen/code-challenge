using System;
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

        [Theory]
        [InlineData(2, new string[] { "(())", "()()" })]
        [InlineData(3, new string[] { "((()))", "()()()", "(())()", "()(())", "(()())" })]
        [InlineData(4, new string[] { "(((())))", "((()()))", "((())())", "((()))()", "(()(()))", "(()()())", "(()())()", "(())(())", "(())()()", "()((()))", "()(()())", "()(())()", "()()(())", "()()()()" })]
        public void GenerateParenthesis_ReturnsExpected(int n, string[] expected)
        {
            // act
            var actual = Random.GenerateParenthesis(n);

            actual.Sort();
            Array.Sort(expected);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("CODINGCHALLENGESAREFUN", 1, "CODINGCHALLENGESAREFUN")]
        [InlineData("CODINGCHALLENGESAREFUN", 1000, "CODINGCHALLENGESAREFUN")]
        [InlineData("CODINGCHALLENGESAREFUN", 2, "CDNCALNEAEUOIGHLEGSRFN")]
        [InlineData("CODINGCHALLENGESAREFUN", 3, "CNANAUOIGHLEGSRFNDCLEE")]
        [InlineData("CODINGCHALLENGESAREFUN", 4, "CCNEOGHEGRFDNALEAUILSN")]
        [InlineData("CODINGCHALLENGESAREFUNS", 4, "CCNEOGHEGRFDNALEAUSILSN")]
        [InlineData("CODINGCHALLENGESAREFUNSS", 4, "CCNEOGHEGRFSDNALEAUSILSN")]
        public void ZigZag_ReturnsExpected(string input, int rows, string expected)
        {
            // act
            var actual = Random.ZigZag(input, rows);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1, new int[] { 1 })]
        [InlineData(new int[] { 1, 1, 1, 2, 2, 3 }, 2, new int[] { 1, 2 })]
        [InlineData(new int[] { 2, 2, 2, 1, 2, 1, 3, 2, 3 }, 3, new int[] { 2, 1, 3 })]
        [InlineData(new int[] { 2, 2, 2, 1, 2, 1, 3, 2, 3, 3, 1, 3, 3 }, 3, new int[] { 2, 3, 1 })]
        [InlineData(new int[] { 2, 2, 3, 2, 1, 2, 1, 3, 2, 3, 3, 1, 3, 3 }, 3, new int[] { 3, 2, 1 })]
        [InlineData(new int[] { 2, 2, 3, 2, 1, 2, 1, 3, 2, 3, 3, 1, 3, 3 }, 1, new int[] { 3 })]
        [InlineData(new int[] { 5, 3, 1, 1, 1, 3, 73, 1 }, 2, new int[] { 1, 3 })]
        public void TopKFrequent_ReturnsExpected(int[] nums, int k, int[] expected)
        {
            // act
            var actual = Random.TopKFrequent(nums, k);

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
