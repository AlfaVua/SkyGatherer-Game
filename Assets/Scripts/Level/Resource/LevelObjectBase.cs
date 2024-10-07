using System;
using Components.Component;
using Core;
using Generators.Level;
using Level.Core;
using UnityEngine;

namespace Level.Resource
{
    public class LevelObjectBase : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer view;
        [SerializeField] private InteractableObject interactionTarget;
        [SerializeField] private ParticleSystem collectEffect;
        [SerializeField] private PlaySoundRandomPitch collectSound;

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
            if (_isCollected) interactionTarget.gameObject.SetActive(false);
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
            Collect();
        }

        private void Collect()
        {
            interactionTarget.gameObject.SetActive(false);
            collectSound?.Play();
            _isCollected = true;
            UpdateView();
            collectEffect.Emit(20);
            GameController.Instance.Player.OnCollectResource(_static);
        }

        private void OnEnable()
        {
            if (!interactionTarget) return;
            interactionTarget.OnInteractionStarted.AddListener(OnPlayerInteractionStarted);
            interactionTarget.OnInteractionEnded.AddListener(OnPlayerInteractionEnded);
        }
        
        private void OnDisable()
        {
            if (!interactionTarget) return;
            interactionTarget.OnInteractionStarted.RemoveListener(OnPlayerInteractionStarted);
            interactionTarget.OnInteractionEnded.RemoveListener(OnPlayerInteractionEnded);
        }
    }
}