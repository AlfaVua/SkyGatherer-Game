using System.Collections.Generic;

namespace Generators.Level.Data
{
    public class LevelRelationData
    {
        private List<int> _left;
        private List<int> _right;
        private List<int> _top;
        private List<int> _bottom;

        public List<int> Left
        {
            get => _left ??= new List<int>();
            set => _left = value;
        }

        public List<int> Right
        {
            get => _right ??= new List<int>();
            set => _right = value;
        }

        public List<int> Top
        {
            get => _top ??= new List<int>();
            set => _top = value;
        }

        public List<int> Bottom
        {
            get => _bottom ??= new List<int>();
            set => _bottom = value;
        }

        public List<int> GetNeighbors(LevelSide side)
        {
            return side switch
            {
                LevelSide.Left => Left,
                LevelSide.Right => Right,
                LevelSide.Top => Top,
                LevelSide.Bottom => Bottom,
                _ => null
            };
        }
    }
}