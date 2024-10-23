using UnityEngine;

namespace Player.Components
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private PlayerPhysicsController physicsController;

        public void Move(float x)
        {
            physicsController.Move(x * moveSpeed);
        }
    }
}