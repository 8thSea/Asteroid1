using System.Collections.Generic;
using UnityEngine;

namespace SpaceMiner
{
    /// <summary>
    /// Manages player resources such as ore, fuel, energy and credits.
    /// </summary>
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance { get; private set; }

        [SerializeField]
        private int fuel = 100;

        [SerializeField]
        private int energy = 100;

        [SerializeField]
        private int credits = 0;

        private readonly Dictionary<MineralDefinition, int> ore = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        /// <summary>
        /// Adds ore to the player's cargo hold.
        /// </summary>
        public void AddOre(MineralDefinition mineral, int amount)
        {
            if (!ore.ContainsKey(mineral))
            {
                ore[mineral] = 0;
            }
            ore[mineral] += amount;
        }

        /// <summary>
        /// Spends credits if available.
        /// </summary>
        public bool SpendCredits(int amount)
        {
            if (credits < amount)
            {
                return false;
            }
            credits -= amount;
            return true;
        }
    }
}
