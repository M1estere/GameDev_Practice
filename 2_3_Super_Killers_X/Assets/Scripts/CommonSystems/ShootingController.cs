using System.Collections;
using UnityEngine;
using Zenject;

public class ShootingController : MonoBehaviour
{
    private enum ShootingCreature { Bot, Player }

    [SerializeField] private ShootingCreature _type;
    [Space(5)] 
    
    [SerializeField] private Transform _bulletSpawnPos;
    [SerializeField] private ShootingConfig _shootingConfig;

    private GunConfig _currentGun;

    [Inject] private UiUpdater _updater;
    
    private int _spentBullets = 0;
    private float _lastShotTime = 0;
    private bool _isReloading = false;

    private void Start()
    {
        if (_type == ShootingCreature.Player)
        {
            _currentGun = CheckGun();
            _updater = FindObjectOfType<UiUpdater>();
            _updater.UpdateAmmo(_currentGun.ClipBulletsAmount - _spentBullets, _currentGun.ClipBulletsAmount);

            _updater.UpdateIcon(_currentGun.GunUIIcon);
        }
        else
        {
            _currentGun = Resources.Load<GunConfig>("Guns/Rev");
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

    private GunConfig CheckGun()
    {
        GunConfig temp = PlayerPrefs.GetInt("Gun") switch
        {
            1 => Resources.Load<GunConfig>("Guns/Fastel"),
            2 => Resources.Load<GunConfig>("Guns/Glock"),
            3 => Resources.Load<GunConfig>("Guns/Minigun"),
            4 => Resources.Load<GunConfig>("Guns/Rev"),
            _ => Resources.Load<GunConfig>("Guns/Beretta")
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
        BulletSystem bullet = Instantiate(_shootingConfig.BulletPrefab, _bulletSpawnPos.position, _bulletSpawnPos.rotation).GetComponent<BulletSystem>();
        bullet.GiveImpulse(_bulletSpawnPos.forward);
        _spentBullets++;
        
        Destroy(bullet, 30);
        
        if (_type == ShootingCreature.Player)
            _updater.UpdateAmmo(_currentGun.ClipBulletsAmount - _spentBullets, _currentGun.ClipBulletsAmount);
    }
}
