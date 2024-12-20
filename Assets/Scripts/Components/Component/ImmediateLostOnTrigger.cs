using Core;
using UI;
using UnityEngine;

namespace Components.Component
{
    public class ImmediateLostOnTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (Utils.Utils.IsPlayer(other.gameObject))
            {
                GameController.Instance.OnLost(LoseReason.Fell);
            }
        }
    }
}