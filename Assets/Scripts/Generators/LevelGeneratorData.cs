using System.Collections.Generic;
using Level.Core;
using UnityEngine;

namespace Generators
{
    using Utils.ListExtension;
    public enum LevelType
    {
        LeftExit,
        RightExit,
        TopExit,
        BottomExit
    }
    [CreateAssetMenu(menuName = "Level Generation/Base")]
    public class LevelGeneratorData : ScriptableObject
    {
        public List<LevelBase> prefabs;

        private List<LevelBase> _leftExitLevels;
        private List<LevelBase> _rightExitLevels;
        private List<LevelBase> _bottomExitLevels;
        private List<LevelBase> _topExitLevels;
        
        public void Init()
        {
            _leftExitLevels = new List<LevelBase>();
            _rightExitLevels = new List<LevelBase>();
            _bottomExitLevels = new List<LevelBase>();
            _topExitLevels = new List<LevelBase>();
            UpdatePrefabs();
        }

        public void Clear()
        {
            _leftExitLevels.Clear();
            _rightExitLevels.Clear();
            _bottomExitLevels.Clear();
            _topExitLevels.Clear();
        }

        private void UpdatePrefabs()
        {
            var i = 0;
            prefabs.ForEach(levelBase =>
            {
                levelBase.staticID = i++;
                if (levelBase.LeftExit) _leftExitLevels.Add(levelBase);
                if (levelBase.RightExit) _rightExitLevels.Add(levelBase);
                if (levelBase.BottomExit) _bottomExitLevels.Add(levelBase);
                if (levelBase.TopExit) _topExitLevels.Add(levelBase);
            });
        }

        public LevelBase GetById(int id)
        {
            return prefabs[id];
        }

        public LevelBase GetRandom(bool isBottom, LevelType type)
        {
            return GetRandom(isBottom, type switch
            {
                LevelType.LeftExit => _leftExitLevels,
                LevelType.TopExit => _topExitLevels,
                LevelType.RightExit => _rightExitLevels,
                _ => _bottomExitLevels
            });
        }

        private LevelBase GetRandom(bool isBottom, IReadOnlyList<LevelBase> takeFrom)
        {
            var array = new List<LevelBase>(takeFrom);
            array.Shuffle();
            return array.Find(level => !(isBottom && level.BottomExit));
        }
    }
}