using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour, Inputs.IPlayerActions
    {
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float rayCastingOffsetY;
        [SerializeField] private float raycastDistance = 1;
        [SerializeField] private float jumpPower = 7;
        [SerializeField] private float moveSpeed = 1;
        [SerializeField] [Min(0)] private float rayDistanceFromCenter = .7f;
        private bool IsOnGround => rigidBody.velocity.y <= 0 && (
            Physics2D.Raycast(GetRayStartPosition(-1), Vector2.down, raycastDistance, groundMask) ||
            Physics2D.Raycast(GetRayStartPosition(1), Vector2.down, raycastDistance, groundMask)
            );
        public float RaycastDistance => raycastDistance;

        public Vector3 GetRayStartPosition(float rayDirectionX = 0)
        {
            return rigidBody.transform.position + Vector3.right * (rayDirectionX * rayDistanceFromCenter * .35f) + Vector3.up * rayCastingOffsetY;
        }

        private void JumpAction()
        {
            rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            if (rigidBody.velocity.y < -1)
            {
                rigidBody.velocity += Vector2.up * (rigidBody.mass * Physics2D.gravity.y / 50f);
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            rigidBody.velocity = new Vector2(context.ReadValue<Vector2>().x * moveSpeed, rigidBody.velocity.y);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!IsOnGround) return;
            JumpAction();
        }

        public void OnDrop(InputAction.CallbackContext context)
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
            Gizmos.DrawLine(positionLeft, positionLeft + Vector3.down * RaycastDistance);
            Gizmos.DrawLine(positionRight, positionRight + Vector3.down * RaycastDistance);
        }

        private void OnEnable()
        {
            GameController.Instance.Inputs.Player.AddCallbacks(this);
        }

        private void OnDisable()
        {
            GameController.Instance.Inputs.Player.RemoveCallbacks(this);
        }
    }
}
