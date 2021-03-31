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
            
            var retval = new bool[numberOfRows,numberOfColumns];
            int mines = Convert.ToInt32(numberOfRows * numberOfColumns * density);

            for (int i = 0; i < numberOfRows; ++i)
            {
                for (int j = 0; j < numberOfColumns; ++j)
                {
                    if (mines-- <= 0)
                        return retval;
                    retval[i, j] = true;
                }
            }

            return retval;
        }
    }
}