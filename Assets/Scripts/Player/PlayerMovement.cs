using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float raycastDistance = .35f;
        [SerializeField] private float jumpPower = 7;
        [SerializeField] private float moveSpeed = 1;
        [SerializeField] [Min(0)] private float rayDistanceFromCenter = 1;
        [SerializeField] private ParticleSystem slowdownParticles;

        public readonly UnityEvent<float> OnFellFromHeightSignal = new();

        private bool _fallingSlowed;
        private bool IsOnGround => RaycastGround();

        private float _movingVelocityX;
        private float _fallDamageThreshold;

        [HideInInspector] public float jumpHeightMultiplier;

        public void Init(PlayerData playerData)
        {
            jumpHeightMultiplier = 1;
            _fallDamageThreshold = playerData.fallDamageThreshold;
        }

        private Vector3 GetRayStartPosition()
        {
            return rigidBody.transform.position + rayDistanceFromCenter * .35f * Vector3.down;
        }

        private bool RaycastGround(float distanceMultiplier = 1)
        {
            var hit = Physics2D.BoxCast(GetRayStartPosition(), Vector2.one / 2, transform.rotation.z, Vector2.down, distanceMultiplier * raycastDistance, groundMask);
            if (hit.point != Vector2.zero)
                Debug.DrawLine(GetRayStartPosition(), hit.point, Color.red, 2);
            return hit;
        }

        private void JumpAction()
        {
            ResetVerticalVelocity();
            rigidBody.AddForce(jumpPower * jumpHeightMultiplier * Vector2.up, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            rigidBody.velocity = new Vector2(_movingVelocityX, rigidBody.velocity.y);
            if (rigidBody.velocity.y < -1)
            {
                rigidBody.velocity += rigidBody.mass / 50f * Physics2D.gravity;
            }
        }

        private void SlowdownFalling()
        {
            rigidBody.AddForce(Vector2.down * (rigidBody.velocity.y * .9f), ForceMode2D.Impulse);
            _fallingSlowed = true;
            slowdownParticles.Emit(25);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _fallingSlowed = false;
            if (other.relativeVelocity.y > _fallDamageThreshold && IsOnGround)
                OnFellFromHeight(other.relativeVelocity.y);
        }

        public void Move(float direction)
        {
            _movingVelocityX = direction * moveSpeed;
        }

        public void Jump()
        {
            if (IsOnGround) JumpAction();
            else if (!_fallingSlowed && rigidBody.velocity.y < 0) SlowdownFalling();
        }

        public void Drop()
        {
            if (rigidBody.velocity.y < -1) return;

            ResetVerticalVelocity();
            if (IsOnGround)
            {
                playerCollider.enabled = false;
                StartCoroutine(nameof(EnableCollisionRoutine));
                rigidBody.AddForce(Vector2.down * 2, ForceMode2D.Impulse);
            }
            else
            {
                rigidBody.AddForce(Vector2.down, ForceMode2D.Impulse);
            }
        }

        private void ResetVerticalVelocity()
        {
            rigidBody.velocity = Vector2.right * rigidBody.velocity.x;
        }

        private IEnumerator EnableCollisionRoutine()
        {
            yield return new WaitForSeconds(.2f);
            playerCollider.enabled = true;
        }

        private void OnFellFromHeight(float fallSpeed)
        {
            var damage = (fallSpeed - _fallDamageThreshold * .9f) * .78f;
            OnFellFromHeightSignal.Invoke(damage * damage);
        }
    }
}