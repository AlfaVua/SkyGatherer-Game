using Components.Component;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private HealthComponent healthComponentCore;
        [SerializeField] private CameraShake shakeOnDamage;
        [SerializeField] private ParticleSystem takeDamageParticles;

        public void Init()
        {
            healthComponentCore.Init(playerData.maxHealth);
            movement.Init(playerData);
            movement.OnFellFromHeightSignal.AddListener(OnFellFromHeight);
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
            if (healthComponentCore.CurrentHealth <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}