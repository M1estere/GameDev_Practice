using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class GunContentBlock : MonoBehaviour
{
    public GunConfig BlockGun { get; set; }

    [Header("Block Setup")] 
    [SerializeField] private Image _gunIcon;

    [SerializeField] private TMP_Text _gunClipBullets;
    [SerializeField] private TMP_Text _gunName;
    [SerializeField] private TMP_Text _gunReloadTime;
    [SerializeField] private TMP_Text _gunDelay;

    private void Start() => Setup();
    private void Setup()
    {
        _gunIcon.sprite = BlockGun.GunUIIcon;

        _gunName.text = BlockGun.GunName;

        _gunClipBullets.text = "Capacity: " + BlockGun.ClipBulletsAmount;
        _gunReloadTime.text = "R. Time: " + BlockGun.ReloadTime;
        _gunDelay.text = "D. Time: " + BlockGun.ShootDelay;

        gameObject.name = _gunName.text + " block";
    }
}