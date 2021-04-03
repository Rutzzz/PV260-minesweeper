using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper
{
    public class Board
    {
        private readonly char[,] _board;

        public Board(char[,] board)
        {
            _board = board;
        }

        public Board(int numberOfRows, int numberOfColumns)
        {
            _board = new char[numberOfRows, numberOfColumns];
            for (var r = 0; r < numberOfRows; r++)
            {
                for (var c = 0; c < numberOfColumns; c++)
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
            
            for (var row = 1; row <= NumberOfRows; row++)
            {
                for (var col = 1; col <= NumberOfColumns; col++)
                {
                    if (this[row, col] != otherBoard[row, col]) return false;
                }
            }
            return true;
        }
    }
}
