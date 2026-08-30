using UnityEngine;

namespace World
{
    public class MapGenerator : MonoBehaviour
    {
        [Header("Map")]
        [SerializeField] private GameObject mapPrefab;
        [SerializeField] private Vector3 spawnPosition = new Vector3(0f, -1f, 0f);
        [SerializeField, Range(0, 31)] private int mapLayer = 6;

        public GameObject CurrentMap { get; private set; }

        public GameObject GenerateMap()
        {
            if (mapPrefab == null)
            {
                Debug.LogError(
                    "MapGenerator : aucun prefab de map n'est assigné.",
                    this
                );

                return null;
            }

            if (CurrentMap != null)
            {
                Debug.LogWarning(
                    "MapGenerator : une map existe déjà.",
                    this
                );

                return CurrentMap;
            }

            CurrentMap = Instantiate(
                mapPrefab,
                spawnPosition,
                Quaternion.identity,
                transform
            );

            SetLayerRecursively(CurrentMap.transform, mapLayer);

            return CurrentMap;
        }

        private static void SetLayerRecursively(Transform root, int layer)
        {
            root.gameObject.layer = layer;

            foreach (Transform child in root)
            {
                SetLayerRecursively(child, layer);
            }
        }
    }
}
