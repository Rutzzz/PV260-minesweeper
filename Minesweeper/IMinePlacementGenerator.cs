using System;

namespace Minesweeper
{
    public interface IMinePlacementGenerator
    {
        public Tuple<int,int> Next();
    }
}