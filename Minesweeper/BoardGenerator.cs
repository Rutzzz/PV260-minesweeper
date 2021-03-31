using System;

namespace Minesweeper
{
    public static class BoardGenerator
    {
        public static bool[,] Generate(int numberOfRows, int numberOfColumns, float density)
        {
            if (numberOfRows < 1 || numberOfColumns < 1)
                throw new ArgumentException("Invalid dimensions given.");

            if (density < 0.2 || density > 0.6)
                throw new ArgumentException("Invalid mine density given.");
            
            return null;
        }
    }
}