using Player.Modifiers;
using Player.Modifiers.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerExperienceController : MonoBehaviour, IModifiable
    {
        [SerializeField] private AudioSource onLevelUpSound;
        public readonly UnityEvent<uint, float> OnLevelChanged = new();
        public readonly UnityEvent<float> OnXpChanged = new();
        private float _nextLevelXp = 100;
        private float _currentXp = 0;
        
        private float _expModifier = 1;

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
                _nextLevelXp = 100 + 1000 * Mathf.Log(1 + ++CurrentLevel / 10f);
            }
            onLevelUpSound.Play();
            OnLevelChanged.Invoke(CurrentLevel, _nextLevelXp);
        }

        public float GetActualExperience(float rawExperience)
        {
            return rawExperience * _expModifier;
        }

        public void Modify(ModifierType type)
        {
            if (type == ModifierType.ExperienceMultiplier)
                _expModifier += .25f;
        }
    }
}