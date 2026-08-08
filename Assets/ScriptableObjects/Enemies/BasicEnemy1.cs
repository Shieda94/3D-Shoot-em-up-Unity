using UnityEngine;

[CreateAssetMenu(menuName = "Game/Enemies/Basic Enemy 1")]
public class BasicEnemy1 : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float acceleration = 10f;

    [Header("Combat")]
    [Min(0)]
    public float damage = 10f;
    [Min(0)]
    public float contactDamageCooldown = 0.5f;

    [Header("Stats")]
    public float maxHealth = 100f;
}