using Generators;
using Generators.Level;
using Level.Core;
using UnityEngine;

namespace Core
{
    using Utils.TransformExtenstion;
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Transform levelContainer;
        [SerializeField] private LevelGeneratorData generatorBehavior;
        
        private LevelGenerator _generator;
        private LevelCache _cache;

        private int activeLevelIndexX;
        private uint activeLevelIndexY = 999;

        public void Init(GameController mainController)
        {
            _generator = new LevelGenerator(levelContainer, generatorBehavior);
            _cache = new LevelCache();
        }

        public void GenerateInit()
        {
            _generator.GenerateStartingLevel();
        }

        public void GenerateLevelAt(int indexX, uint indexY)
        {
            if (activeLevelIndexX == indexX && activeLevelIndexY == indexY) return;
            _generator.GetRemovableLevels(indexX, indexY).ForEach(RemoveLevel);
            var level = GetLevel(indexX, indexY, LevelType.LeftExit);
            GenerateLevelsAround(level);
        }

        private void GenerateLevelsAround(LevelBase level)
        {
            activeLevelIndexX = level.IndexX;
            activeLevelIndexY = level.IndexY;
            var indexX = level.IndexX;
            var indexY = level.IndexY;
            
            if (level.LeftExit) GetLevel(indexX - 1, indexY, LevelType.RightExit);
            if (level.TopExit) GetLevel(indexX, indexY + 1, LevelType.BottomExit);
            if (level.RightExit) GetLevel(indexX + 1, indexY, LevelType.LeftExit);
            if (level.BottomExit) GetLevel(indexX, indexY - 1, LevelType.TopExit);
        }

        private LevelBase GetLevel(int indexX, uint indexY, LevelType type)
        {
            if (_generator.IsCoordsActive(indexX, indexY)) return _generator.GetActiveLevelAt(indexX, indexY);
            return _cache.HasLevel(indexX, indexY) ? 
                _generator.GetLevelAt(indexX, indexY, _cache.GetLevelData(indexX, indexY)) :
                _generator.GetLevelAt(indexX, indexY, type);
        }

        private void RemoveLevel(LevelBase level)
        {
            _generator.RemoveLevel(level);
            _cache.SaveLevel(level);
            Destroy(level.gameObject);
        }

        public void Clear()
        {
            levelContainer.ClearChildren();
            _cache.ClearCache();
            _generator.Clear();
        }
    }
}