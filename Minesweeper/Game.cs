using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper
{
    public class Game
    {

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
        }
    }
}
