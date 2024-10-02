using Player;
using TMPro;
using UnityEngine;

namespace UI.Components
{
    public class ExperienceProgressbar : MonoBehaviour
    {
        [SerializeField] private PlayerExperienceController playerExperienceController;
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private TextMeshProUGUI levelText;

        private void UpdateLevel(uint level, float nextExperience)
        {
            levelText.text = level.ToString();
            progressBar.SetMaxValue(nextExperience);
        }

        private void UpdateExperience(float experience)
        {
            progressBar.SetValue(experience, true);
        }

        private void OnEnable()
        {
            playerExperienceController.OnLevelChanged.AddListener(UpdateLevel);
            playerExperienceController.OnXpChanged.AddListener(UpdateExperience);
        }

        private void OnDisable()
        {
            playerExperienceController.OnLevelChanged.RemoveListener(UpdateLevel);
            playerExperienceController.OnXpChanged.RemoveListener(UpdateExperience);
        }
    }
}