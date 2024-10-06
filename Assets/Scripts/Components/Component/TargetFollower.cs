using UnityEngine;

namespace Components.Component
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D target;
        [SerializeField][Range(0, 1)] private float lerpSpeed;
        [SerializeField] private float followOffset;

        private Vector2 _followVelocity = new Vector2();

        private void FixedUpdate()
        {
            if (!target) return;
            _followVelocity = Vector2.LerpUnclamped(_followVelocity, target.velocity, lerpSpeed / 3f);
            var targetPos = target.position + _followVelocity * followOffset;
            var currentPos = transform.position;
            transform.position = new Vector3(
                Mathf.Lerp(currentPos.x, targetPos.x, lerpSpeed),
                Mathf.Lerp(currentPos.y, targetPos.y, lerpSpeed),
                currentPos.z
                );
        }
    }
}