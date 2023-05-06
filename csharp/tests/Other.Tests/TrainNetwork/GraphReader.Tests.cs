using Other.TrainNetwork;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Other.Tests.TrainNetwork
{
    public class GraphReaderTests
    {
        private const string inputDir = "TrainNetwork/input";

        [Fact]
        public void ReadFromTextFile_MatchExpected()
        {
            // arrange
            var isMatch = true;
            var path = $"{inputDir}/valid-data.txt";
            var expected = Expected_TextFileGraph();
            var _sut = GraphReader.ReadFromTextFile(path);

            // act
            if (expected.AdjacencyList.Count != _sut.AdjacencyList.Count)
            {
                Assert.True(false); return;
            }

            foreach (var station in expected.AdjacencyList)
            {
                if (!_sut.AdjacencyList.ContainsKey(station.Key) || station.Value.Count != _sut.AdjacencyList[station.Key].Count)
                {
                    isMatch = false; break;
                }

                foreach (var neighbour in station.Value)
                {
                    if (!_sut.AdjacencyList[station.Key].ContainsKey(neighbour.Key) ||
                         _sut.AdjacencyList[station.Key][neighbour.Key] != neighbour.Value)
                    {
                        isMatch = false; break;
                    }
                }
            }

            // assert
            Assert.True(isMatch);
        }

        [Fact]
        public void ReadFromTextFile_WithBadData_ThrowsFormatException()
        {
            // arrange
            string path = $"{inputDir}/bad-data.txt";

            // assert
            Assert.Throws<FormatException>(() => GraphReader.ReadFromTextFile(path));
        }

        [Fact]
        public void ReadFromTextFile_FileDoesNotExist_ThrowsFileNotFoundException()
        {
            // arrange
            string path = $"{inputDir}/not-found.txt";

            // assert
            Assert.Throws<FileNotFoundException>(() => GraphReader.ReadFromTextFile(path));
        }

        // text file data 
        // AB5
        // BC4
        // CD8
        // DC8
        // DE6
        // AD5
        // CE2
        // EB3
        // AE7
        private static Graph Expected_TextFileGraph()
        {
            var adjacencyList = new Dictionary<char, Dictionary<char, int>>()
            {
                { 'A', new Dictionary<char, int>(){ { 'B', 5 }, { 'D', 5}, { 'E', 7} } },
                { 'B', new Dictionary<char, int>(){ { 'C', 4 } } },
                { 'C', new Dictionary<char, int>(){ { 'D', 8 }, { 'E', 2 } } },
                { 'D', new Dictionary<char, int>(){ { 'C', 8 }, { 'E', 6 } } },
                { 'E', new Dictionary<char, int>(){ { 'B', 3 } } },
            };

            return new Graph(adjacencyList);
        }
    }
}
