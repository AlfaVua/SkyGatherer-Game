using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerHealth
    {
        public readonly UnityEvent<float> OnTakeDamage = new();
        private float _currentHealth;
        
        public PlayerHealth(PlayerData playerData)
        {
            _currentHealth = playerData.maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (damage == 0) return;
            OnTakeDamage.Invoke(_currentHealth -= damage);
            Debug.Log("Health: " + _currentHealth);
        }
    }
}