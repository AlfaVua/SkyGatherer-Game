using System;
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

        protected override void UpdateView()
        {
            loseReason.text = reason switch
            {
                LoseReason.Fell => "Fell out of map",
                _ => "Health at zero",
            };

            levelReached.text = player.ExperienceController.CurrentLevel.ToString();
        }

        private void StartNextGame()
        {
            GameController.ReloadActiveScene();
        }

        private void BackToMenu()
        {
            GameController.BackToMenu();
        }

        private void OnEnable()
        {
            startNext.onClick.AddListener(StartNextGame);
            backToMain.onClick.AddListener(BackToMenu);
        }

        private void OnDisable()
        {
            startNext.onClick.RemoveListener(StartNextGame);
            backToMain.onClick.RemoveListener(BackToMenu);
        }
    }
}