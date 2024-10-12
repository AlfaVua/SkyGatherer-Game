using Core;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public enum LoseReason { Fell, Health }
    public class LoseUI : UIBase
    {
        [SerializeField] private PlayerHandler player;
        [SerializeField] private TextMeshProUGUI loseReason;
        [SerializeField] private TextMeshProUGUI levelReached;
        [SerializeField] private Button startNext;
        [SerializeField] private Button backToMain;

        public LoseReason reason;

        public override bool CanCloseByOthers => false;

        protected override void UpdateView()
        {
            loseReason.text = reason switch
            {
                LoseReason.Fell => "Fell out of map",
                _ => "Health at zero",
            };

            levelReached.text = player.ExperienceController.CurrentLevel.ToString();
        }

        private void OnEnable()
        {
            startNext.onClick.AddListener(SceneController.ReloadActiveScene);
            backToMain.onClick.AddListener(SceneController.Instance.ShowMainMenu);
        }

        private void OnDisable()
        {
            startNext.onClick.RemoveListener(SceneController.ReloadActiveScene);
            backToMain.onClick.RemoveListener(SceneController.Instance.ShowMainMenu);
        }
    }
}