using Generators.Level;
using UnityEngine;

namespace Player
{
    public class CollectingController : MonoBehaviour
    {
        [SerializeField] private PlayerInventory inventory;
        
        public void Init()
        {
            inventory.Init();
        }

        public void Collect(LevelObjectData collectable)
        {
            collectable.Drops.ForEach(resource =>
            {
                inventory.AddResource(resource.staticID);
            });
        }
    }
}