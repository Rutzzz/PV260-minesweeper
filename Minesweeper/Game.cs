using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper
{
    public class Game
    {
        private bool[,] _mines;
        private int NumberOfRows => Board.GetLength(0);
        private int NumberOfColumns => Board.GetLength(1);
        public char[,] Board { get; }
        public Game(bool[,] mines)
        {
            var nrow = mines.GetLength(0);
            var ncol = mines.GetLength(1);

            if (nrow < 3 || nrow > 50 || ncol < 3 || ncol > 50)
                throw new ArgumentException("Invalid mines array dimension");
            
            Board = new char[nrow, ncol];
            for (var r = 0; r < nrow; r++)
            {
                for (var c = 0; c < ncol; c++)
                {
                    Board[r, c] = '.';
                }
            }
            _mines = mines;
        }

        public void Uncover(int row, int col)
        {
            Board[row - 1, col - 1] = char.Parse(ComputeAdjacentMines(row, col).ToString());
        }

        private int ComputeAdjacentMines(int row, int col)
        {
            (int ro, int co)[] offsets =
            {
                (-1,-1),(-1,0),(-1,1),(0,-1),(0,1),(1,-1),(1,0),(1,1)
            };
            return offsets
                .Select(t => (row + t.ro - 1, col + t.co - 1))
                .Where(t => t.Item1 >= 0 && t.Item1 < NumberOfRows && t.Item2 >= 0 && t.Item2 < NumberOfColumns)
                .Count(t => _mines[t.Item1, t.Item2]);
        }
    }
}
