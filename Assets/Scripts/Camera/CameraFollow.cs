using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CameraConfig config;
    [SerializeField] private Transform target;

    private Vector3 velocity;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(config.rotation);
    }

    private void LateUpdate()
    {
        if (target == null || config == null)
            return;

        Vector3 desiredPosition = target.position + config.offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            config.followSmoothTime
        );
    }
}