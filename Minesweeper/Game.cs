using System;

namespace Minesweeper
{
    public class Game
    {
        private readonly bool[,] _mines;
        public Board Board { get; }

        public enum GameState
        {
            InProgress,
            Defeat,
            Victory,
        }

        public GameState State = GameState.InProgress;

        public Game(bool[,] mines)
        {
            var numberOfRows = mines.GetLength(0);
            var numberOfColumns = mines.GetLength(1);

            if (numberOfRows < 3 || numberOfRows > 50 || numberOfColumns < 3 || numberOfColumns > 50)
                throw new ArgumentException("Invalid mines array dimension");

            Board = new Board(numberOfRows, numberOfColumns);
            _mines = mines;
        }

        private void FloodUncover(int row, int col)
        {
            if (!IsCovered(row, col))
            {
                return;
            }

            var numberOfAdjacentMines = GetNumberOfAdjacentMines(row, col);
            Board[row, col] = char.Parse(numberOfAdjacentMines.ToString());

            if (numberOfAdjacentMines != 0)
            {
                return;
            }

            ForEachNeighbour(row, col, FloodUncover);
        }


        public void Uncover(int row, int col)
        {
            if (IsMine(row - 1, col - 1))
            {
                State = GameState.Defeat;
                return;
            }
            
            FloodUncover(row, col);
        }


        private int GetNumberOfAdjacentMines(int row, int col)
        {
            var sum = 0;
            ForEachNeighbour(row, col, (r, c) =>
            {
                if (IsMine(x, y))
                {
                    sum += 1;
                }
            });
            return sum;
        }

        public void FlagTile(int row, int col)
        {
            if (IsCovered(row, col))
            {
                Board[row, col] = 'f';

                if (!ExistsNonFlaggedMine())
                {
                    State = GameState.Victory;
                }
            }
            else if (IsFlagged(row, col))
            {
                Board[row, col] = '.';
            }
        }

        private void ForEachNeighbour(int row, int col, Action<int, int> callback)
        {
            (int x, int y)[] neighbouringOffsets =
            {
                (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)
            };

            foreach (var (relativeX, relativeY) in neighbouringOffsets)
            {
                var x = row + relativeX;
                var y = col + relativeY;

                if (x <= 0 || x > Board.NumberOfRows || y <= 0 || y > Board.NumberOfColumns)
                {
                    continue;
                }

                callback(x, y);
            }
        }
        

        private bool IsFlagged(int row, int col)
        {
            return Board[row, col] == 'f';
        }
        
        private bool IsMine(int x, int y)
        {
            return _mines[x, y];
        }
        
        private bool IsCovered(int row, int col)
        {
            return Board[row, col] == '.';
        }

        private bool ExistsNonFlaggedMine()
        {
            for (var row = 1; row <= Board.NumberOfRows; row++)
            {
                for (var col = 1; col <= Board.NumberOfColumns; col++)
                {
                    if (!IsFlagged(row, col) && IsMine(row, col))
                    {
                        return true;
                    }        
                }
            }

            return false;
        }
    }
}