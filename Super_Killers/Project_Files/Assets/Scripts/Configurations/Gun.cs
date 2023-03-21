using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun", order=1)]
public class Gun : ScriptableObject
{
    public string GunName;
    public int ClipBulletsAmount;
    [Tooltip("Time in seconds")] public float ReloadTime;
    [Tooltip("Time in seconds")] public float ShootDelay;
    public Sprite GunUIIcon;
}
