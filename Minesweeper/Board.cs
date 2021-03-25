using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper
{
    public class Board
    {
        private char[,] _board;

        public Board(char[,] board)
        {
            _board = board;
        }

        public Board(int nrow, int ncol)
        {
            _board = new char[nrow, ncol];
            for (int r = 0; r < nrow; r++)
            {
                for (int c = 0; c < ncol; c++)
                {
                    _board[r, c] ='.';
                }
            }
        }

        public int NumberOfRows => _board.GetLength(0);
        public int NumberOfColumns => _board.GetLength(1);


        public char this[int row, int col]
        {
            get => _board[row - 1, col - 1];
            set => _board[row - 1, col - 1] = value;
        }

        public override bool Equals(object? other)
        {
            var otherBoard = other as Board;
            if (otherBoard == null) return false;
            if (NumberOfRows != otherBoard.NumberOfRows || NumberOfColumns != otherBoard.NumberOfColumns) return false;
            for (int r = 1; r <= NumberOfRows; r++)
            {
                for (int c = 1; c <= NumberOfColumns; c++)
                {
                    if (this[r, c] != otherBoard[r, c]) return false;
                }
            }
            return true;
        }
    }
}
