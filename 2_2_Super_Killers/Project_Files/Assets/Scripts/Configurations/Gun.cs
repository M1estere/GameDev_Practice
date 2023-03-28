using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun")]
public class Gun : ScriptableObject
{
    public string GunName => _gunName;
    public int ClipBulletsAmount => _clipBulletsAmount;
    
    public float ReloadTime => _reloadTime;
    public float ShootDelay => _shootDelay;
    
    public Sprite GunUIIcon => _gunUIIcon;

    [Header("Gun Style Parameters")]
    [SerializeField] private string _gunName;
    [Space(5)]
    
    [Header("Gun Shooting Parameters")]
    [SerializeField] private int _clipBulletsAmount;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _shootDelay;
    [Space(5)]
    
    [Header("Gun UI Parameters")]
    [SerializeField] private Sprite _gunUIIcon;
}
