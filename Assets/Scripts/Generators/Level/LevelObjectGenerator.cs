using System.Collections.Generic;
using System.Linq;
using Level.Core;
using Level.Resource;
using UnityEngine;

namespace Generators.Level
{
    public class LevelObjectGenerator : MonoBehaviour
    {
        [SerializeField] private LevelObjectGeneratorConfig config;
        [SerializeField] private List<Transform> objectPlaces;

        private List<LevelObjectBase> _objects;

        public List<CachedLevelObjectData> CachedObjectsData => _objects?.Select(obj => obj.CachedData).ToList();
        
        public void Init()
        {
            if (objectPlaces.Count == 0) return;
            _objects = new List<LevelObjectBase>();
            config.Init();
        }

        public void Generate()
        {
            var i = -1;
            objectPlaces.ForEach(place =>
            {
                i++;
                if (!config.RollSpawnChance()) return;
                var data = config.GetRandomObject();
                var instance = data.GetObjectInstance(place);
                instance.placeIndex = i;
                _objects.Add(instance);
            });
        }

        public void Generate(List<CachedLevelObjectData> objects)
        {
            objects?.ForEach(obj =>
            {
                var objectData = config.GetObjectById(obj.SessionID);
                var instance = objectData.GetObjectInstance(objectPlaces[obj.PlaceIndex]);
                instance.UpdateData(obj);
                _objects.Add(instance);
            });
        }
    }
}