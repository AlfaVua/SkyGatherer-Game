using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerExperienceController : MonoBehaviour
    {
        public readonly UnityEvent<uint, float> OnLevelChanged = new();
        public readonly UnityEvent<float> OnXpChanged = new();
        private uint _currentLevel = 1;
        private float _nextLevelXp = 100;
        private float _currentXp = 0;
        
        [HideInInspector] public float expModifier = 1;

        public void Init()
        {
        }

        public void AddExperience(float amount)
        {
            _currentXp += amount * expModifier;
            TryLevelUp();
            OnXpChanged.Invoke(_currentXp);
        }

        private void TryLevelUp()
        {
            if (_currentXp < _nextLevelXp) return;
            while (_currentXp >= _nextLevelXp)
            {
                _currentXp -= _nextLevelXp;
                _nextLevelXp *= 1.5f;
                _currentLevel++;
            }
            OnLevelChanged.Invoke(_currentLevel, _nextLevelXp);
        }
    }
}