using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public class Mod2MineGenerator : IMinePlacementGenerator
    {
        private int _returned;
        private List<Tuple<int, int>> _coords;

        public Mod2MineGenerator(int rows, int cols)
        {
            _coords = new List<Tuple<int, int>>();
            _returned = 0;
            for (int row = 0, counter = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++, counter++)
                {
                    if (counter % 2 == 0)
                        continue;
                    _coords.Add(new Tuple<int, int>(row, col));
                }
            }
        }

        public List<Tuple<int, int>> MineCoordinates
        {
            get => _coords;
        }

        public Tuple<int, int> Next()
        {
            return (_returned >= _coords.Count) ? null : _coords[_returned++];
        }
    }
}