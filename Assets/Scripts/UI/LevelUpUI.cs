using System.Collections.Generic;
using Core;
using TMPro;
using UI.Components;
using UnityEngine;

namespace UI
{
    public class LevelUpUI : UIBase
    {
        [SerializeField] private List<UpgradeCard> cardList;
        [SerializeField] private TextMeshProUGUI availableLevelsText;

        private uint _lastDisplayedLevel = 1;

        private uint _levelsLeft = 0;
        public override bool CanCloseByOthers => false;
        
        protected override void UpdateView()
        {
            var playerLevel = GameController.Instance.Player.ExperienceController.CurrentLevel;
            _levelsLeft = playerLevel - _lastDisplayedLevel;
            _lastDisplayedLevel = playerLevel;
            RedrawCards();
        }

        private void RedrawCards()
        {
            availableLevelsText.text = _levelsLeft.ToString();
            cardList.ForEach(card =>
            {
                card.Redraw();
            });
        }
        
        private void OnCardClicked(UpgradeCard card)
        {
            card.Apply();
            if (--_levelsLeft != 0)
            {
                RedrawCards();
                availableLevelsText.text = _levelsLeft.ToString();
            }
            else
            {
                UISignal.ToggleLevelUp.Invoke();
            }
        }

        private void OnEnable()
        {
            cardList.ForEach(card =>
            {
                card.OnClicked.AddListener(() =>
                {
                    OnCardClicked(card);
                });
            });
        }
        
        private void OnDisable()
        {
            cardList.ForEach(card =>
            {
                card.OnClicked.RemoveAllListeners();
            });
        }
    }
}