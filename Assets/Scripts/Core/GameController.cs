using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        
        [SerializeField] private LevelController levelController;
        [SerializeField] private Camera mainCamera;
        
        public readonly UnityEvent<int, uint> PlayerMovedToNewLevel = new UnityEvent<int, uint>();
        
        private void Awake()
        {
            Instance = this;
            levelController.Init(this);
        }

        private void Start()
        {
            PlayerMovedToNewLevel.Invoke(0, 0);
        }

        private int positionX = 0;
        private uint positionY = 0;

        private void Update() //tests
        {
            var update = false;
            if (Input.GetKeyDown(KeyCode.A))
            {
                positionX--;
                update = true;
            } else if (Input.GetKeyDown(KeyCode.D))
            {
                positionX++;
                update = true;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                positionY++;
                update = true;
            } else if (Input.GetKeyDown(KeyCode.S) && positionY != 0)
            {
                positionY--;
                update = true;
            }

            if (update)
            {
                PlayerMovedToNewLevel.Invoke(positionX, positionY);
                mainCamera.transform.position = new Vector3(positionX * 17.8f, positionY * 10f, mainCamera.transform.position.z);
            }
        }
    }
}