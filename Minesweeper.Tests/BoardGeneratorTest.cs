using System;
using NUnit.Framework;

namespace Minesweeper.Tests
{
    public class BoardGeneratorTest
    {
        [Test]
        [TestCase(-1,-1)]
        public void Generate_InvalidDimension_ThrowsException(int numberOfRows, int numberOfColumns)
        {
            Assert.Throws<ArgumentException>( ( ) =>
                    BoardGenerator.Generate(numberOfRows, numberOfColumns, 0.5f)
            );
        }
    }
}