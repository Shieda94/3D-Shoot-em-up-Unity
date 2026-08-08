using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform head;

    [Header("Settings")]
    [SerializeField] private float rotationSpeed = 1080f;
    [SerializeField] private LayerMask groundLayer;

    private Camera mainCamera;
    private Vector2 mousePosition;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        RotateHead();
    }

    private void RotateHead()
    {
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
            return;

        Vector3 direction = hit.point - head.position;

        // On ignore la hauteur pour rester sur un plan horizontal
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        head.rotation = Quaternion.RotateTowards(
            head.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}