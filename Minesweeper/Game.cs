using System;

namespace Minesweeper
{
    public class Game
    {
        private readonly bool[,] _mines;
        private int NumberOfRows => Board.GetLength(0);
        private int NumberOfColumns => Board.GetLength(1);
        public char[,] Board { get; }

        public Game(bool[,] mines)
        {
            var numberOfRows = mines.GetLength(0);
            var numberOfColumns = mines.GetLength(1);

            if (numberOfRows < 3 || numberOfRows > 50 || numberOfColumns < 3 || numberOfColumns > 50)
                throw new ArgumentException("Invalid mines array dimension");

            Board = new char[numberOfRows, numberOfColumns];
            for (var x = 0; x < numberOfRows; x++)
            {
                for (var y = 0; y < numberOfColumns; y++)
                {
                    Board[x, y] = '.';
                }
            }

            _mines = mines;
        }

        private void FloodUncover(int absoluteX, int absoluteY)
        {
            if (!IsCovered(absoluteX, absoluteY))
            {
                return;
            }
            
            var numberOfAdjacentMines = GetNumberOfAdjacentMines(absoluteX, absoluteY);
            Board[absoluteX, absoluteY] = char.Parse(numberOfAdjacentMines.ToString());

            if (numberOfAdjacentMines != 0)
            {
                return;
            }

            ForEachNeighbour(absoluteX, absoluteY, FloodUncover);
        }

        private bool IsCovered(int absoluteX, int absoluteY)
        {
            return Board[absoluteX, absoluteY] == '.';
        }

        public void Uncover(int row, int col)
        {
            FloodUncover(row - 1, col - 1);
        }

        private int GetNumberOfAdjacentMines(int absoluteX, int absoluteY)
        {
            var sum = 0;
            ForEachNeighbour(absoluteX, absoluteY, (x, y) =>
            {
                if (_mines[x, y])
                {
                    sum += 1;
                }
            });
            return sum;
        }

        private void ForEachNeighbour(int absoluteX, int absoluteY, Action<int, int> callback)
        {
            (int x, int y)[] neighbouringOffsets =
            {
                (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)
            };

            foreach (var (relativeX, relativeY) in neighbouringOffsets)
            {
                var x = absoluteX + relativeX;
                var y = absoluteY + relativeY;

                if (x < 0 || x >= NumberOfRows || y < 0 || y >= NumberOfColumns)
                {
                    continue;
                }

                callback(x, y);
            }
        }
    }
}