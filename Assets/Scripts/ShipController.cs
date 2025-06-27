using UnityEngine;

namespace SpaceMiner
{
    /// <summary>
    /// Handles six degrees of freedom flight control for the player's ship.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class ShipController : MonoBehaviour
    {
        [SerializeField]
        private float thrustPower = 10f;

        [SerializeField]
        private float rotationSpeed = 2f;

        private Rigidbody rb;
        private bool inertiaDampening = true;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                inertiaDampening = !inertiaDampening;
            }
        }

        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
            if (inertiaDampening)
            {
                rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.fixedDeltaTime);
                rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, Time.fixedDeltaTime);
            }
        }

        private void HandleMovement()
        {
            Vector3 thrust = new(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Jump"),
                Input.GetAxis("Vertical"));
            rb.AddRelativeForce(thrust * thrustPower, ForceMode.Acceleration);
        }

        private void HandleRotation()
        {
            float yaw = Input.GetAxis("Mouse X") * rotationSpeed;
            float pitch = -Input.GetAxis("Mouse Y") * rotationSpeed;
            float roll = Input.GetKey(KeyCode.Q) ? 1f : Input.GetKey(KeyCode.E) ? -1f : 0f;
            rb.AddRelativeTorque(new Vector3(pitch, yaw, roll) * rotationSpeed, ForceMode.VelocityChange);
        }
    }
}
