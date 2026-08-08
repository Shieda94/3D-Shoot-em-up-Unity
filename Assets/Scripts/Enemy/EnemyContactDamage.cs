using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BasicEnemy1 config;

    private float cooldownTimer;

    private void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (cooldownTimer > 0f)
            return;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth == null)
            return;

        playerHealth.TakeDamage(config.damage);

        cooldownTimer = config.contactDamageCooldown;
    }
}