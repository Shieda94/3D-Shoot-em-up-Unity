using System;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class EnvironmentGenerator : MonoBehaviour
    {
        private struct OccupiedArea
        {
            public Vector2 Position;
            public float Radius;

            public OccupiedArea(Vector2 position, float radius)
            {
                Position = position;
                Radius = radius;
            }
        }

        [SerializeField] private MapGenerationConfig config;

        public Transform CurrentDecorationsRoot { get; private set; }
        public int LastUsedSeed { get; private set; }

        public bool GenerateDecorations(GameObject map, int seed)
        {
            if (map == null || config == null)
            {
                Debug.LogError("EnvironmentGenerator: map or generation config is missing.", this);
                return false;
            }

            if (config.mapWidth <= 0f || config.mapHeight <= 0f)
            {
                Debug.LogError("EnvironmentGenerator: map dimensions must be greater than zero.", config);
                return false;
            }

            if (CurrentDecorationsRoot != null)
            {
                Debug.LogWarning("EnvironmentGenerator: decorations have already been generated.", this);
                return true;
            }

            LastUsedSeed = seed;
            var random = new System.Random(seed);
            var occupiedAreas = new List<OccupiedArea>();
            var decorationsRoot = new GameObject("Generated Decorations").transform;
            decorationsRoot.SetParent(map.transform, false);
            CurrentDecorationsRoot = decorationsRoot;

            if (config.environmentsRules == null)
            {
                return true;
            }

            foreach (EnvironmentRule rule in config.environmentsRules)
            {
                GenerateRule(rule, random, occupiedAreas, decorationsRoot);
            }

            Debug.Log($"Environment generated with seed {seed} ({decorationsRoot.childCount} objects).", this);
            return true;
        }

        private void GenerateRule(
            EnvironmentRule rule,
            System.Random random,
            List<OccupiedArea> occupiedAreas,
            Transform parent)
        {
            if (rule == null || rule.amount <= 0)
            {
                return;
            }

            if (rule.prefab == null)
            {
                Debug.LogWarning($"EnvironmentGenerator: no prefab assigned to rule '{rule.name}'.", rule);
                return;
            }

            int placedCount = 0;
            int maximumAttempts = Math.Max(1, config.attemptsPerObject) * rule.amount;
            float radius = Mathf.Max(0f, rule.minimumSpacing * 0.5f);

            for (int attempt = 0; attempt < maximumAttempts && placedCount < rule.amount; attempt++)
            {
                Vector2 position = GetRandomPosition(random);

                if (!CanPlace(position, radius, occupiedAreas))
                {
                    continue;
                }

                float yaw = rule.randomizeRotation ? (float)random.NextDouble() * 360f : 0f;
                GameObject instance = Instantiate(rule.prefab, parent);
                instance.transform.localPosition = new Vector3(position.x, rule.verticalOffset, position.y);
                instance.transform.localRotation = Quaternion.Euler(0f, yaw, 0f);

                occupiedAreas.Add(new OccupiedArea(position, radius));
                placedCount++;
            }

            if (placedCount < rule.amount)
            {
                Debug.LogWarning(
                    $"EnvironmentGenerator: placed {placedCount}/{rule.amount} objects for '{rule.name}'. " +
                    "Increase the map size, reduce spacing, or increase attempts per object.",
                    rule
                );
            }
        }

        private Vector2 GetRandomPosition(System.Random random)
        {
            float halfWidth = Mathf.Max(0f, config.mapWidth * 0.5f - config.boundaryPadding);
            float halfHeight = Mathf.Max(0f, config.mapHeight * 0.5f - config.boundaryPadding);
            float x = Mathf.Lerp(-halfWidth, halfWidth, (float)random.NextDouble());
            float z = Mathf.Lerp(-halfHeight, halfHeight, (float)random.NextDouble());
            return new Vector2(x, z);
        }

        private bool CanPlace(Vector2 candidate, float radius, List<OccupiedArea> occupiedAreas)
        {
            float safeDistance = config.playerSafeRadius + radius;

            if (candidate.sqrMagnitude < safeDistance * safeDistance)
            {
                return false;
            }

            foreach (OccupiedArea occupiedArea in occupiedAreas)
            {
                float minimumDistance = radius + occupiedArea.Radius;

                if ((candidate - occupiedArea.Position).sqrMagnitude < minimumDistance * minimumDistance)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
