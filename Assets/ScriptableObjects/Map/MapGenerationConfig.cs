using UnityEngine;
using World;

[CreateAssetMenu(fileName = "MapGenerationConfig", menuName = "Scriptable Objects/MapGenerationConfig")]
public class MapGenerationConfig : ScriptableObject
{
    [Header("Size")]
    [Min(0f)] public float mapWidth = 100f;
    [Min(0f)] public float mapHeight = 100f;
    [Min(0f)] public float boundaryPadding = 1f;

    [Header("PlayerInfo")]
    [Min(0f)] public float playerSafeRadius = 3f;
    
    [Header("EnvironmentRule")]
    public EnvironmentRule[] environmentsRules;

    [Header("Generation")]
    [Min(1)] public int attemptsPerObject = 30;
}
