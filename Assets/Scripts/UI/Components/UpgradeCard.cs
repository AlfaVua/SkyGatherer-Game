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

        public void Redraw()
        {
        }
    }
}