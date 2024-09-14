using Components.Collection;
using UnityEngine;

namespace Level.Resource
{
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Special,
        Legendary
    }

    [CreateAssetMenu(menuName = "Resource/Rarity Config")]
    public class RarityConfig : ScriptableObject
    {
        [SerializeField] private WeightsCollection<Rarity> rarities;

        public void Awake()
        {
            rarities?.UpdateWeights();
        }

        public Rarity GetRandomRarity()
        {
            return rarities.GetRandomData();
        }
    }
}