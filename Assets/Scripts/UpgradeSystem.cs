using System.Collections.Generic;
using UnityEngine;

namespace SpaceMiner
{
    /// <summary>
    /// Manages available upgrades and their application at runtime.
    /// </summary>
    public class UpgradeSystem : MonoBehaviour
    {
        [SerializeField]
        private List<UpgradeDefinition> availableUpgrades = new();

        private readonly HashSet<UpgradeDefinition> purchased = new();

        /// <summary>
        /// Attempts to purchase an upgrade.
        /// </summary>
        public bool Purchase(UpgradeDefinition upgrade)
        {
            if (purchased.Contains(upgrade))
                return false;

            if (!ResourceManager.Instance.SpendCredits(upgrade.cost))
                return false;

            purchased.Add(upgrade);
            upgrade.Apply();
            return true;
        }
    }

    /// <summary>
    /// ScriptableObject describing an upgrade.
    /// </summary>
    [CreateAssetMenu(menuName = "Space Miner/Upgrade Definition")]
    public class UpgradeDefinition : ScriptableObject
    {
        public string upgradeName;
        public int cost;

        /// <summary>
        /// Apply upgrade effects to the game.
        /// </summary>
        public virtual void Apply()
        {
            // TODO: implement upgrade logic
        }
    }
}
