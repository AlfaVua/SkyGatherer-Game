using System;
using Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        [SerializeField] private LevelController levelController;
        [SerializeField] private FollowingCamera camera;
        [SerializeField] private PlayerHandler player;
        
        public readonly UnityEvent<int, uint> PlayerMovedToNewLevel = new UnityEvent<int, uint>();
        private void Awake()
        {
            Instance = this;
            player.Init();
            levelController.Init();
            PlayerMovedToNewLevel.AddListener(OnPlayerInNewLevel);
        }

        private void Start()
        {
            levelController.GenerateStartingLevel();
        }

        private void OnPlayerInNewLevel(int indexX, uint indexY)
        {
            levelController.SetNewActiveCoords(indexX, indexY);
            SetCameraTarget(Utils.Utils.IndexToX * indexX, Utils.Utils.IndexToY * indexY);
        }

        private Action<float, float> SetCameraTarget => camera.SetTargetPosition;

        // private int positionX = 0;
        // private uint positionY = 0;
        // private void Update() //tests
        // {
        //     var update = false;
        //     if (Input.GetKeyDown(KeyCode.A))
        //     {
        //         positionX--;
        //         update = true;
        //     } else if (Input.GetKeyDown(KeyCode.D))
        //     {
        //         positionX++;
        //         update = true;
        //     }
        //     if (Input.GetKeyDown(KeyCode.W))
        //     {
        //         positionY++;
        //         update = true;
        //     } else if (Input.GetKeyDown(KeyCode.S) && positionY != 0)
        //     {
        //         positionY--;
        //         update = true;
        //     }
        //     
        //     if (update)
        //     {
        //         PlayerMovedToNewLevel.Invoke(positionX, positionY);
        //     }
        // }

        private void OnEnable()
        {
            InputController.Inputs.Enable();
        }

        private void OnDisable()
        {
            InputController.Inputs.Disable();
        }

        public static void InitLevelLose()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}