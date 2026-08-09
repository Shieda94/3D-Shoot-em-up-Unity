using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameOverUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text experienceText;
        [SerializeField] private Button respawnButton;

        private RunManager runManager;

        private void Awake()
        {
            runManager = FindFirstObjectByType<RunManager>();

            if (runManager == null)
            {
                Debug.LogError(
                    "GameOverUI: No RunManager found.",
                    this
                );

                return;
            }

            gameOverPanel.SetActive(false);

            respawnButton.onClick.AddListener(HandleRespawnClicked);
        }

        private void OnDestroy()
        {
            if (respawnButton != null)
            {
                respawnButton.onClick.RemoveListener(HandleRespawnClicked);
            }
        }

        public void Show(int level, int experience)
        {
            levelText.text = $"Level : {level}";
            experienceText.text = $"XP : {experience}";

            gameOverPanel.SetActive(true);
        }

        public void Hide()
        {
            gameOverPanel.SetActive(false);
        }

        private void HandleRespawnClicked()
        {
            runManager.RestartRun();
        }
    }
}