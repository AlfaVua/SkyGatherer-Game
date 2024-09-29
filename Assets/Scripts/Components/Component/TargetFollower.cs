using UnityEngine;

namespace Components.Component
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] [Range(-1, 1)] private float parallaxEffect = 1;
        [SerializeField] private bool vertical = true;

        private void Update()
        {
            transform.position = new Vector3(target.position.x * parallaxEffect, vertical ? target.position.y * parallaxEffect : transform.position.y, transform.position.z);
        }
    }
}