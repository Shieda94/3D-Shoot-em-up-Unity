using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI enemyCountText;

    private void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateHealth;
    }

    private void Start()
    {
        UpdateHealth(playerHealth.CurrentHealth);
    }

    private void Update()
    {
        enemyCountText.text = $"Enemies : {enemySpawner.CurrentEnemies}";
    }

    private void UpdateHealth(float currentHealth)
    {
        float percent = currentHealth / playerHealth.MaxHealth * 100f;

        healthText.text = $"Health : {Mathf.RoundToInt(percent)}%";
    }
}