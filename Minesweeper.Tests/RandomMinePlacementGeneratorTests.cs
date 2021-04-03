using System;
using NUnit.Framework;

namespace Minesweeper.Tests
{
    public class RandomMinePlacementGeneratorTests
    {
        [Test]
        [TestCase(-50, -50)]
        public void Constructor_InvalidDimensions_Throws(int numberOfRows, int numberOfCols)
        {
            Assert.Throws<ArgumentException>(() => new RandomMinePlacementGenerator(numberOfRows, numberOfCols));
        }
    }
}