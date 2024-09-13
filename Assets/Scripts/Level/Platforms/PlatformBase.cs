using UnityEngine;

namespace Level.Platforms
{
    public class PlatformBase : MonoBehaviour
    {

        public virtual CachedPlatformData CachedData => new CachedPlatformData()
        {
        };

        public void Init()
        {
        }

        public void UpdateData(CachedPlatformData data)
        {
        }
    }
}