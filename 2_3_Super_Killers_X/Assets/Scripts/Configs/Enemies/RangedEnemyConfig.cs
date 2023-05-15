using UnityEngine;

[CreateAssetMenu(fileName = "Ranged Enemy", menuName = "Enemies/Ranged Enemy")]
public class RangedEnemyConfig : ScriptableObject
{
    [field: SerializeField, Range(1, 10)] public float DistanceToStop { get; private set; } = 3;
    [field: SerializeField] public float RotationInterpolation { get; private set; }
    [field: SerializeField, Range(70, 120)] public float MovingForceMultiplier { get; private set; } = 80;
}
