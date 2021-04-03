using System;
using NUnit.Framework;

namespace Minesweeper.Tests
{
    public class BoardGeneratorTest
    {
        [Test]
        [TestCase(-1, -1)]
        public void Generate_InvalidDimension_ThrowsException(int numberOfRows, int numberOfColumns)
        {
            Assert.Throws<ArgumentException>(() =>
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
            int mineCount = 0;
            const int numberOfRows = 10;
            const int numberOfCols = 10;
            var minefield = BoardGenerator.Generate(numberOfRows, numberOfCols, 0.2f);

            for (int row = 0; row < numberOfRows; ++row)
            {
                for (int col = 0; col < numberOfCols; ++col)
                {
                    mineCount += (minefield[row, col]) ? 1 : 0;
                }
            }

            Assert.AreEqual(mineCount, 20);
        }

        [Test]
        [TestCase(10, 10, 0.2f)]
        public void Generate_ValidDensity_RandomGeneratorSucceeds(int numberOfRows, int numberOfColumns, float density)
        {
            Assert.DoesNotThrow(() => BoardGenerator.Generate(numberOfRows, numberOfColumns, density));
        }

        [Test]
        [TestCase(10, 10, 0.2f)]
        public void Generate_ValidDensity_RandomGeneratorMinefieldIsValid(int numberOfRows, int numberOfColumns,
            float density)
        {
            var minefield = BoardGenerator.Generate(numberOfRows, numberOfColumns, density);

            int mineCount = 0;
            for (int row = 0; row < numberOfRows; ++row)
            {
                for (int col = 0; col < numberOfColumns; ++col)
                {
                    if (minefield[row, col])
                        mineCount += 1;
                }
            }

            Assert.AreEqual(Convert.ToInt32(numberOfRows * numberOfColumns * density), mineCount);
        }
    }
}