using System.Collections.Generic;
using System.Linq;
using Core;
using Generators;
using Level.Platforms;
using UnityEngine;

namespace Level.Core
{
    public class LevelBase : MonoBehaviour
    {
        [SerializeField] private LevelSidesData sidesData;
        [SerializeField] private List<PlatformBase> platforms;
        public string Name { get; private set; }
        public int IndexX { get; private set; }
        public uint IndexY { get; private set; }

        [HideInInspector] public int staticID;

        public virtual CachedLevelData CachedData => new()
        {
            platforms = platforms.Select(platform => platform.CachedData).ToList(),
            name = Name,
            id = staticID
        };

        public void Init(int indexX, uint indexY)
        {
            Name = LevelCache.GetKey(indexX, indexY);
            IndexX = indexX;
            IndexY = indexY;
            transform.position = new Vector3(Utils.Utils.IndexToX * indexX, Utils.Utils.IndexToY * indexY, 0);
        }

        public void UpdateData(CachedLevelData cached)
        {
            Name = cached.name;
            for (var i = 0; i < cached.platforms.Count; i++)
            {
                platforms[i].UpdateData(cached.platforms[i]);
            }
        }

        public bool LeftExit => sidesData.leftExit;
        public bool RightExit => sidesData.rightExit;
        public bool TopExit => sidesData.topExit;
        public bool BottomExit => sidesData.bottomExit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameController.Instance.PlayerMovedToNewLevel.Invoke(IndexX, IndexY);
        }
    }
}