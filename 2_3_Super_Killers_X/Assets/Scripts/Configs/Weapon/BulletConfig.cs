using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Weapon/Gun/Bullet")]
public class BulletConfig : ScriptableObject
{
    [field: SerializeField, Range(50, 100)] public float StartBulletImpactForce { get; private set; }
    [field: SerializeField] public float BulletImpactDamage { get; private set; }
    [field: SerializeField] public GameObject BulletImpactEffect { get; private set; }
}