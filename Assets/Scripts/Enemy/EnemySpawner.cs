using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Player.PlayerSpawner playerSpawner;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxEnemies = 50;

    [Header("Spawn Area")]
    [SerializeField] private float spawnDistance = 15f;

    private float spawnTimer;
    private int currentEnemies;

    public int CurrentEnemies => currentEnemies;

    private void Update()
    {
        // Le Player n'est pas encore prêt.
        if (playerSpawner == null ||
            playerSpawner.PlayerTransform == null)
        {
            return;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            TrySpawnEnemy();
        }
    }

    private void TrySpawnEnemy()
    {
        if (currentEnemies >= maxEnemies)
            return;

        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetSpawnPosition();

        GameObject enemy = Instantiate(
            enemyPrefab,
            spawnPosition,
            Quaternion.identity
        );

        EnemyMovement enemyMovement =
            enemy.GetComponent<EnemyMovement>();

        if (enemyMovement != null)
        {
            enemyMovement.SetTarget(
                playerSpawner.PlayerTransform
            );
        }

        EnemyHealth enemyHealth =
            enemy.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.OnDeath += HandleEnemyDeath;
        }

        currentEnemies++;
    }

    private Vector3 GetSpawnPosition()
    {
        Vector2 randomDirection =
            Random.insideUnitCircle.normalized;

        Vector3 position =
            playerSpawner.PlayerTransform.position
            + new Vector3(
                randomDirection.x,
                0f,
                randomDirection.y
            ) * spawnDistance;

        return position;
    }

    private void HandleEnemyDeath()
    {
        currentEnemies = Mathf.Max(
            0,
            currentEnemies - 1
        );
    }
}