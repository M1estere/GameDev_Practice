using UnityEngine;

[CreateAssetMenu(fileName = "Melee Enemy", menuName = "Enemies/Melee Enemy")]
public class MeleeEnemyConfig : ScriptableObject
{
    [field: SerializeField, Range(70, 150)] public float MovingForceMultiplier { get; private set; } = 90;
    [field: SerializeField, Range(1, 35)] public float RotationInterpolation { get; private set; } = 15;

    [field: SerializeField] public float AttackDelay { get; private set; } = 2;
    
    [field: SerializeField] public float Damage { get; private set; }
}