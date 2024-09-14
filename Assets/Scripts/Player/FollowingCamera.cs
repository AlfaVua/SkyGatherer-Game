using UnityEngine;

namespace Player
{
    public class FollowingCamera : MonoBehaviour
    {
        [SerializeField] private float playerPositionEffect;
        [HideInInspector] public Transform player;
        private Vector3 targetPosition;

        private void Update()
        {
            var playerCoords = new Vector3(player.position.x, player.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition + (playerCoords - targetPosition) * playerPositionEffect, .05f * Time.deltaTime * 60);
        }

        public void SetTargetPosition(float x, float y)
        {
            targetPosition.Set(x, y, transform.position.z);
        }
    }
}