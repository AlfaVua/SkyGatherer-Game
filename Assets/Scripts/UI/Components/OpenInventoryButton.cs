using System.Collections.Generic;
using Generators.Resource;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class OpenInventoryButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private ResourceFloatImage collectedResourcePrefab;
        [SerializeField] private Transform collectedResourceContainer;

        private void OnResourceCollected(IEnumerable<ResourceData> resourceData, Vector3 worldPosition)
        {
            if (!Camera.main) return;
            var screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            foreach (var data in resourceData)
                CreateFloatImage(data, screenPosition + Vector3.right * (Random.value * 50 - 25));
        }

        private void CreateFloatImage(ResourceData resourceData, Vector3 screenPosition)
        {
            var floatImage = Instantiate(collectedResourcePrefab, collectedResourceContainer);
            floatImage.transform.position = screenPosition;
            floatImage.Init(resourceData, transform.position);
        }

        private void OnClick()
        {
            UISignal.ToggleInventory.Invoke();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnClick);
            playerInventory.OnCollectResources.AddListener(OnResourceCollected);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
            playerInventory.OnCollectResources.RemoveListener(OnResourceCollected);
        }
    }
}