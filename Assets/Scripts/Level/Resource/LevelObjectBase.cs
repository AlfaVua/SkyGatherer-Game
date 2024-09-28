using Components.Component;
using Generators.Level;
using Level.Core;
using UnityEngine;

namespace Level.Resource
{
    public class LevelObjectBase : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer view;
        [SerializeField] private InteractableObject interactionTarget;

        private LevelObjectData _static;
        private bool _isCollected;
        
        [HideInInspector] public int placeIndex;

        public CachedLevelObjectData CachedData => new()
        {
            PlaceIndex = placeIndex,
            SessionID = _static.GetInstanceID(),
            IsCollected = _isCollected
        };
        public void Init(LevelObjectData data)
        {
            _static = data;
            UpdateView();
        }

        public void UpdateData(CachedLevelObjectData data)
        {
            placeIndex = data.PlaceIndex;
            _isCollected = data.IsCollected;
            UpdateView();
        }

        private void UpdateView()
        {
            view.sprite = _isCollected ? _static.CollectedView : _static.View;
        }

        private void OnPlayerInteractionStarted()
        {
        }
        
        private void OnPlayerInteractionEnded()
        {
            _isCollected = true;
            UpdateView();
        }

        private void OnEnable()
        {
            interactionTarget?.OnInteractionStarted.AddListener(OnPlayerInteractionStarted);
            interactionTarget?.OnInteractionEnded.AddListener(OnPlayerInteractionEnded);
        }
        
        private void OnDisable()
        {
            interactionTarget?.OnInteractionStarted.RemoveListener(OnPlayerInteractionStarted);
            interactionTarget?.OnInteractionEnded.RemoveListener(OnPlayerInteractionEnded);
        }
    }
}