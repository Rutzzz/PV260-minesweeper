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
    }
}