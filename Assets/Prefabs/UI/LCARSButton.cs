using UnityEngine;
using UnityEngine.UI;

namespace SpaceMiner
{
    /// <summary>
    /// Simple controller for LCARS style UI buttons.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class LCARSButton : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            // TODO: style the button with neon colors
        }

        /// <summary>
        /// Adds a listener to the underlying button.
        /// </summary>
        public void AddListener(UnityEngine.Events.UnityAction callback)
        {
            button.onClick.AddListener(callback);
        }
    }
}
