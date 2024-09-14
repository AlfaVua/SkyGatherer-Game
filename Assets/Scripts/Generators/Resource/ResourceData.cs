using Level.Resource;
using UnityEngine;

namespace Generators.Resource
{
    [CreateAssetMenu(menuName = "Resource/Base")]
    public class ResourceData : ScriptableObject
    {
        [SerializeField] private Sprite resourceIcon;
        [HideInInspector] public uint staticID;
        public Rarity rarity;

        public Sprite Icon => resourceIcon;
    }
}