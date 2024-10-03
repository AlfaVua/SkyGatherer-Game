using UnityEngine;
using UnityEngine.Events;

namespace Components.Component
{
    public class HealthComponent : MonoBehaviour
    {
        public readonly UnityEvent<float> OnHealthChanged = new();
        public readonly UnityEvent<float> OnMaxHealthChanged = new();
        public float CurrentHealth { get; private set; }
        private float _initialMaxHealth, _currentMaxHealth;

        public void Init(float maxHealth)
        {
            _currentMaxHealth = _initialMaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (damage == 0) return;
            OnHealthChanged.Invoke(CurrentHealth -= damage);
        }

        public void IncreaseMaxHealthByPercent(float maxHpModifier)
        {
            var newMaxHealth = _initialMaxHealth * maxHpModifier;
            var difference = newMaxHealth - _currentMaxHealth;
            _currentMaxHealth = newMaxHealth;
            OnMaxHealthChanged.Invoke(newMaxHealth);
            HealForValue(difference);
        }

        private void HealForValue(float healAmount)
        {
            if (healAmount == 0) return;
            CurrentHealth = Mathf.Min(_currentMaxHealth, CurrentHealth + healAmount);
            OnHealthChanged.Invoke(CurrentHealth);
        }

        public void HealForPercentage(float percentage)
        {
            HealForValue(percentage * _currentMaxHealth);
        }
    }
}