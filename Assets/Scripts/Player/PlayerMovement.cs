using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float raycastDistance = 1;
        [SerializeField] private float jumpPower = 7;
        [SerializeField] private float moveSpeed = 1;
        [SerializeField] [Min(0)] private float rayDistanceFromCenter = .7f;
        [SerializeField] private ParticleSystem slowdownParticles;
        
        public readonly UnityEvent<float> OnFellFromHeightSignal = new();

        private bool _fallingSlowed;
        private bool IsOnGround => rigidBody.velocity.y <= 0 && RaycastGround();

        private float _movingVelocityX;
        private float _fallDamageThreshold;

        public void Init(PlayerData playerData)
        {
            _fallDamageThreshold = playerData.fallDamageThreshold;
        }

        private Vector3 GetRayStartPosition(float rayDirectionX = 0)
        {
            return rigidBody.transform.position + Vector3.right * (rayDirectionX * rayDistanceFromCenter * .35f);
        }

        private bool RaycastGround(float distanceMultiplier = 1)
        {
            return Physics2D.Raycast(GetRayStartPosition(-1), Vector2.down, raycastDistance * distanceMultiplier,
                       groundMask) ||
                   Physics2D.Raycast(GetRayStartPosition(1), Vector2.down, raycastDistance * distanceMultiplier,
                       groundMask);
        }

        private void JumpAction()
        {
            rigidBody.velocity = Vector2.right * rigidBody.velocity.x;
            rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
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
            if (other.relativeVelocity.y > _fallDamageThreshold && IsOnGround) OnFellFromHeight(other.relativeVelocity.y);
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
            playerCollider.enabled = false;
            rigidBody.AddForce(Vector2.down, ForceMode2D.Impulse);
            StartCoroutine(nameof(EnableCollisionRoutine));
        }

        private IEnumerator EnableCollisionRoutine()
        {
            yield return new WaitForSeconds(.2f);
            playerCollider.enabled = true;
        }

        private void OnDrawGizmos()
        {
            var positionLeft = GetRayStartPosition(-1);
            var positionRight = GetRayStartPosition(1);
            Gizmos.DrawLine(positionLeft, positionLeft + Vector3.down * raycastDistance);
            Gizmos.DrawLine(positionRight, positionRight + Vector3.down * raycastDistance);
        }
        
        private void OnFellFromHeight(float fallSpeed)
        {
            var damage = (fallSpeed - _fallDamageThreshold * .9f) * .78f;
            OnFellFromHeightSignal.Invoke(damage * damage);
        }
    }
}