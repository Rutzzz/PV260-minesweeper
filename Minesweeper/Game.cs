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
            if (mines.GetLength(0) < 3 || mines.GetLength(0) > 50 || mines.GetLength(1) < 3 || mines.GetLength(1) > 50)
                throw new ArgumentException("Invalid mines array dimension");
            Board = new char[mines.GetLength(0), mines.GetLength(1)];
            for (int r = 0; r < mines.GetLength(0); r++)
            {
                for (int c = 0; c < mines.GetLength(1); c++)
                {
                    Board[r, c] = '.';
                }
            }
        }
    }
}
