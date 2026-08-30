using UnityEngine;

namespace World
{
    public abstract class EnvironmentRule : ScriptableObject
    {
        [Header("Prefab")]
        public GameObject prefab;

        [Header("Placement")]
        [Min(0)] public int amount = 10;
        [Min(0f)] public float minimumSpacing = 2f;
        public float verticalOffset;
        public bool randomizeRotation = true;
    }
}
