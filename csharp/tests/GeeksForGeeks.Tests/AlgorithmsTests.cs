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
    }
}
