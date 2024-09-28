using UnityEngine;

namespace Player
{
    public class PlayerHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerData playerData;
        private PlayerHealth _healthCore;

        public void Init()
        {
            _healthCore = new PlayerHealth(playerData);
            movement.Init(playerData);
            movement.OnFellFromHeightSignal.AddListener(OnFellFromHeight);
        }


        private void OnFellFromHeight(float damageTaken)
        {
            _healthCore.TakeDamage(damageTaken);
        }
    }
}