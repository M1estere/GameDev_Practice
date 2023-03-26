using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ShootingGraphics))]
public class ShootingController : MonoBehaviour
{
    private enum ShootingCreature { Bot, Player }

    [SerializeField] private ShootingCreature _type;
    [Space(5)]
    
    [SerializeField] private GameObject _gunGfx;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _mask;
    [Space(5)]

    [SerializeField] private Gun _currentGun;

    private ShootingGraphics _graphics;
    
    private int _spentBullets = 0;
    private float _lastShotTime = 0;
    private bool _isReloading = false;

    private UiUpdater _updater;

    private void Start()
    {
        if (_type == ShootingCreature.Player) _currentGun = CheckGun();
        
        _graphics = GetComponent<ShootingGraphics>();
        if (_type == ShootingCreature.Player)
        {
            _updater = FindObjectOfType<UiUpdater>();
            _updater.UpdateAmmo(_currentGun.ClipBulletsAmount - _spentBullets, _currentGun.ClipBulletsAmount);

            _updater.UpdateIcon(_currentGun.GunUIIcon);
        }
    }

    private void Update()
    {
        if (_isReloading == true) return;

        if (Time.time > _currentGun.ShootDelay + _lastShotTime) 
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
    
    private bool CheckBullets() => _spentBullets < _currentGun.ClipBulletsAmount;
    private void Reload() => StartCoroutine(ReloadCoroutine());

    private IEnumerator ReloadCoroutine()
    {
        _isReloading = true;
        if (_type == ShootingCreature.Player) _updater.SetReload(_currentGun.ReloadTime);
        
        yield return new WaitForSeconds(_currentGun.ReloadTime);
        
        _spentBullets = 0;
        _isReloading = false;
        
        if (_type == ShootingCreature.Player)
            _updater.UpdateAmmo(_currentGun.ClipBulletsAmount - _spentBullets, _currentGun.ClipBulletsAmount);
    }
    
    private void Shoot()
    {
        if (Physics.Raycast(_gunGfx.transform.position, _gunGfx.transform.forward, out var hit, _maxDistance, _mask))
        {
            _graphics.Shoot(hit);
            if (hit.collider.TryGetComponent(out Health health))
            {
                if (health.TakeDamage(25) == true)
                    KillCounter.KillCount++;
            }
            _spentBullets++;
            
            if (_type == ShootingCreature.Player)
                _updater.UpdateAmmo(_currentGun.ClipBulletsAmount - _spentBullets, _currentGun.ClipBulletsAmount);
        }
    }
}
