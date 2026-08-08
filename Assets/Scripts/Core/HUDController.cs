using TMPro;
using UnityEngine;

namespace UI
{
    public class HUDController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Player.PlayerSpawner playerSpawner;
        [SerializeField] private EnemySpawner enemySpawner;

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI experienceText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI enemyCountText;

        private Player.PlayerHealth playerHealth;
        private Player.PlayerProgression playerProgression;

        private void Update()
        {
            UpdateEnemyCount();

            TryInitializePlayer();
        }

        private void TryInitializePlayer()
        {
            if (playerHealth != null &&
                playerProgression != null)
            {
                return;
            }

            if (playerSpawner == null)
                return;

            if (playerSpawner.PlayerInstance == null)
                return;

            playerHealth =
                playerSpawner.PlayerInstance
                    .GetComponent<Player.PlayerHealth>();

            playerProgression =
                playerSpawner.PlayerInstance
                    .GetComponent<Player.PlayerProgression>();

            if (playerHealth == null)
            {
                Debug.LogError(
                    "HUDController: PlayerHealth not found on Player.",
                    this
                );
            }

            if (playerProgression == null)
            {
                Debug.LogError(
                    "HUDController: PlayerProgression not found on Player.",
                    this
                );
            }

            if (playerHealth != null)
            {
                playerHealth.OnHealthChanged += UpdateHealth;

                UpdateHealth(
                    playerHealth.CurrentHealth
                );
            }

            if (playerProgression != null)
            {
                playerProgression.ExperienceChanged += UpdateExperience;
                playerProgression.LevelUp += UpdateLevel;

                UpdateExperience(
                    playerProgression.CurrentExperience
                );

                UpdateLevel(
                    playerProgression.CurrentLevel
                );
            }
        }

        private void OnDestroy()
        {
            if (playerHealth != null)
            {
                playerHealth.OnHealthChanged -= UpdateHealth;
            }

            if (playerProgression != null)
            {
                playerProgression.ExperienceChanged -= UpdateExperience;
                playerProgression.LevelUp -= UpdateLevel;
            }
        }

        private void UpdateHealth(float currentHealth)
        {
            if (playerHealth == null)
                return;

            float percent =
                currentHealth / playerHealth.MaxHealth * 100f;

            healthText.text =
                $"Health : {Mathf.RoundToInt(percent)}%";
        }

        private void UpdateExperience(int currentExperience)
        {
            if (playerProgression == null)
                return;

            experienceText.text =
                $"XP : {currentExperience} / {playerProgression.ExperienceToNextLevel}";
        }

        private void UpdateLevel(int currentLevel)
        {
            levelText.text =
                $"Level : {currentLevel}";
        }

        private void UpdateEnemyCount()
        {
            if (enemySpawner == null)
                return;

            enemyCountText.text =
                $"Enemies : {enemySpawner.CurrentEnemies}";
        }
    }
}