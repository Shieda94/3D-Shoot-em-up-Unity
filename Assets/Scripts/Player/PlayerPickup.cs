using UnityEngine;

namespace Player
{
    public class PlayerPickup : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private LayerMask pickableLayer;

        private PlayerStats stats;

        private void Awake()
        {
            stats = GetComponent<PlayerStats>();
        }

        private void Update()
        {
            DetectPickables();
        }

        private void DetectPickables()
        {
            Collider[] colliders = Physics.OverlapSphere(
                transform.position,
                stats.PickupRadius,
                pickableLayer
            );

            foreach (Collider collider in colliders)
            {
                Pickable.Pickable pickable =
                    collider.GetComponent<Pickable.Pickable>();

                if (pickable == null)
                    continue;

                pickable.Pickup(gameObject);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (stats == null)
                return;

            Gizmos.color = Color.yellow;

            Gizmos.DrawWireSphere(
                transform.position,
                stats.PickupRadius
            );
        }
    }
}