using UnityEngine;

[CreateAssetMenu(fileName = "Shooting Config", menuName = "Weapon/Gun/Shooting Config", order = 1)]
public class ShootingConfig : ScriptableObject
{
    [field: SerializeField] public GameObject BulletPrefab { get; private set; }
}
