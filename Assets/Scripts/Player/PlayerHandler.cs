using Components.Component;
using Core;
using UnityEngine;

namespace Player
{
    public class PlayerHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private HealthComponent healthComponentCore;
        [SerializeField] private CameraShake shakeOnDamage;
        [SerializeField] private ParticleSystem takeDamageParticles;
        [SerializeField] private PlayerInputController inputController;
        [SerializeField] private PlayerInventory inventory;

        public void Init()
        {
            healthComponentCore.Init(playerData.maxHealth);
            inventory.Init();
            movement.Init(playerData);
            movement.OnFellFromHeightSignal.AddListener(OnFellFromHeight);
            inputController.Init(movement, this);
        }

        private void OnFellFromHeight(float damageTaken)
        {
            healthComponentCore.TakeDamage(damageTaken);
            OnTakeDamage(damageTaken);
        }

        private void OnTakeDamage(float damageTaken)
        {
            shakeOnDamage.Play(damageTaken / 100, .1f);
            takeDamageParticles.Emit(25);
            if (healthComponentCore.CurrentHealth <= 0) GameController.InitLevelLose();
        }
        
        public void AddResource(uint id, int amount = 1)
        {
            inventory.AddResource(id, amount);
        }
    }
}