using UnityEngine;

namespace Components.Component
{
    public class StartAnimationAtRandomFrame : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void Start()
        {
            var state = animator.GetCurrentAnimatorStateInfo(0);
            animator.Play(state.fullPathHash, 0, UnityEngine.Random.Range(0f, 1f));
        }
    }
}