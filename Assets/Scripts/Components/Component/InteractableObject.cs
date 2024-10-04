using Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Components.Component
{
    public class InteractableObject : MonoBehaviour, Inputs.IInteractableActions
    {
        [SerializeField] private InteractionObjectBubble interactionObjectBubble;
        public readonly UnityEvent OnInteractionStarted = new();
        public readonly UnityEvent OnInteractionEnded = new();

        private bool _canInteract;
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (!_canInteract) return;
            (context.canceled ? OnInteractionEnded : OnInteractionStarted).Invoke();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!Utils.Utils.IsPlayer(other.gameObject)) return;
            _canInteract = true;
            interactionObjectBubble?.Show();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!Utils.Utils.IsPlayer(other.gameObject)) return;
            _canInteract = false;
            interactionObjectBubble?.Hide();
        }

        private void OnEnable()
        {
            InputController.Inputs.Interactable.AddCallbacks(this);
        }

        private void OnDisable()
        {
            InputController.Inputs.Interactable.RemoveCallbacks(this);
        }
    }
}