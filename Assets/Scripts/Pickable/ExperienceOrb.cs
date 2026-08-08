using UnityEngine;

namespace Pickable
{
    public class ExperienceOrb : Pickable
    {
        [Header("Experience")]
        [SerializeField] private int experienceAmount = 1;

        [Header("Pickup")]
        [SerializeField] private float pickupDelay = 0.5f;

        private bool canBePickedUp;

        private void Awake()
        {
            canBePickedUp = false;
        }

        private void Start()
        {
            Invoke(nameof(EnablePickup), pickupDelay);
        }

        private void EnablePickup()
        {
            canBePickedUp = true;
        }

        public override void Pickup(GameObject picker)
        {
            if (!canBePickedUp)
                return;

            Player.PlayerProgression progression =
                picker.GetComponent<Player.PlayerProgression>();

            if (progression == null)
            {
                Debug.LogWarning(
                    $"No PlayerProgression found on {picker.name}.",
                    picker
                );

                return;
            }

            progression.AddExperience(experienceAmount);

            Destroy(gameObject);
        }
    }
}