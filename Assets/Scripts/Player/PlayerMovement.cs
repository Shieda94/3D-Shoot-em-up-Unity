using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerStats))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform body;

        private Rigidbody rb;
        private PlayerStats stats;

        private Vector2 moveInput;

        public Vector3 MoveDirection { get; private set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            stats = GetComponent<PlayerStats>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            UpdateMovement();
        }

        private void Update()
        {
            UpdateRotation();
        }

        private void UpdateMovement()
        {
            MoveDirection = new Vector3(
                moveInput.x,
                0f,
                moveInput.y
            ).normalized;

            Vector3 targetVelocity =
                MoveDirection * stats.MoveSpeed;

            Vector3 currentVelocity = new Vector3(
                rb.linearVelocity.x,
                0f,
                rb.linearVelocity.z
            );

            float acceleration =
                MoveDirection.sqrMagnitude > 0.001f
                    ? stats.Acceleration
                    : stats.Deceleration;

            Vector3 newVelocity = Vector3.MoveTowards(
                currentVelocity,
                targetVelocity,
                acceleration * Time.fixedDeltaTime
            );

            rb.linearVelocity = new Vector3(
                newVelocity.x,
                rb.linearVelocity.y,
                newVelocity.z
            );
        }

        private void UpdateRotation()
        {
            if (moveInput.sqrMagnitude < 0.001f)
                return;

            Vector3 direction = new Vector3(
                moveInput.x,
                0f,
                moveInput.y
            ).normalized;

            Quaternion targetBodyRotation =
                Quaternion.LookRotation(direction);

            body.localRotation = Quaternion.RotateTowards(
                body.localRotation,
                targetBodyRotation,
                stats.RotationSpeed * Time.deltaTime
            );
        }
    }
}