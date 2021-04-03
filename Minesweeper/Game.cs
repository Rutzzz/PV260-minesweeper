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
            if (!IsCovered(row, col)) return;

            var numberOfAdjacentMines = GetNumberOfAdjacentMines(row, col);
            Board[row, col] = char.Parse(numberOfAdjacentMines.ToString());

            if (numberOfAdjacentMines == 0)
               ForEachNeighbour(row, col, FloodUncover);
        }


        public void Uncover(int row, int col)
        {
            if (IsMine(row, col))
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
                if (IsMine(r, c))
                    sum += 1;
            });
            return sum;
        }

        public void FlagTile(int row, int col)
        {
            if (IsCovered(row, col))
            {
                Board[row, col] = 'f';

                CheckVictory();
            }
            else if (IsFlagged(row, col))
            {
                Board[row, col] = '.';
            }
        }

        private void CheckVictory()
        {
            if (!ExistsNonFlaggedMine())
                State = GameState.Victory;
        }

        private void ForEachNeighbour(int absoluteRow, int absoluteCol, Action<int, int> callback)
        {
            (int row, int col)[] neighbouringOffsets =
            {
                (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)
            };

            foreach (var (relativeRow, relativeCol) in neighbouringOffsets)
            {
                var row = absoluteRow + relativeRow;
                var col = absoluteCol + relativeCol;

                if (IsValidPosition(row, col))
                    callback(row, col);
            }
        }

        private bool IsValidPosition(int row, int col)
        {
            return row >= 1 && row <= Board.NumberOfRows && col >= 1 && col <= Board.NumberOfColumns;
        }


        private bool IsFlagged(int row, int col)
        {
            return Board[row, col] == 'f';
        }
        
        private bool IsMine(int row, int col)
        {
            return _mines[row - 1, col - 1];
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
                        return true;
                }
            }

            return false;
        }
    }
}