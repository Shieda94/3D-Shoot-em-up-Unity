using System;
using UnityEngine;

namespace Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform spawnPoint;

        public GameObject PlayerInstance { get; private set; }
        public Transform PlayerTransform { get; private set; }
        
        public event Action<GameObject> OnPlayerSpawned;

        public GameObject SpawnPlayer()
        {
            if (PlayerInstance != null)
            {
                Debug.LogWarning(
                    "PlayerSpawner: A player instance already exists.", 
                    this
                );
                
                return PlayerInstance;
            }
            
            if (playerPrefab == null)
            {
                Debug.LogError(
                    "PlayerSpawner: Player prefab is missing.",
                    this
                );

                return null;
            }

            if (spawnPoint == null)
            {
                Debug.LogError(
                    "PlayerSpawner: Spawn point is missing.",
                    this
                );

                return null;
            }

            PlayerInstance = Instantiate(
                playerPrefab,
                spawnPoint.position,
                spawnPoint.rotation
            );

            PlayerTransform = PlayerInstance.transform;

            OnPlayerSpawned?.Invoke(PlayerInstance);
            return PlayerInstance;
        }
        
        public void ClearPlayerReference()
        {
            PlayerInstance = null;
            PlayerTransform = null;
        }
    }
}
