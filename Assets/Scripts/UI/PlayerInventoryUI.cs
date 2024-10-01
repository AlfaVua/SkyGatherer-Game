using Core;
using Generators.Resource;
using Player;
using UI.Components;
using UnityEngine;
using UnityEngine.UI;
using Utils.TransformExtenstion;

namespace UI
{
    public class PlayerInventoryUI : UIBase
    {
        [SerializeField] private PlayerInventory inventory;
        [SerializeField] private GridLayoutGroup layout;
        [SerializeField] private InventoryCell prefab;
        protected override void UpdateView()
        {
            RenderResources();
        }

        private void RenderResources()
        {
            layout.transform.ClearChildren();
            foreach (var resourcePair in inventory.GetResources())
            {
                RenderResource(GameController.Instance.GetResourceById(resourcePair.Key), resourcePair.Value);
            }
        }

        private void RenderResource(ResourceData resource, int amount)
        {
            var item = Instantiate(prefab, layout.transform);
            item.Init(resource, amount);
        }
    }
}