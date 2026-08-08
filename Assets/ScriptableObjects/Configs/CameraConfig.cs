using UnityEngine;

[CreateAssetMenu(menuName = "Game/Configs/Camera")]
public class CameraConfig : ScriptableObject
{
    [Header("Position")]
    public Vector3 offset = new Vector3(0f, 15f, -10f);

    [Header("Follow")]
    [Min(0.01f)]
    public float followSmoothTime = 0.15f;

    [Header("Rotation")]
    public Vector3 rotation = new Vector3(50f, 0f, 0f);
}