using UnityEngine;

public class EnemyShootingController : MonoBehaviour
{
    public bool CanShoot { get; set; }

    [Header("Graphics Setup")]
    [SerializeField] private Transform _gunTip;
    [Space(2)]

    [SerializeField] private GameObject _bulletObject;
    [Space(5)]

    [SerializeField] private float _shootDelay;

    [SerializeField] private AudioSource _shootSource;

    private float _lastShotTime = 3;

    private void Update()
    {
        if (!CanShoot || Time.timeScale != 1) return;

        if (Time.time > _shootDelay + _lastShotTime)
        {
            Shoot();

            _lastShotTime = Time.time;
        }
    }

    private void Shoot()
    {
        _shootSource.Play();
        EnemyBulletController bullet = Instantiate(_bulletObject, _gunTip.position, Quaternion.identity).GetComponent<EnemyBulletController>();
        bullet.SetDirectionVector(_gunTip.forward);

        Destroy(bullet, 60);
    }
}
