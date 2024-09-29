using Components.Component;
using UnityEngine;

namespace Player
{
    public class PlayerHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private HealthComponent healthComponentCore;

        public void Init()
        {
            healthComponentCore.Init(playerData.maxHealth);
            movement.Init(playerData);
            movement.OnFellFromHeightSignal.AddListener(OnFellFromHeight);
        }

        private void OnFellFromHeight(float damageTaken)
        {
            healthComponentCore.TakeDamage(damageTaken);
        }
    }
}