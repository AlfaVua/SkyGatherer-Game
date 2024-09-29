using UnityEngine;
using UnityEngine.Events;

namespace Components.Component
{
    public class HealthComponent : MonoBehaviour
    {
        public readonly UnityEvent<float> OnHealthChanged = new();
        public float CurrentHealth { get; private set; }

        public void Init(float maxHealth)
        {
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (damage == 0) return;
            OnHealthChanged.Invoke(CurrentHealth -= damage);
        }
    }
}