using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Components.Component
{
    public class PlayAnimationRandomTime : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string animationName;

        private void Start()
        {
            animator.Play(animationName, 0, Random.value);
        }
    }
}