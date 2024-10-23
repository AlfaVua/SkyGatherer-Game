using Core;
using Player.Components;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputController : MonoBehaviour, Inputs.IPlayerActions
    {
        [SerializeField] private JumpComponent jumpComponent;
        [SerializeField] private MovementComponent movementComponent;
        [SerializeField] private PlayerPhysicsController physicsController;

        private bool _disableMovement;

        public void Init()
        {
            UISignal.OnOverlayUIChanged.AddListener(OnUIVisibilityChanged);
            AddListeners();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (_disableMovement) return;
            movementComponent.Move(context.ReadValue<Vector2>().x);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (_disableMovement || context.canceled) return;
            jumpComponent.Jump();
        }

        public void OnDrop(InputAction.CallbackContext context)
        {
            if (_disableMovement) return;
            physicsController.Drop();
        }

        public void OnOpenInventory(InputAction.CallbackContext context)
        {
            if (context.started) UISignal.ToggleInventory.Invoke();
        }

        public void OnSettings(InputAction.CallbackContext context)
        {
            if (context.started) UISignal.OnEscPressed.Invoke();
        }

        private void OnUIVisibilityChanged(bool visible)
        {
            _disableMovement = visible;
            if (visible) movementComponent.Move(0);
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