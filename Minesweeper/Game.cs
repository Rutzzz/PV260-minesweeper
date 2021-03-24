using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper
{
    public class Game
    {
        public Game(bool[,] mines)
        {
            if (mines.GetLength(0) < 3 || mines.GetLength(0) > 50 || mines.GetLength(1) < 3 || mines.GetLength(1) > 50)
                throw new ArgumentException("Invalid mines array dimension");
        }
    }
}
