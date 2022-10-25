using UnityEngine;

public class TurretFollowPlayer : MonoBehaviour
{
    private enum ShootingStyle
    {
        AutoShooting,
        Manual,
    }

    [Header("Main")] 
    [SerializeField] private bool _isActive = true;
    [Space(3)]
    
    [SerializeField] private ShootingStyle _style;
    [SerializeField, Tooltip("Position to follow")] private Transform _followPoint;
    [Space(7)]
    
    [Header("Speeds")]
    [SerializeField] private float _followSpeed;
    [SerializeField] private float _rotateSpeed;
    [Space(7)] 
    
    [Header("Enemy spotting")] 
    [SerializeField, Range(5, 15)] private float _lookRadius;
    private bool _caughtEnemy = false;
    private Transform _currentSpottedEnemy;
    [Space(7)] 
    
    [Header("Shooting")] 
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _shootPoint;
    [Space(2)]
    
    [SerializeField] private float _fireRate = 1f;
    private float _fireCountdown = 0f;
    
    private SphereCollider _sphereCollider;
    
    private void Awake()
    {
        _sphereCollider = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
        _sphereCollider.isTrigger = true;
        _sphereCollider.radius = _lookRadius;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _followPoint.position, Time.deltaTime * _followSpeed);
        if (!_caughtEnemy) transform.rotation = Quaternion.Lerp(transform.rotation, _followPoint.rotation, Time.deltaTime * _rotateSpeed);
        else if (_isActive) LookAtEnemy();
    }

    private void LookAtEnemy()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _currentSpottedEnemy.rotation, Time.deltaTime * _rotateSpeed);
        if (_fireCountdown <= 0f)
        {
            if (_style == ShootingStyle.AutoShooting)
            {
                Shoot();
            } else
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    Shoot();
                }
            }
            _fireCountdown = 1f / _fireRate;
        }

        _fireCountdown -= Time.deltaTime; // decrease with every second
    }

    private void Shoot()
    {
        var projectile = Instantiate(_projectile, _shootPoint.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Seek(_currentSpottedEnemy);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var otherObject = other.gameObject;
        if (otherObject.CompareTag("Enemy"))  // found enemy
        {
            if (!_caughtEnemy)  // no current enemy
            {
                _currentSpottedEnemy = other.transform;
                _caughtEnemy = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var otherObject = other.gameObject;
        if (otherObject.CompareTag("Enemy"))
        {
            if (_caughtEnemy)
            {
                _currentSpottedEnemy = null;
                _caughtEnemy = false;
            }
        }
    }
}
