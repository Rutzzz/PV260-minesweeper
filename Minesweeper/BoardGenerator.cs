using System;

namespace Minesweeper
{
    public static class BoardGenerator
    {
        public static bool[,] Generate(int numberOfRows, int numberOfColumns, float density)
        {
            if (numberOfRows < 1 || numberOfColumns < 1)
                throw new ArgumentException("Invalid dimensions given.");
            return null;
        }
    }
}