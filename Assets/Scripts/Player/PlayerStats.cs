using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerConfig config;

        [Header("Runtime Stats")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float deceleration;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float maxHealth;
        [SerializeField] private float pickupRadius;
        [SerializeField] private float contactDamageCooldown;

        public float MoveSpeed => moveSpeed;
        public float Acceleration => acceleration;
        public float Deceleration => deceleration;
        public float RotationSpeed => rotationSpeed;
        public float MaxHealth => maxHealth;
        public float PickupRadius => pickupRadius;
        public float contactDamage => contactDamageCooldown;
        
        private void Awake()
        {
            if (config == null)
            {
                Debug.LogError(
                    "PlayerStats: PlayerConfig is missing.",
                    this
                );

                return;
            }

            InitializeFromConfig();
        }

        private void InitializeFromConfig()
        {
            moveSpeed = config.moveSpeed;
            acceleration = config.acceleration;
            deceleration = config.deceleration;
            rotationSpeed = config.rotationSpeed;
            maxHealth = config.maxHealth;
            pickupRadius = config.pickupRadius;
            contactDamageCooldown = config.contactDamageCooldown;
        }
    }
}