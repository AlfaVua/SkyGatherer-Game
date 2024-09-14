using Core;
using Generators;
using Generators.Level;
using UnityEngine;

namespace Level.Core
{
    public class LevelBase : MonoBehaviour
    {
        [SerializeField] private LevelSidesData sidesData;
        [SerializeField] private LevelObjectGenerator objectGenerator;
        public string Name { get; private set; }
        public int IndexX { get; private set; }
        public uint IndexY { get; private set; }

        [HideInInspector] public int sessionID;

        public virtual CachedLevelData CachedData => new()
        {
            Objects = objectGenerator.CachedObjectsData,
            name = Name,
            sessionID = sessionID
        };

        public void Init(int indexX, uint indexY)
        {
            Name = LevelCache.GetKey(indexX, indexY);
            IndexX = indexX;
            IndexY = indexY;
            transform.position = new Vector3(Utils.Utils.IndexToX * indexX, Utils.Utils.IndexToY * indexY, 0);
            objectGenerator.Init();
        }

        public void AfterFirstInit()
        {
            objectGenerator.Generate();
        }

        public void UpdateData(CachedLevelData cached)
        {
            Name = cached.name;
            objectGenerator.Generate(cached.Objects);
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