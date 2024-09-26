using Generators.Level;
using Generators.Level.Data;
using UnityEngine;

namespace Core
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Transform levelContainer;
        [SerializeField] private LevelData startingLevel;
        [SerializeField] private LevelsList levels;
        
        private LevelGenerator _generator;
        private LevelDataManager _manager;

        public void Init()
        {
            _manager = new LevelDataManager(levels.levels, startingLevel);
            _generator = new LevelGenerator(_manager);
        }

        public void GenerateStartingLevel()
        {
            var level = startingLevel.GetNewInstance();
            level.Init(0, 0);
            level.transform.SetParent(levelContainer);
            _generator.AddStartingLevel(level);
            GenerateAround(0, 0);
        }

        private void GenerateLevelAt(int indexX, uint indexY)
        {
            var level = _generator.GetLevel(indexX, indexY);
            if (!level) return;
            level.transform.SetParent(levelContainer);
            level.Init(indexX, indexY);
        }

        public void SetNewActiveCoords(int indexX, uint indexY)
        {
            GenerateLevelAt(indexX, indexY);
            GenerateAround(indexX, indexY);
            _generator.RemoveFarLevels(indexX, indexY);
        }

        private void GenerateAround(int indexX, uint indexY)
        {
            GenerateLevelAt(indexX + 1, indexY);
            GenerateLevelAt(indexX - 1, indexY);
            GenerateLevelAt(indexX, indexY + 1);
            if (indexY != 0) GenerateLevelAt(indexX, indexY - 1);
        }
    }
}