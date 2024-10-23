using Player.Modifiers;
using Player.Modifiers.Data;
using UnityEngine;

namespace Player.Components
{
    public class JumpComponent : MonoBehaviour, IModifiable
    {
        [SerializeField] private float jumpForce = 7f;
        [SerializeField] private PlayerPhysicsController controller;
        [SerializeField] private Rigidbody2D rigidBody;
        
        private float _modifier = 0;

        public void Modify(ModifierType type)
        {
            if (type == ModifierType.JumpHeight)
                _modifier += 0.04f;
        }

        public void Jump()
        {
            if (controller.IsOnGround) controller.JumpAction(jumpForce + _modifier);
            else if (!controller.IsFallingSlowed && rigidBody.velocity.y < 0) controller.SlowdownFalling();
        }
    }
}