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

        private bool _disableMovement = false;

        public void Init(PlayerMovement movement, PlayerHandler handler)
        {
            UISignal.OnOverlayUIChanged.AddListener(OnUIVisibilityChanged);
            AddListeners();
            _movement = movement;
            _handler = handler;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (_disableMovement) return;
            _movement.Move(context.ReadValue<Vector2>().x);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (_disableMovement || context.canceled) return;
            _movement.Jump();
        }

        public void OnDrop(InputAction.CallbackContext context)
        {
            if (_disableMovement) return;
            _movement.Drop();
        }

        public void OnOpenInventory(InputAction.CallbackContext context)
        {
            UISignal.ToggleInventory.Invoke();
        }

        private void OnUIVisibilityChanged(bool visible)
        {
            _disableMovement = visible;
            if (!visible) _movement.Move(0);
        }

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void AddListeners()
        {
            InputController.Inputs.Player.AddCallbacks(this);
        }
        
        private void RemoveListeners()
        {
            InputController.Inputs.Player.RemoveCallbacks(this);
        }
    }
}