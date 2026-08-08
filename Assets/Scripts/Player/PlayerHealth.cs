using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerStats))]
    public class PlayerHealth : MonoBehaviour
    {
        private PlayerStats stats;

        public float CurrentHealth { get; private set; }
        public float MaxHealth => stats.MaxHealth;

        public event Action<float> OnHealthChanged;
        public event Action OnDeath;

        private void Awake()
        {
            stats = GetComponent<PlayerStats>();

            CurrentHealth = stats.MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (damage <= 0f)
                return;

            CurrentHealth = Mathf.Max(
                0f,
                CurrentHealth - damage
            );

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
                stats.MaxHealth,
                CurrentHealth + amount
            );

            OnHealthChanged?.Invoke(CurrentHealth);
        }

        public void RestoreFullHealth()
        {
            CurrentHealth = stats.MaxHealth;

            OnHealthChanged?.Invoke(CurrentHealth);
        }
    }
}