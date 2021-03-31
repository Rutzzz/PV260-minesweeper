using System;

namespace Minesweeper
{
    public class RandomMinePlacementGenerator : IMinePlacementGenerator
    {
        private int _rows;
        private int _cols;
        private Random _rng;
        public RandomMinePlacementGenerator(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
            _rng = new Random();
        }
        public Tuple<int,int> Next()
        {
            return new Tuple<int,int>(_rng.Next(0, _rows), _rng.Next(0, _cols));
        }
    }
}