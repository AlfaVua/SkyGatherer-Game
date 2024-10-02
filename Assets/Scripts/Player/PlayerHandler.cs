using Components.Component;
using Core;
using Generators.Level;
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
        [SerializeField] private PlayerExperienceController experienceController;
        [SerializeField] private CollectingController collectingController;

        public void Init()
        {
            collectingController.Init();
            healthComponentCore.Init(playerData.maxHealth);
            movement.Init(playerData);
            experienceController.Init();
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

        public void OnCollectResource(LevelObjectData collectable)
        {
            collectingController.Collect(collectable);
            experienceController.AddExperience(collectable.CollectionExperience);
        }
    }
}