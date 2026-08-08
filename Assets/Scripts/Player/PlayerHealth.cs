using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerConfig config;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => config.maxHealth;

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

    public void RestoreFullHealth()
    {
        CurrentHealth = config.maxHealth;
        OnHealthChanged?.Invoke(CurrentHealth);
    }
}