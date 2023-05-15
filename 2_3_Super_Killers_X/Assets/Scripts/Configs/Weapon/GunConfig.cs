using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun/Gun")]
public class GunConfig : ScriptableObject
{
    [field: Header("Gun Style Parameters")]
    [field: SerializeField] public string GunName { get; private set; }
    [field: Space(5)]
    
    [field: Header("Gun Shooting Parameters")]
    [field: SerializeField] public int ClipBulletsAmount { get; private set; }
    [field: SerializeField] public float ReloadTime { get; private set; }
    [field: SerializeField] public float ShootDelay { get; private set; }
    [field: Space(5)]
    
    [field: Header("Gun UI Parameters")]
    [field: SerializeField] public Sprite GunUIIcon { get; private set; }
}