using UnityEngine;

namespace UI
{
    public abstract class UIBase : MonoBehaviour
    {

        public void Show()
        {
            gameObject.SetActive(true);
            UpdateView();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        protected abstract void UpdateView();
    }
}
