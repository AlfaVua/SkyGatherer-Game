using Generators.Level;
using Level.Core;
using UnityEngine;

namespace Level.Resource
{
    public class LevelObjectBase : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer view;

        private LevelObjectData _static;
        
        [HideInInspector] public int placeIndex;

        public CachedLevelObjectData CachedData => new()
        {
            PlaceIndex = placeIndex,
            SessionID = _static.GetInstanceID()
        };
        public void Init(LevelObjectData data)
        {
            _static = data;
            view.sprite = data.View;
            view.transform.position += Vector3.up * view.bounds.size.y / 2;
        }

        public void UpdateData(CachedLevelObjectData data)
        {
            placeIndex = data.PlaceIndex;
        }
    }
}