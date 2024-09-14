using UnityEngine;

namespace Components.Component
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float parallaxDivider = 2;
        private void FixedUpdate()
        {
            if (parallaxDivider == 0) return;
            var position = target.position;
            transform.position = new Vector3(position.x / parallaxDivider, position.y / parallaxDivider, transform.position.z);
        }
    }
}