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
    }
}