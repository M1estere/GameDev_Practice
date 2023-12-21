using UnityEngine;

[RequireComponent(typeof(BulletGFX))]
public class BulletController : MonoBehaviour
{
    [SerializeField] private AudioSource _shotSource;
    [SerializeField, Range(10, 55)] private float _traversalSpeed;

    [SerializeField] private GameObject _collisionEffect;

    private Animator _gunAnimator;
    private Transform _gunTipTransform;

    private float _distanceToTipToDestroy = 2.7f;
    private int _animatorActivationTimes = 0;

    private void Start()
    {
        _gunTipTransform = FindFirstObjectByType<GunTip>().transform;
        _gunAnimator = _gunTipTransform.GetComponentInParent<Animator>();

        GetComponent<BulletGFX>().SetGunTip(_gunTipTransform);
    }

    private void Update()
    {
        // shot returned
        if (Vector3.Distance(transform.position, _gunTipTransform.position) < _distanceToTipToDestroy)
        {
            if (_animatorActivationTimes++ < 1)
            {
                _gunAnimator.SetTrigger("Gun_Shot");

                if (Time.timeScale == 1)
                    _shotSource.Play();
            }

            Destroy(gameObject, .5f);
        }

        transform.LookAt(_gunTipTransform);
        transform.Translate(Vector3.forward * _traversalSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyDeath enemyDeath))
        {
            enemyDeath.InitiateDeath();

            GameObject particles = Instantiate(_collisionEffect, transform.position, Quaternion.identity);
            Destroy(particles, 3);
        }
    }
}
