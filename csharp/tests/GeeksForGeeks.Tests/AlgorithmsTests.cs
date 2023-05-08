using GeeksForGeeks.OtherChallenges;
using Xunit;

namespace GeeksForGeeks.Tests
{
    public class AlgorithmsTests
    {
        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 1 }, 1)]
        [InlineData(new int[] { 1, 2, 1 }, 1)]        
        [InlineData(new int[] { 3, 1, 3, 3, 2 }, 3)]
        [InlineData(new int[] { 1, 2 }, -1)]
        [InlineData(new int[] { 1, 2, 3 }, -1)]
        [InlineData(new int[] { 1, 1, 3, 3 }, -1)]
        [InlineData(new int[] { 1, 1, 3, 3, 5 }, -1)]
        public void majorityElement_ReturnsExpected(int[] input, int expected)
        {
            // act
            var actual = Algorithms.majorityElement(input, input.Length);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 0, 1 }, 1)]
        [InlineData(new int[] { 1, 1 }, 2)]
        [InlineData(new int[] { 1, 2, 1 }, 4)]
        [InlineData(new int[] { 2, 0, 0, -1, 2 }, 3)]
        [InlineData(new int[] { 1, 2, 3, -2, 5 }, 9)]
        [InlineData(new int[] { 1, 2, 3, -7, 6 }, 6)]
        [InlineData(new int[] { 1, 2, 3, -5, 6, 1 }, 8)]
        [InlineData(new int[] { 1, 2, 3, -5, 6, -1, 1, 0, 10 }, 17)]
        [InlineData(new int[] { -1, -2, -3, -4 }, -1)]
        [InlineData(new int[] { -1, -2, -3, 0 }, 0)]
        [InlineData(new int[] { -1, 2, -3, 0 }, 2)]
        public void maxSubarraySum_ReturnsExpected(int[] input, int expected)
        {
            // act
            var actual = Algorithms.maxSubarraySum(input, input.Length);

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
