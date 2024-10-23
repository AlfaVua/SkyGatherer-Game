using System;
using Player.Modifiers;
using Player.Modifiers.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Player.Components
{
    public class HealthComponent : MonoBehaviour, IModifiable
    {
        public readonly UnityEvent<float> OnHealthChanged = new();
        public readonly UnityEvent<float> OnMaxHealthChanged = new();
        public float CurrentHealth { get; private set; }
        private float _initialMaxHealth, _currentMaxHealth;
        
        private float _maxHealthMultiplier = 1;

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

        private void IncreaseMaxHealthByPercent(float percent)
        {
            _maxHealthMultiplier += percent;
            var newMaxHealth = _initialMaxHealth * _maxHealthMultiplier;
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

        private void HealForPercentage(float percentage)
        {
            HealForValue(percentage * _currentMaxHealth);
        }

        public void Modify(ModifierType type)
        {
            switch (type)
            {
                case ModifierType.Heal:
                    HealForPercentage(0.5f);
                    break;
                case ModifierType.MaxHpIncrease:
                    IncreaseMaxHealthByPercent(.1f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}