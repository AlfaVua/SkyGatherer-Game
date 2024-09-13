using System;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        
        [SerializeField] private LevelController levelController;
        [SerializeField] private FollowingCamera camera;
        [SerializeField] private PlayerMovement playerPrefab;
        
        private Inputs _inputs;
        
        public readonly UnityEvent<int, uint> PlayerMovedToNewLevel = new UnityEvent<int, uint>();
        public Inputs Inputs => _inputs;
        private void Awake()
        {
            Instance = this;
            _inputs = new Inputs();
            levelController.Init(this);
            PlayerMovedToNewLevel.AddListener(OnPlayerInNewLevel);
        }

        private void Start()
        {
            camera.player = Instantiate(playerPrefab).transform;
            levelController.GenerateInit();
        }

        private void OnPlayerInNewLevel(int indexX, uint indexY)
        {
            SetCameraTarget(Utils.Utils.IndexToX * indexX, Utils.Utils.IndexToY * indexY);
        }

        public void SetCameraTarget(float x, float y)
        {
            camera.targetPosition.Set(x, y, camera.transform.position.z);
        }

        // private int positionX = 0;
        // private uint positionY = 0;
        private void Update() //tests
        {
            // var update = false;
            // if (Input.GetKeyDown(KeyCode.A))
            // {
            //     positionX--;
            //     update = true;
            // } else if (Input.GetKeyDown(KeyCode.D))
            // {
            //     positionX++;
            //     update = true;
            // }
            // if (Input.GetKeyDown(KeyCode.W))
            // {
            //     positionY++;
            //     update = true;
            // } else if (Input.GetKeyDown(KeyCode.S) && positionY != 0)
            // {
            //     positionY--;
            //     update = true;
            // }
            //
            // if (update)
            // {
            //     PlayerMovedToNewLevel.Invoke(positionX, positionY);
            //     mainCamera.transform.position = new Vector3(positionX * 17.8f, positionY * 10f, mainCamera.transform.position.z);
            // }
        }

        private void OnEnable()
        {
            _inputs.Enable();
        }

        private void OnDisable()
        {
            _inputs.Disable();
        }
    }
}