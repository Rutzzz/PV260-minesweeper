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
        
        [Test]
        [TestCase(0.7f)]
        [TestCase(0.1f)]
        public void Generate_InvalidDensity_Throw(float density)
        {
            Assert.Throws<ArgumentException>(() => 
                BoardGenerator.Generate(11, 10, density)
            );
        }
        
        [Test]
        public void Generate_CorrectMineCount()
        {
            int cnt = 0;
            int numberOfRows = 10, numberOfCols = 10;
            var minefield = BoardGenerator.Generate(numberOfRows, numberOfCols, 0.2f);
            
            for (int row = 0; row < numberOfRows; ++row)
            {
                for (int col = 0; col < numberOfCols; ++col)
                {
                    cnt += (minefield[row, col]) ? 1 : 0;
                }
            }
            
            Assert.AreEqual(cnt, 20);
        }
    }
}