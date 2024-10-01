using Generators.Resource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class InventoryCell : MonoBehaviour
    {
        [SerializeField] private Image sprite;
        [SerializeField] private TextMeshProUGUI amountText;
        public void Init(ResourceData resource, int amount)
        {
            sprite.sprite = resource.Icon;
            amountText.text = amount < 2 ? "" : amount.ToString();
        }
    }
}