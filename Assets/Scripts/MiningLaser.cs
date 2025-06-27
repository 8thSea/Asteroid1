using UnityEngine;

namespace SpaceMiner
{
    /// <summary>
    /// Fires a ray to mine asteroids and collects minerals defined via ScriptableObject.
    /// </summary>
    public class MiningLaser : MonoBehaviour
    {
        [SerializeField]
        private float range = 100f;

        [SerializeField]
        private LineRenderer laserLine;

        [SerializeField]
        private MineralDefinition defaultMineral;

        private Camera cam;

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                Fire();
            }
            else
            {
                laserLine.enabled = false;
            }
        }

        private void Fire()
        {
            laserLine.enabled = true;
            laserLine.SetPosition(0, transform.position);

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out var hit, range))
            {
                laserLine.SetPosition(1, hit.point);
                var mineral = hit.collider.GetComponent<MineralDefinition>() ?? defaultMineral;
                ResourceManager.Instance.AddOre(mineral, 1);
                // TODO: voxel cut-in and particle effects
            }
            else
            {
                laserLine.SetPosition(1, cam.transform.position + cam.transform.forward * range);
            }
        }
    }

    /// <summary>
    /// Defines mineral yield properties for asteroids.
    /// </summary>
    [CreateAssetMenu(menuName = "Space Miner/Mineral Definition")]
    public class MineralDefinition : ScriptableObject
    {
        public string mineralName;
        public int value;
    }
}
