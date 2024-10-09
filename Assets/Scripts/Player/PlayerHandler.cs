using Components.Component;
using Core;
using Generators.Level;
using Player.Components;
using UI;
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
        
        public PlayerMovement PlayerMovement => movement;
        public HealthComponent PlayerHealth => healthComponentCore;
        public PlayerExperienceController ExperienceController => experienceController;

        public void Init()
        {
            collectingController.Init();
            healthComponentCore.Init(playerData.maxHealth);
            movement.Init(playerData);
            experienceController.Init();
            inputController.Init(movement, this);
            AddListeners();
        }

        private void AddListeners()
        {
            movement.OnFellFromHeightSignal.AddListener(OnFellFromHeight);
            experienceController.OnLevelChanged.AddListener(OnLevelUp);
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
            FloatingTextUI.CreateFloatingText(transform.position, Color.white, "EXP +" + Mathf.Floor(experienceController.GetActualExperience(collectable.CollectionExperience)));
        }

        private void OnLevelUp(uint level, float nextExperience)
        {
            UISignal.ToggleLevelUp.Invoke();
        }
    }
}