using UnityEngine;

namespace Components.Component
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] [Range(-1, 1)] private float parallaxEffect;

        private void Update()
        {
            transform.position = new Vector3(target.position.x * parallaxEffect, target.position.y * parallaxEffect, transform.position.z);
        }
    }
}