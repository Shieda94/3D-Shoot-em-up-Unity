using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WeaponConfig config;

    private float cooldownTimer;

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            TryAttack();
            cooldownTimer = 1f / config.attackRate;
        }
    }

    private void TryAttack()
    {
        EnemyHealth target = FindClosestEnemy();

        if (target == null)
            return;

        Shoot(target.transform);
    }

    private EnemyHealth FindClosestEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(
            transform.position,
            config.range
        );

        EnemyHealth closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider enemyCollider in enemies)
        {
            EnemyHealth enemy = enemyCollider.GetComponent<EnemyHealth>();

            if (enemy == null)
                continue;

            float distance = Vector3.Distance(
                transform.position,
                enemy.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    private void Shoot(Transform target)
    {
        GameObject projectile = Instantiate(
            config.projectilePrefab,
            transform.position,
            Quaternion.identity
        );

        Projectile projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.Initialize(
                target,
                config.damage,
                config.projectileSpeed
            );
        }
    }
}