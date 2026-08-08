using UnityEngine;

[CreateAssetMenu(menuName = "Game/Weapons/Weapon")]
public class WeaponConfig : ScriptableObject
{
    [Header("Stats")]
    public float damage = 10f;
    public float attackRate = 1f;
    public float range = 10f;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;
}