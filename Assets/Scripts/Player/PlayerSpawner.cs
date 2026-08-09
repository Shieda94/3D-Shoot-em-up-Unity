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

        private void Start()
        {
            SpawnPlayer();
        }

        public void SpawnPlayer()
        {
            if (PlayerInstance != null)
            {
                Debug.LogError(
                    "PlayerSpawner: A player instance already exists.", 
                    this
                );
                
                return;
            }
            
            if (playerPrefab == null)
            {
                Debug.LogError(
                    "PlayerSpawner: Player prefab is missing.",
                    this
                );

                return;
            }

            if (spawnPoint == null)
            {
                Debug.LogError(
                    "PlayerSpawner: Spawn point is missing.",
                    this
                );

                return;
            }

            PlayerInstance = Instantiate(
                playerPrefab,
                spawnPoint.position,
                spawnPoint.rotation
            );

            PlayerTransform = PlayerInstance.transform;
            
            OnPlayerSpawned?.Invoke(PlayerInstance);
        }
        
        public void ClearPlayerReference()
        {
            PlayerInstance = null;
            PlayerTransform = null;
        }
    }
}