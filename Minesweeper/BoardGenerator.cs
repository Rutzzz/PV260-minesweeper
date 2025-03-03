using System;
using System.Collections;

namespace Minesweeper
{
    public static class BoardGenerator
    {
        private const double MinimumDensity = 0.2;
        private const double MaximumDensity = 0.6;


        public static bool[,] Generate(
            int numberOfRows, int numberOfColumns,
            float density
        ) {
            if (numberOfRows < 1 || numberOfColumns < 1)
                throw new ArgumentException("Invalid dimensions given.");

            if (density < MinimumDensity || density > MaximumDensity)
                throw new ArgumentException("Invalid mine density given.");


            var generator = new Random();
            var mineBoard = new bool[numberOfRows, numberOfColumns];
            var remainingMines = Convert.ToInt32(numberOfRows * numberOfColumns * density);

            while (remainingMines > 0)
            {
                var x = generator.Next(0, numberOfColumns);
                var y = generator.Next(0, numberOfRows);

                if (mineBoard[x, y])
                    continue;

                mineBoard[x, y] = true;
                remainingMines--;
            }

            return mineBoard;
        }
    }
}