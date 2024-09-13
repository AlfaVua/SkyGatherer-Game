using UnityEngine;

namespace Player
{
    public class FollowingCamera : MonoBehaviour
    {
        [HideInInspector] public Vector3 targetPosition;
        [HideInInspector] public Transform player;
        [SerializeField] private float playerPositionEffect;

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition + (player.position - targetPosition) * playerPositionEffect, .05f * Time.deltaTime * 60);
        }
    }
}