using Core;
using Player.Modifiers.Data;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace UI.Components
{
    public class UpgradeCard : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;

        public Button.ButtonClickedEvent OnClicked => button.onClick;
        
        private PlayerModifierData _staticData;

        public void Redraw()
        {
            _staticData = GameController.Instance.ModifierHandler.GetRandomModifier();
            title.text = _staticData.Title;
            description.text = _staticData.Description;
        }

        public void Apply()
        {
            GameController.Instance.ModifierHandler.ApplyModifier(_staticData);
        }
    }
}