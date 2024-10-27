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

        public void Collect(LevelObjectData collectable, Vector3 worldPosition)
        {
            if (collectable.Drops.Count == 0) return;
            collectable.Drops.ForEach(resource => inventory.AddResource(resource.staticID));
            inventory.OnCollectResources.Invoke(collectable.Drops, worldPosition);
        }
    }
}