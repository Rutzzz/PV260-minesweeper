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
                    BoardGenerator.Generate(null, numberOfRows, numberOfColumns, 0.5f)
            );
        }
        
        [Test]
        [TestCase(0.7f)]
        [TestCase(0.1f)]
        public void Generate_InvalidDensity_Throw(float density)
        {
            Assert.Throws<ArgumentException>(() => 
                BoardGenerator.Generate(null, 11, 10, density)
            );
        }
        
        [Test]
        public void Generate_CorrectMineCount()
        {
            int cnt = 0;
            int numberOfRows = 10, numberOfCols = 10;
            var minefield = BoardGenerator.Generate(null, numberOfRows, numberOfCols, 0.2f);
            
            for (int row = 0; row < numberOfRows; ++row)
            {
                for (int col = 0; col < numberOfCols; ++col)
                {
                    cnt += (minefield[row, col]) ? 1 : 0;
                }
            }
            
            Assert.AreEqual(cnt, 20);
        }
        
        [Test]
        public void Generate_MinesRandom()
        {
            int numberOfRows = 10, numberOfCols = 10;

            var generator = new Mod2MineGenerator(numberOfRows, numberOfCols);
            var generatorCmp = new Mod2MineGenerator(numberOfRows, numberOfCols);
            
            var minefield = BoardGenerator.Generate(
                generator,
                numberOfRows,
                numberOfCols, 0.5f);

            var expected = new bool[numberOfRows, numberOfCols];
            foreach (var mine in generatorCmp.MineCoordinates)
            {
                expected[mine.Item1, mine.Item2] = true;
            }
            
            Assert.AreEqual(expected, minefield);
        }
        
        [Test]
        [TestCase(10,10, 0.2f)]
        public void Generate_ValidDensity_RandomGeneratorSucceeds(int numberOfRows, int numberOfColumns, float density)
        {
            Assert.DoesNotThrow(() => BoardGenerator.Generate(null, numberOfRows, numberOfColumns, density ));
        }
        
        [Test]
        [TestCase(10,10, 0.2f)]
        public void Generate_ValidDensity_RandomGeneratorMinefieldIsValid(int numberOfRows, int numberOfColumns, float density)
        {
            var minefield = BoardGenerator.Generate(null, numberOfRows, numberOfColumns, density);

            int cnt = 0;
            for (int row = 0; row < numberOfRows; ++row)
            {
                for (int col = 0; col < numberOfColumns; ++col)
                {
                    cnt += (minefield[row, col]) ? 1 : 0;
                }
            }
            
            Assert.AreEqual(Convert.ToInt32(numberOfRows*numberOfColumns*density), cnt);
        }
        
        [Test]
        [TestCase(10, 10, 0.5f, 1)]
        public void Generate_SmallEntropy_Throws(int numberOfRows, int numberOfColumns, float density, int poolSize)
        {
            var generator = new Mod2MineGenerator(poolSize, poolSize);
            Assert.Throws<NullReferenceException>(() => BoardGenerator.Generate(
                generator,
                numberOfRows,
                numberOfColumns, 0.5f));
        }
    }
}