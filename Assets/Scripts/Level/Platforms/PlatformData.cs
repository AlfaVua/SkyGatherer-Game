using UnityEngine;

namespace Level.Platforms
{
    [CreateAssetMenu(menuName = "Platform/Base")]
    public class PlatformData : ScriptableObject
    {
        [SerializeField] private PlatformBase prefab;

        public PlatformBase Prefab => prefab;
    }
}