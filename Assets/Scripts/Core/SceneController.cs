using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneController : MonoBehaviour
    {
        public static SceneController Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static void ReloadActiveScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ShowGameScene()
        {
            SceneManager.LoadScene("Scenes/LevelScene");
        }

        public void ShowMainMenu()
        {
            SceneManager.LoadScene("Scenes/MainScreen");
        }
    }
}
