using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxEnemies = 50;

    [Header("Spawn Area")]
    [SerializeField] private float spawnDistance = 15f;

    private float spawnTimer;
    private int currentEnemies;
    private Transform playerTarget;
    private bool isSpawning;

    public int CurrentEnemies => currentEnemies;
    public bool IsSpawning => isSpawning;

    public void StartSpawning(GameObject player)
    {
        if (player == null)
        {
            Debug.LogError("EnemySpawner: Player target is missing.", this);
            return;
        }

        if (enemyPrefab == null)
        {
            Debug.LogError("EnemySpawner: Enemy prefab is missing.", this);
            return;
        }

        playerTarget = player.transform;
        spawnTimer = 0f;
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
        playerTarget = null;
    }

    private void Update()
    {
        if (!isSpawning || playerTarget == null)
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
                playerTarget
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
            playerTarget.position
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
