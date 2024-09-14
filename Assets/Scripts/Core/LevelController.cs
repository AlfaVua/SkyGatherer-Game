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

        public void Init(GameController mainController)
        {
            _generator = new LevelGenerator(levelContainer, generatorBehavior);
            _cache = new LevelCache();
            mainController.PlayerMovedToNewLevel.AddListener(GenerateLevelAt);
        }

        public void GenerateInit()
        {
            GenerateLevelsAround(_generator.GetStartingLevel());
        }

        private void GenerateLevelAt(int indexX, uint indexY)
        {
            _generator.GetRemovableLevels(indexX, indexY).ForEach(RemoveLevel);
            var level = GetLevel(indexX, indexY, LevelType.LeftExit);
            GenerateLevelsAround(level);
        }

        private void GenerateLevelsAround(LevelBase level)
        {
            var indexX = level.IndexX;
            var indexY = level.IndexY;
            
            if (level.LeftExit) GetLevel(indexX - 1, indexY, LevelType.RightExit);
            if (level.TopExit) GetLevel(indexX, indexY + 1, LevelType.BottomExit);
            if (level.RightExit) GetLevel(indexX + 1, indexY, LevelType.LeftExit);
            if (level.BottomExit) GetLevel(indexX, indexY - 1, LevelType.TopExit);
        }

        private LevelBase GetLevel(int indexX, uint indexY, LevelType type)
        {
            return _generator.GetActiveLevelAt(indexX, indexY) ?? (
                _cache.HasLevel(indexX, indexY) ? 
                _generator.GetLevelAt(indexX, indexY, _cache.GetLevelData(indexX, indexY)) :
                _generator.GetLevelAt(indexX, indexY, type));
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