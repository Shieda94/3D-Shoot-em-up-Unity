using Player;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class RunManager : MonoBehaviour
    {
        private PlayerSpawner playerSpawner;
        private GameOverUI gameOverUI;
        
        private void Awake()
        {
            playerSpawner = FindAnyObjectByType<PlayerSpawner>();
            gameOverUI = FindAnyObjectByType<GameOverUI>();
            
            if (playerSpawner == null)
            {
                Debug.LogError(
                    "RunManager: PlayerSpawner reference is missing.",
                    this
                );

                return;
            }
            
            if (gameOverUI == null)
            {
                Debug.LogError(
                    "RunManager: No GameOverUI found in the scene.",
                    this
                );

                return;
            }

            playerSpawner.OnPlayerSpawned += HandlePlayerSpawned;
        }
        
        private void OnDestroy()
        {
            if (playerSpawner != null)
            {
                playerSpawner.OnPlayerSpawned -= HandlePlayerSpawned;
            }
        }

        private void HandlePlayerSpawned(GameObject player)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            if (playerHealth == null)
            {
                Debug.LogError(
                    "RunManager: Spawned player does not have a PlayerHealth component.",
                    player
                );

                return;
            }

            playerHealth.OnDeath += PlayerDeath;
        }

        public void PlayerDeath()
        {
            Debug.Log("RunManager: Player died.");

            if (playerSpawner.PlayerInstance == null)
                return;

            GameObject player =
                playerSpawner.PlayerInstance;

            PlayerProgression progression =
                player.GetComponent<PlayerProgression>();

            if (progression == null)
            {
                Debug.LogError(
                    "RunManager: Player does not have a PlayerProgression component.",
                    player
                );

                return;
            }

            int finalLevel =
                progression.CurrentLevel;

            int finalExperience =
                progression.CurrentExperience;

            gameOverUI.Show(
                finalLevel,
                finalExperience
            );
            
            Destroy(playerSpawner.PlayerInstance);

            playerSpawner.ClearPlayerReference();
        }
        
        public void RestartRun()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }
}