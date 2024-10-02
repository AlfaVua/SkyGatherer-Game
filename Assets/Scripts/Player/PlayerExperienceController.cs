using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerExperienceController : MonoBehaviour
    {
        public readonly UnityEvent<uint, float> OnLevelChanged = new();
        public readonly UnityEvent<float> OnXpChanged = new();
        private uint _currentLevel = 1;
        private float _nextLevelXp = 100;
        private float _currentXp = 0;

        public void Init()
        {
        }

        public void AddExperience(float amount)
        {
            _currentXp += amount;
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