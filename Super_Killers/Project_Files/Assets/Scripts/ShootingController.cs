using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ShootingGraphics))]
public class ShootingController : MonoBehaviour
{
    private enum ShootingCreature
    {
        Bot, 
        Player,
    }

    [SerializeField] private ShootingCreature type;
    [Space(5)]
    
    [SerializeField] private GameObject gunGFX;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask mask;
    [Space(5)]

    [SerializeField] private Gun currentGun;

    private ShootingGraphics _graphics;
    
    private int _spentBullets = 0;
    private float _lastShotTime = 0;
    private bool _isReloading = false;

    private UiUpdater _updater;

    private void Start()
    {
        if (type == ShootingCreature.Player) currentGun = CheckGun();
        
        _graphics = GetComponent<ShootingGraphics>();
        if (type == ShootingCreature.Player)
        {
            _updater = FindObjectOfType<UiUpdater>();
            _updater.UpdateAmmo(currentGun.ClipBulletsAmount - _spentBullets, currentGun.ClipBulletsAmount);

            _updater.UpdateIcon(currentGun.GunUIIcon);
        }
    }

    private void Update()
    {
        if (_isReloading == true) return;

        if (Time.time > currentGun.ShootDelay + _lastShotTime) 
        {
            _lastShotTime = Time.time;

            if (CheckBullets() == true) Shoot();
            if (CheckBullets() == false) Reload();
        }
    }

    private Gun CheckGun()
    {
        Gun temp = PlayerPrefs.GetInt("Gun") switch
        {
            2 => Resources.Load<Gun>("Guns/Fastel"),
            3 => Resources.Load<Gun>("Guns/Glock"),
            4 => Resources.Load<Gun>("Guns/Rev"),
            _ => Resources.Load<Gun>("Guns/Beretta")
        };

        return temp;
    }
    
    private bool CheckBullets() => _spentBullets < currentGun.ClipBulletsAmount;
    private void Reload() => StartCoroutine(ReloadCoroutine());

    private IEnumerator ReloadCoroutine()
    {
        _isReloading = true;
        if (type == ShootingCreature.Player) _updater.SetReload(currentGun.ReloadTime);
        
        yield return new WaitForSeconds(currentGun.ReloadTime);
        
        _spentBullets = 0;
        _isReloading = false;
        
        if (type == ShootingCreature.Player)
            _updater.UpdateAmmo(currentGun.ClipBulletsAmount - _spentBullets, currentGun.ClipBulletsAmount);
    }
    
    private void Shoot()
    {
        if (Physics.Raycast(gunGFX.transform.position, gunGFX.transform.forward, out var hit, maxDistance, mask))
        {
            _graphics.Shoot(hit);
            if (hit.collider.TryGetComponent(out Health health))
            {
                if (health.TakeDamage(25) == true)
                    KillCounter.KillCount++;
            }
            _spentBullets++;
            
            if (type == ShootingCreature.Player)
                _updater.UpdateAmmo(currentGun.ClipBulletsAmount - _spentBullets, currentGun.ClipBulletsAmount);
        }
    }
}
