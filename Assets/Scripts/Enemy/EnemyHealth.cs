using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BasicEnemy1 config;

    public float CurrentHealth { get; private set; }

    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        CurrentHealth = config.maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0f)
            return;

        CurrentHealth = Mathf.Max(0f, CurrentHealth - damage);

        OnHealthChanged?.Invoke(CurrentHealth);

        if (CurrentHealth <= 0f)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    public void Heal(float amount)
    {
        if (amount <= 0f)
            return;

        CurrentHealth = Mathf.Min(
            config.maxHealth,
            CurrentHealth + amount
        );

        OnHealthChanged?.Invoke(CurrentHealth);
    }
}