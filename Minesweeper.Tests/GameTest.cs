using System;
using NUnit.Framework;

namespace Minesweeper.Tests
{
    public class GameTest
    {

        [Test]
        public void Constructor_GivenCorrectDimensions_CreateGame()
        {
            bool[,] mines = new bool[3, 3];
            new Game(mines);
        }

        [TestCase(2,2)]
        [TestCase(2, 3)]
        [TestCase(3, 2)]
        [TestCase(51, 51)]
        [TestCase(51, 50)]
        [TestCase(50, 51)]
        public void Constructor_GivenIncorrectDimensions_ThrowException(int nrow, int ncol)
        {
            bool[,] mines = new bool[nrow, ncol];
            Assert.Throws<ArgumentException>(() => new Game(mines));
        }
        [Test]
        [TestCase(3,3)]
        [TestCase(6,10)]
        [TestCase(45,50)]
        public void GetBoard_GivenBoard_EmptyBoard(int nrow, int ncol)
        {
            char[,] expectedBoard = new char[nrow,ncol];
            for (int r = 0; r < nrow; r++)
            {
                for (int c = 0; c < ncol; c++)
                {
                    expectedBoard[r, c] = '.';
                }
            }

            bool[,] mines = new bool[nrow, ncol];
            Game game = new Game(mines);
            Assert.That(game.Board, Is.EquivalentTo(expectedBoard));
        }

    }
}