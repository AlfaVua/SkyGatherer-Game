using Core;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputController : MonoBehaviour, Inputs.IPlayerActions
    {
        private PlayerMovement _movement;
        private PlayerHandler _handler;

        public void Init(PlayerMovement movement, PlayerHandler handler)
        {
            _movement = movement;
            _handler = handler;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _movement.Move(context.ReadValue<Vector2>().x);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.canceled) return;
            _movement.Jump();
        }

        public void OnDrop(InputAction.CallbackContext context)
        {
            _movement.Drop();
        }

        public void OnOpenInventory(InputAction.CallbackContext context)
        {
            UISignal.ToggleInventory.Invoke();
        }

        private void OnEnable()
        {
            InputController.Inputs.Player.AddCallbacks(this);
        }

        private void OnDisable()
        {
            InputController.Inputs.Player.RemoveCallbacks(this);
        }
    }
}