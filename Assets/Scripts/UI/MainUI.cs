using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private Scene gameScene;

        private void StartGame()
        {
            SceneManager.LoadScene("Scenes/LevelScene");
        }

        private void CloseGame()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
                return;
            }
#endif
            Application.Quit();
        }
        
        private void OnEnable()
        {
            startButton.onClick.AddListener(StartGame);
            closeButton.onClick.AddListener(CloseGame);
        }
        
        private void OnDisable()
        {
            startButton.onClick.RemoveListener(StartGame);
            closeButton.onClick.RemoveListener(CloseGame);
        }
    }
}