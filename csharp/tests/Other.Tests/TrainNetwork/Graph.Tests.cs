using System.Collections.Generic;
using System;
using Xunit;
using Other.TrainNetwork;

namespace Other.Tests.TrainNetwork
{
    public class GraphTests
    {
        // system under test
        private readonly Graph _sut;

        public GraphTests()
        {
            _sut = new Graph(InitializeAdjacencyList());
        }

        [Theory]
        [InlineData(new char[] { 'A', 'B', 'C' }, 9)]
        [InlineData(new char[] { 'A', 'D' }, 5)]
        [InlineData(new char[] { 'A', 'D', 'C' }, 13)]
        [InlineData(new char[] { 'A', 'E', 'B', 'C', 'D' }, 22)]
        public void CalculateRouteDistance_RouteExists_ReturnsPositive(char[] stations, int expected)
        {
            // arrange

            // act
            int result = _sut.CalculateRouteDistance(stations);

            // assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new char[] { 'A', 'E', 'D' })]
        [InlineData(new char[] { 'B', 'D', 'E' })]
        [InlineData(new char[] { 'A', 'E', 'D', 'A' })]
        [InlineData(new char[] { 'B', 'D', 'E', 'C' })]
        public void CalculateRouteDistance_NoRouteExists_ReturnsNegativeOne(char[] stations)
        {
            // arrange
            int expected = -1;

            // act
            int result = _sut.CalculateRouteDistance(stations);

            // assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 'C', 9)]
        [InlineData('B', 'B', 9)]
        public void CalculateShortestRoute_RouteExists_ReturnsPositive(char source, char destination, int expected)
        {
            // arrange

            // act
            int result = _sut.CalculateShortestRoute(source, destination);

            // assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('B', 'A')]
        [InlineData('E', 'A')]
        public void CalculateShortestRoute_NoRouteExists_ReturnsNegativeOne(char source, char destination)
        {
            // arrange
            int expected = -1;

            // act
            int result = _sut.CalculateShortestRoute(source, destination);

            // assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('C', 'C', 3, 2)]
        [InlineData('C', 'C', 5, 6)]
        [InlineData('A', 'C', 5, 9)]
        public void CountDistinctRoutesWithinMaxStops_RouteExists_ReturnsPositive(char source, char destination, int stops, int expected)
        {
            // arrange

            // act
            int result = _sut.CountDistinctRoutesWithinMaxStops(source, destination, stops);

            // assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('E', 'D', 2)]
        [InlineData('C', 'A', 6)]
        [InlineData('X', 'Z', 50)]
        public void CountDistinctRoutesWithinMaxStops_NoRouteExists_ReturnsZero(char source, char destination, int stops)
        {
            // arrange
            int expected = 0;

            // act
            int result = _sut.CountDistinctRoutesWithinMaxStops(source, destination, stops);

            // assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 'C', 4, 3)]
        [InlineData('A', 'C', 2, 2)]
        [InlineData('B', 'B', 4, 1)]
        public void CountDistinctRoutesWithExactStops_RouteExists_ReturnsPositive(char source, char destination, int stops, int expected)
        {
            // arrange

            // act
            int result = _sut.CountDistinctRoutesWithExactStops(source, destination, stops);

            // assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('E', 'X', 4)]
        [InlineData('B', 'E', 1)]
        public void CountDistinctRoutesWithExactStops_NoRouteExists_ReturnsZero(char source, char destination, int stops)
        {
            // arrange
            int expected = 0;

            // act
            int result = _sut.CountDistinctRoutesWithExactStops(source, destination, stops);

            // assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('C', 'C', 30, 7)]
        [InlineData('A', 'C', 20, 5)]
        [InlineData('A', 'C', 30, 11)]
        public void CountDistinctRoutesWithinMaxDistance_RouteExists_ReturnPositive(char source, char destination, int distance, int expected)
        {
            // arrange

            // act
            int result = _sut.CountDistinctRoutesWithinMaxDistance(source, destination, distance);

            // assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 'C', 8)]
        [InlineData('C', 'B', 4)]
        [InlineData('A', 'C', 0)]
        public void CountDistinctRoutesWithinMaxDistance_RouteOutOfRange_ReturnsZero(char source, char destination, int distance)
        {
            // arrange
            int expected = 0;

            // act
            int result = _sut.CountDistinctRoutesWithinMaxDistance(source, destination, distance);

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Graph_WithNullArgument_ThrowsArgumentNullException()
        {
            // arrange
            Dictionary<char, Dictionary<char, int>> adjlist = null;

            // assert
            Assert.Throws<ArgumentNullException>(() => new Graph(adjlist));

        }

        // AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7
        private Dictionary<char, Dictionary<char, int>> InitializeAdjacencyList()
        {
            Dictionary<char, Dictionary<char, int>> adjacencyList = new Dictionary<char, Dictionary<char, int>>()
            {
                { 'A', new Dictionary<char, int>(){ { 'B', 5 }, { 'D', 5}, { 'E', 7} } },
                { 'B', new Dictionary<char, int>(){ { 'C', 4 } } },
                { 'C', new Dictionary<char, int>(){ { 'D', 8 }, { 'E', 2 } } },
                { 'D', new Dictionary<char, int>(){ { 'C', 8 }, { 'E', 6 } } },
                { 'E', new Dictionary<char, int>(){ { 'B', 3 } } },
            };

            return adjacencyList;
        }

        // the stations and their neighbours
        // A - B D E
        // B - C 
        // C - D E
        // D - C E
        // E - B

        // permuations of routes from C - C 
        // CDC
        // CEBC
        // CDEBC
        // CDCDC
        // CEBCDC
        // CDCEBC

        // permutations of routes from A - C with their distances
        // AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7
        // ABC          = 9
        // ADC          = 13
        // AEBC         = 14
        // ADEBC        = 18
        // ABCDC        = 25
        // ADCDC        = 29
        // ADCEBC       = 22
        // ABCEBC       = 18
        // AEBCEBC      = 23
        // ADEBCEBC     = 27
        // ABCEBCEBC    = 27
        // AEBCDC       = 30
        // ADCEBCEBC    = 31
    }
}
