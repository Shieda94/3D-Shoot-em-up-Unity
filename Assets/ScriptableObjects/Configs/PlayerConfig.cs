using UnityEngine;

[CreateAssetMenu(menuName = "Game/Configs/Player")]

public class PlayerConfig : ScriptableObject
{
    [Header("Movement")]
    [Min(0)]
    public float moveSpeed = 8f;

    [Min(0)]
    public float acceleration = 25f;

    [Min(0)]
    public float deceleration = 35f;

    [Min(0)]
    public float rotationSpeed = 720f;
    
    [Header("Health")]
    [Min(1)]
    public float maxHealth = 100f;

    [Header("Combat")]
    [Min(0)]
    public float contactDamageCooldown = 0.5f;

    [Header("Collection")]
    [Min(0)]
    public float pickupRadius = 2f;
}
