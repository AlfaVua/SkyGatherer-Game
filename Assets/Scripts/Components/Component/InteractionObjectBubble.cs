using System;
using UnityEngine;

namespace Components.Component
{
    public class InteractionObjectBubble : MonoBehaviour
    {
        [SerializeField] private Transform bubbleContainer;
        [SerializeField] private Animator animator;


        private void Awake()
        {
            if (animator)
            {
                bubbleContainer.gameObject.SetActive(true);
            }
        }

        public void Show()
        {
            if (animator)
            {
                animator.Play("Show");
                return;
            }
            bubbleContainer.gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            if (animator)
            {
                animator.Play("Hide");
                return;
            }
            bubbleContainer.gameObject.SetActive(false);
        }
    }
}