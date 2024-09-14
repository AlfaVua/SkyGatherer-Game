using System.Collections.Generic;
using Level.Resource;
using UnityEngine;
using Utils.ListExtension;
using Random = UnityEngine.Random;

namespace Generators.Level
{
    [CreateAssetMenu(menuName = "Level/Object Generator Config")]
    public class LevelObjectGeneratorConfig : ScriptableObject
    {
        [SerializeField] private List<LevelObjectData> objects;
        [SerializeField] [Range(0, 1)] private float spawnChance = .5f;
        public RarityConfig rarityConfig;

        private Dictionary<Rarity, List<LevelObjectData>> _objectsByRarity;

        public void Init()
        {
            if (_objectsByRarity != null && _objectsByRarity.Count != 0) return;
            _objectsByRarity = new Dictionary<Rarity, List<LevelObjectData>>();
            objects.ForEach(obj =>
            {
                var rarity = obj.Rarity;
                if (!_objectsByRarity.ContainsKey(rarity)) _objectsByRarity.Add(rarity, new List<LevelObjectData>());
                _objectsByRarity[rarity].Add(obj);
            });
        }

        public LevelObjectData GetObjectById(int id)
        {
            return objects.Find(resource => resource.GetInstanceID() == id);
        }

        public LevelObjectData GetRandomObject()
        {
            return _objectsByRarity[rarityConfig.GetRandomRarity()].GetRandom();
        }

        public bool RollSpawnChance()
        {
            return Random.value < spawnChance;
        }
    }
}