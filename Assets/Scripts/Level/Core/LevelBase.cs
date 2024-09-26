using Core;
using Generators;
using Generators.Level;
using Generators.Level.Data;
using UnityEngine;

namespace Level.Core
{
    public class LevelBase : MonoBehaviour
    {
        [SerializeField] private LevelObjectGenerator objectGenerator;
        public string Name { get; private set; }
        public int IndexX { get; private set; }
        public uint IndexY { get; private set; }

        [HideInInspector] public LevelData staticData;

        public virtual CachedLevelData CachedData => new()
        {
            Objects = objectGenerator.CachedObjectsData,
            Name = Name,
            LevelID = staticData.id
        };

        public void Init(int indexX, uint indexY)
        {
            Name = LevelCache.GetKey(indexX, indexY);
            IndexX = indexX;
            IndexY = indexY;
            transform.position = new Vector3(Utils.Utils.IndexToX * indexX, Utils.Utils.IndexToY * indexY, 0);
            objectGenerator.Init();
        }

        public void Init(int indexX, uint indexY, CachedLevelData cache)
        {
            Init(indexX, indexY);
            UpdateData(cache);
        }

        public void AfterFirstInit()
        {
            objectGenerator.Generate();
        }

        private void UpdateData(CachedLevelData cached)
        {
            Name = cached.Name;
            objectGenerator.Generate(cached.Objects);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (Utils.Utils.IsPlayer(other.gameObject))
            {
                GameController.Instance.PlayerMovedToNewLevel.Invoke(IndexX, IndexY);
            }
        }
    }
}