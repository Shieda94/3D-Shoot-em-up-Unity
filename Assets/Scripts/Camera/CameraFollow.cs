using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CameraConfig config;
    [SerializeField] private Player.PlayerSpawner playerSpawner;

    private Vector3 velocity;
    
    private Transform target;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(config.rotation);
    }

    private void LateUpdate()
    {
        if (target == null || config == null)
        {
            if (playerSpawner == null)
                return;

            target = playerSpawner.PlayerTransform;

            if (target == null)
                return;
        }

        Vector3 desiredPosition = target.position + config.offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            config.followSmoothTime
        );
    }
}