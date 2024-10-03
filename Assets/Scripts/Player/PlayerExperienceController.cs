using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerExperienceController : MonoBehaviour
    {
        public readonly UnityEvent<uint, float> OnLevelChanged = new();
        public readonly UnityEvent<float> OnXpChanged = new();
        private float _nextLevelXp = 100;
        private float _currentXp = 0;
        
        [HideInInspector] public float expModifier = 1;

        public uint CurrentLevel { get; private set; } = 1;

        public void Init()
        {
        }

        public void AddExperience(float amount)
        {
            _currentXp += GetActualExperience(amount);
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
                CurrentLevel++;
            }
            OnLevelChanged.Invoke(CurrentLevel, _nextLevelXp);
        }

        public float GetActualExperience(float rawExperience)
        {
            return rawExperience * expModifier;
        }
    }
}