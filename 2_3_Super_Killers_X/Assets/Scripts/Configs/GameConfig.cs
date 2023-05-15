using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Game Config", fileName = "Game Config")]
public class GameConfig : ScriptableObject
{
    [field: Header("General")]
    [field: SerializeField, Range(30, 144)] public int FrameRate { get; private set; } = 60;
    [field: Space(5)]
    
    [field: Header("Player Setup")]
    [field: SerializeField, Range(1, 75)] public float PlayerMovingForceMultiplier { get; private set; } = 50;
    [field: SerializeField, Range(10, 35)] public float RotationSpeedMultiplier { get; private set; } = 20;
    [field: SerializeField] public float EnemySearchRadius { get; private set; } = 15;
    [field: Space(5)]

    [field: Header("Camera Setup")]
    [field: SerializeField, Range(1, 35)] public float CameraFollowInterpolation { get; private set; } = 15;
    [field: SerializeField] public Vector3 Offset { get; private set; }
}