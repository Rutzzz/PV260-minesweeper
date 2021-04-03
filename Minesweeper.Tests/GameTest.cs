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
            Board expectedBoard = CreateFilledBoard(4, 4, '.');
            expectedBoard[posRow, posCol] = '1';
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
            expectedBoard[1, 1] = '.';
            expectedBoard[2, 1] = '1';
            expectedBoard[1, 2] = '1';
            expectedBoard[2, 2] = '1';

            var game = new Game(mines);
            game.Uncover(3, 3);
            
            Assert.That(game.Board, Is.EqualTo(expectedBoard));
        }
        
        [Test]
        public void Uncover_GivenMultipleMines_DoNotUncoverUnreachable() 
        {
            var mines = new bool[4, 4];
            mines[1, 1] = true;
            var expectedBoard = new Board(new[,]
            {
                {'.', '.', '1', '0'},
                {'.', '.', '1', '0'},
                {'1', '1', '1', '0'},
                {'0', '0', '0', '0'},
            });

            var game = new Game(mines);
            game.Uncover(4, 4);
            
            Assert.That(game.Board, Is.EqualTo(expectedBoard));
        }

        [Test]
        public void Uncover_GivenUncoverNextToMultipleMines_FieldWithCorrectNumber()
        {
            var mineCoords = new[]
            {
                (1, 2),
                (2, 1)
            };

            var mines = new bool[4, 4];
            foreach (var (col, row) in mineCoords)
            {
                mines[col - 1, row - 1] = true;
            }

            var game = new Game(mines);
            game.Uncover(2, 2);
            
            Assert.That(game.Board[2, 2], Is.EqualTo('2'));
        }

        [Test]
        public void FlagTile_GivenFlagTile_FlagOnBoard()
        {
            var mines = new bool[3, 3];
            var expectedBoard = CreateFilledBoard(3, 3, '.');
            expectedBoard[2, 2] = 'f';

            var game = new Game(mines);
            game.FlagTile(2, 2);
            
            Assert.That(game.Board, Is.EqualTo(expectedBoard));
        }
                
        [Test]
        public void FlagTile_GivenFlaggedTile_FlagRemoved()
        {
            var mines = new bool[3, 3];
            var expectedBoard = CreateFilledBoard(3, 3, '.');

            var game = new Game(mines);
            game.FlagTile(2, 2);
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

        [Test]
        public void Uncover_GivenBlockingFlags_DoNotUncoverAll()
        {
            var mines = new bool[4, 4];
            var expectedBoard = new Board(new[,]
            {
                {'.', '.', '.', '.'},
                {'f', 'f', 'f', 'f'},
                {'0', '0', '0', '0'},
                {'0', '0', '0', '0'},
            });

            var game = new Game(mines);
            game.FlagTile(2, 1);
            game.FlagTile(2, 2);
            game.FlagTile(2, 3);
            game.FlagTile(2, 4);
            game.Uncover(4, 4);
            
            Assert.That(game.Board, Is.EqualTo(expectedBoard));
        }

        [Test]
        public void GameState_GivenUnflaggedMines_InProgress()
        {
            var mines = new bool[4, 4];
            mines[3, 3] = true;

            var game = new Game(mines);
            Assert.That(game.State, Is.EqualTo(Game.GameState.InProgress));
            game.Uncover(1, 1);
            Assert.That(game.State, Is.EqualTo(Game.GameState.InProgress));
        }
        
        [Test]
        public void GameState_GivenUncoveredMine_Defeat()
        {
            var mines = new bool[4, 4];
            mines[1, 1] = true;

            var game = new Game(mines);
            game.Uncover(2, 2);
            
            Assert.That(game.State, Is.EqualTo(Game.GameState.Defeat));
        }
        
        [Test]
        public void GameState_GivenUncoveredMine_Victory()
        {
            var mines = new bool[4, 4];
            mines[1, 1] = true;

            var game = new Game(mines);
            game.FlagTile(2, 2);
            
            Assert.That(game.State, Is.EqualTo(Game.GameState.Victory));
        }

        [Test]
        public void GameState_GivenNotFlaggedMine_InProgress()
        {
            var mines = new bool[4, 4];
            mines[1, 1] = true;
            mines[2, 1] = true;

            var game = new Game(mines);
            game.FlagTile(2, 2);
            
            Assert.That(game.State, Is.EqualTo(Game.GameState.InProgress));
        }

        [Test]
        public void ComplexScenario_GivenWinningSequenceOfMoves_Win()
        {
            bool[,] mines =
            {
                {true, false, false, true},
                {true, false, false, false},
                {true, false, false, false},
            };
            Game game = new Game(mines);
            Board expectedBoard = CreateFilledBoard(3, 4, '.');
            Assert.That(game.Board, Is.EqualTo(expectedBoard));

            AssertUncoverResult(game,2,4, Game.GameState.InProgress, new [,]
            {
                {'.','.','.','.'},
                {'.','.','.','1'},
                {'.','.','.','.'},
            });

            AssertUncoverResult(game, 3,4, Game.GameState.InProgress, new [,]
            {
                {'.','.','.','.'},
                {'.','3','1','1'},
                {'.','2','0','0'},
            });

            AssertUncoverResult(game, 1, 2, Game.GameState.InProgress, new[,]
            {
                {'.','2','.','.'},
                {'.','3','1','1'},
                {'.','2','0','0'},
            });

            AssertFlagTileResult(game, 3, 1, Game.GameState.InProgress, new[,]
            {
                {'.','2','.','.'},
                {'.','3','1','1'},
                {'f','2','0','0'},
            });

            AssertFlagTileResult(game, 2, 1, Game.GameState.InProgress, new[,]
            {
                {'.','2','.','.'},
                {'f','3','1','1'},
                {'f','2','0','0'},
            });

            AssertFlagTileResult(game, 1, 1, Game.GameState.InProgress, new[,]
            {
                {'f','2','.','.'},
                {'f','3','1','1'},
                {'f','2','0','0'},
            });

            AssertFlagTileResult(game, 1, 4, Game.GameState.Victory, new[,]
            {
                {'f','2','.','f'},
                {'f','3','1','1'},
                {'f','2','0','0'},
            });
        }

        private static void AssertUncoverResult(Game game, int rowSelection, int colSelection, Game.GameState expectedState, char[,] expectedBoard)
        {
            game.Uncover(rowSelection, colSelection);
            Assert.That(game.Board, Is.EqualTo(new Board(expectedBoard)));
            Assert.That(game.State, Is.EqualTo(expectedState));
        }


        private static void AssertFlagTileResult(Game game, int rowSelection, int colSelection, Game.GameState expectedState, char[,] expectedBoard)
        {
            game.FlagTile(rowSelection, colSelection);
            Assert.That(game.Board, Is.EqualTo(new Board(expectedBoard)));
            Assert.That(game.State, Is.EqualTo(expectedState));
        }

        private static Board CreateFilledBoard(int numberOfRows, int numberOfColumns, char cellValue)
        {
            var board = new char[numberOfRows, numberOfColumns];
            for (var x = 0; x < numberOfRows; x++)
            {
                for (var y = 0; y < numberOfColumns; y++)
                {
                    board[x, y] = cellValue;
                }
            }
            return new Board(board);
        }
    }
}