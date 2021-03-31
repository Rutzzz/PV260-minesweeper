using System;

namespace Minesweeper
{
    public static class BoardGenerator
    {
        public static bool[,] Generate(IMinePlacementGenerator generator, int numberOfRows, int numberOfColumns, float density)
        {
            if (numberOfRows < 1 || numberOfColumns < 1)
                throw new ArgumentException("Invalid dimensions given.");

            if (density < 0.2 || density > 0.6)
                throw new ArgumentException("Invalid mine density given.");
            
            var retval = new bool[numberOfRows,numberOfColumns];
            int mines = Convert.ToInt32(numberOfRows * numberOfColumns * density);
            
            while (mines > 0)
            {
                var coord = generator.Next();
                if (retval[coord.Item1, coord.Item2])
                    continue;
                retval[coord.Item1,coord.Item2] = true;
                mines--;
            }

            return retval;
        }
    }
}