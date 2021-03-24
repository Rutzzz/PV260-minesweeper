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

        [TestCase(2, 2)]
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
        [TestCase(3, 3)]
        [TestCase(6, 10)]
        [TestCase(45, 50)]
        public void GetBoard_GivenBoard_EmptyBoard(int nrow, int ncol)
        {
            bool[,] mines = new bool[nrow, ncol];
            Game game = new Game(mines);
            Assert.That(game.Board, Is.EqualTo(CreateFilledBoard(nrow, ncol, '.')));
        }

        [TestCase(1, 1, 1, 2)]
        [TestCase(1, 1, 2, 2)]
        [TestCase(3, 3, 2, 2)]
        public void Uncover_GivenStepsNextToMine_FieldWithNumber(int mineRow, int mineCol, int posRow, int posCol)
        {
            char[,] expectedBoard = CreateFilledBoard(4, 4, '.');
            expectedBoard[posRow - 1, posCol - 1] = '1';
            bool[,] mines = new bool[4, 4];
            mines[mineRow - 1, mineCol - 1] = true;
            Game game = new Game(mines);

            game.Uncover(posRow, posCol);

            Assert.That(game.Board, Is.EqualTo(expectedBoard));
        }

        [Test]
        public void Uncover_GivenNoMines_UncoverAll()
        {
            var mines = new bool[5,5];
            var expectedBoard = CreateFilledBoard(5, 5, '0');

            var game = new Game(mines);
            game.Uncover(2, 2);
            
            Assert.That(game.Board, Is.EqualTo(expectedBoard));
        }
        
        [Test]
        public void Uncover_GivenAMine_DoNotUncoverMine() 
        {
            var mines = new bool[5,5];
            mines[0, 0] = true;
            var expectedBoard = CreateFilledBoard(5, 5, '0');
            expectedBoard[0, 0] = '.';
            expectedBoard[1, 0] = '1';
            expectedBoard[0, 1] = '1';
            expectedBoard[1, 1] = '1';

            var game = new Game(mines);
            game.Uncover(3, 3);
            
            Assert.That(game.Board, Is.EqualTo(expectedBoard));
        }
        
        [Test]
        public void Uncover_GivenMultipleMines_DoNotUncoverUnreachable() 
        {
            var mines = new bool[4, 4];
            mines[1, 1] = true;
            var expectedBoard = new[,]
            {
                {'.', '.', '1', '0'},
                {'.', '.', '1', '0'},
                {'1', '1', '1', '0'},
                {'0', '0', '0', '0'},
            };

            var game = new Game(mines);
            game.Uncover(4, 4);
            
            Assert.That(game.Board, Is.EqualTo(expectedBoard));
        }

        [Test]
        public void Uncover_GivenUncoverNextToMultipleMines_FieldWithCorrectNumber()
        {
            const int uncoverRow = 2;
            const int uncoverCol = 2;
            var minePositions = new[]
            {
                (1, 2),
                (2, 1)
            };
            var expectedCell = char.Parse(minePositions.Length.ToString());

            var mines = new bool[4, 4];
            foreach (var (col, row) in minePositions)
            {
                mines[col, row] = true;
            }

            var game = new Game(mines);
            game.Uncover(uncoverRow, uncoverCol);
            
            Assert.That(game.Board[1, 1], Is.EqualTo(expectedCell));
        }

        [Test]
        public void FlagTile_GivenFlagTile_FlagOnBoard()
        {
            var mines = new bool[3, 3];
            var expectedBoard = CreateFilledBoard(3, 3, '.');
            expectedBoard[1, 1] = 'f';

            var game = new Game(mines);
            game.FlagTile(2, 2);
            
            Assert.That(game.Board, Is.EqualTo(expectedBoard));
        }
        
        [Test]
        public void FlagTile_GivenUncoveredTile_NoFlagPlaced()
        {
            var mines = new bool[3, 3];
            var expectedBoard = CreateFilledBoard(3, 3, '0');

            var game = new Game(mines);
            game.Uncover(2, 2);
            game.FlagTile(2, 2);
            
            Assert.That(game.Board, Is.EqualTo(expectedBoard));
        }

        private char[,] CreateFilledBoard(int nrow, int ncol, char cellValue)
        {
            char[,] board = new char[nrow, ncol];
            for (int r = 0; r < nrow; r++)
            {
                for (int c = 0; c < ncol; c++)
                {
                    board[r, c] = cellValue;
                }
            }

            return board;
        }
    }
}