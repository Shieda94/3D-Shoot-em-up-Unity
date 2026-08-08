using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerConfig config;
    [SerializeField] private Transform body;

    private Rigidbody rb;
    private Vector2 moveInput;

    public Vector3 MoveDirection { get; private set; }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

        Vector3 targetVelocity = MoveDirection * config.moveSpeed;

        Vector3 currentVelocity = new Vector3(
            rb.linearVelocity.x,
            0f,
            rb.linearVelocity.z
        );

        float acceleration = MoveDirection.sqrMagnitude > 0.001f
            ? config.acceleration
            : config.deceleration;

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

        Quaternion targetBodyRotation = Quaternion.LookRotation(direction);

        body.localRotation = Quaternion.RotateTowards(
            body.localRotation,
            targetBodyRotation,
            config.rotationSpeed * Time.deltaTime
        );
    }
}