using System.Collections.Generic;
using Generators.Resource;
using Level.Resource;
using UnityEngine;

namespace Generators.Level
{
    [CreateAssetMenu(menuName = "Level/Object Data")]
    public class LevelObjectData : ScriptableObject
    {
        [SerializeField] private List<ResourceData> drops;
        [SerializeField] private LevelObjectBase prefab;
        [SerializeField] private Sprite view;
        [SerializeField] private Rarity rarity;

        public Rarity Rarity => rarity;
        public Sprite View => view;

        public LevelObjectBase GetObjectInstance(Transform parent)
        {
            var instance = Instantiate(prefab, parent);
            instance.Init(this);
            return instance;
        }
    }
}