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

        private bool isDead;

        private void Awake()
        {
            stats = GetComponent<PlayerStats>();
        }

        private void Start()
        {
            CurrentHealth = stats.MaxHealth;
            OnHealthChanged?.Invoke(CurrentHealth);
        }

        public void TakeDamage(float damage)
        {
            if (damage <= 0f || isDead)
                return;

            CurrentHealth = Mathf.Max(
                0f,
                CurrentHealth - damage
            );

            OnHealthChanged?.Invoke(CurrentHealth);

            if (CurrentHealth <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead)
                return;

            isDead = true;
            OnDeath?.Invoke();
        }

        public void Heal(float amount)
        {
            if (amount <= 0f || isDead)
                return;

            CurrentHealth = Mathf.Min(
                MaxHealth,
                CurrentHealth + amount
            );

            OnHealthChanged?.Invoke(CurrentHealth);
        }

        public void RestoreFullHealth()
        {
            if (isDead)
                return;

            CurrentHealth = MaxHealth;
            OnHealthChanged?.Invoke(CurrentHealth);
        }
    }
}