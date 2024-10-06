using UnityEngine;

namespace Player
{
    public class MovementViewBehavior
    {
        private Animator _animator;
        private string _lastState = "Idle";

        private Vector2 _velocity;
        public bool IsOnGround;
        public MovementViewBehavior(Animator animator)
        {
            _animator = animator;
        }

        private void UpdateState()
        {
            if (!IsOnGround && _velocity.y != 0)
            {
                ChangeState(_velocity.y > 0 ? "MoveUp" : "MoveDown");
                return;
            }
            ChangeState(_velocity.x switch
            {
                < 0 => "Run",
                > 0 => "Run",
                _ => "Idle"
            });
        }

        public void Update(Vector2 velocity)
        {
            _velocity = velocity;
            UpdateState();
        }

        private void ChangeState(string stateName)
        {
            if (_lastState == stateName) return;
            _animator.Play(stateName);
            _lastState = stateName;
        }
    }
}