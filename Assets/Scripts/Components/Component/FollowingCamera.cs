using UnityEngine;

namespace Components.Component
{
    public class FollowingCamera : MonoBehaviour
    {
        [SerializeField] private float effectorPositionEffect;
        public Transform additionalEffector;
        private Vector3 _targetPosition;

        private void Awake()
        {
            _targetPosition = transform.position;
        }

        private void Update()
        {
            var effector = additionalEffector ? new Vector3(additionalEffector.position.x, additionalEffector.position.y, transform.position.z) : transform.position;
            transform.position = Vector3.Lerp(transform.position, _targetPosition + (effector - _targetPosition) * effectorPositionEffect, .05f * Time.deltaTime * 60);
        }

        public void SetTargetPosition(float x, float y)
        {
            _targetPosition.Set(x, y, transform.position.z);
        }
    }
}