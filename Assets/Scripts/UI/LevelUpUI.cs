using System.Collections.Generic;
using UI.Components;
using UnityEngine;

namespace UI
{
    public class LevelUpUI : UIBase
    {
        [SerializeField] private List<UpgradeCard> cardList;

        protected override void UpdateView()
        {
            cardList.ForEach(card =>
            {
                card.Redraw();
            });
        }
        
        private void OnCardClicked()
        {
            UISignal.ToggleLevelUp.Invoke();
        }

        private void OnEnable()
        {
            cardList.ForEach(card =>
            {
                card.OnClicked.AddListener(OnCardClicked);
            });
        }
        
        private void OnDisable()
        {
            cardList.ForEach(card =>
            {
                card.OnClicked.RemoveListener(OnCardClicked);
            });
        }
    }
}