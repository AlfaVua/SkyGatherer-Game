using System.Collections;
using Components.Component;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private CapsuleCollider2D playerCollider;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float jumpPower = 7;
        [SerializeField] private float moveSpeed = 1;
        [SerializeField] private ParticleSystem slowdownParticles;
        [SerializeField] private Animator movementAnimator;
        [SerializeField] private PlaySoundRandomPitch grassSound;

        public readonly UnityEvent<float> OnFellFromHeightSignal = new();

        private bool _fallingSlowed;
        private bool IsOnGround => RaycastGround();

        private float _movingVelocityX;
        private float _fallDamageThreshold;

        private float _grassSoundCooldown = 0;

        private MovementViewBehavior _moveBehavior;

        [HideInInspector] public float jumpHeightMultiplier;

        public void Init(PlayerData playerData)
        {
            _moveBehavior = new MovementViewBehavior(movementAnimator);
            jumpHeightMultiplier = 1;
            _fallDamageThreshold = playerData.fallDamageThreshold;
        }

        private Vector3 GetRayStartPosition()
        {
            return rigidBody.transform.position + playerCollider.size.y * .35f * Vector3.down;
        }

        private bool RaycastGround(float additionalDistance = 0)
        {
            var hit = Physics2D.BoxCast(GetRayStartPosition(), Vector2.one / 2, 0, Vector2.down, additionalDistance, groundMask);
            return hit;
        }

        private void JumpAction()
        {
            ResetVerticalVelocity();
            _moveBehavior.IsOnGround = false;
            rigidBody.AddForce(jumpPower * jumpHeightMultiplier * Vector2.up, ForceMode2D.Impulse);
        }

        private void Update()
        {
            if (_grassSoundCooldown > (_moveBehavior.IsOnGround ? 0 : .15f))
                _grassSoundCooldown -= Time.deltaTime;
            else if (_moveBehavior.IsOnGround && _movingVelocityX != 0) PlayGrassSound();
        }

        private void PlayGrassSound()
        {
            grassSound.Play();
            _grassSoundCooldown = .35f;
        }

        private void FixedUpdate()
        {
            rigidBody.velocity = new Vector2(_movingVelocityX, rigidBody.velocity.y);
            if (rigidBody.velocity.y < -1)
            {
                rigidBody.velocity += rigidBody.mass / 50f * Physics2D.gravity;
            }
            _moveBehavior.Update(rigidBody.velocity);
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
            if (!IsOnGround) return;
            _moveBehavior.IsOnGround = true;
            
            if (other.relativeVelocity.y < 0) return;
            PlayGrassSound();
            if (other.relativeVelocity.y > _fallDamageThreshold)
                OnFellFromHeight(other.relativeVelocity.y);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            _moveBehavior.IsOnGround = false;
        }

        public void Move(float direction)
        {
            _movingVelocityX = direction * moveSpeed;
            if (direction != 0)
                movementAnimator.transform.localScale = new Vector3(direction, 1, 1);
            else if (IsOnGround)
                ResetVerticalVelocity();
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