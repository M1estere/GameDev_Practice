using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletSystem : MonoBehaviour
{
    [SerializeField] private BulletConfig _bulletConfig;

    private Rigidbody _rigidbody;
    private Vector3 _direction;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HealthSystem healthSystem) == false) return;
        
        healthSystem.TakeDamage(_bulletConfig.BulletImpactDamage);

        GameObject impactEffect =
            Instantiate(_bulletConfig.BulletImpactEffect, transform.position, Quaternion.identity);
        Destroy(impactEffect, 2);

        Destroy(gameObject);
    }

    public void GiveImpulse(Vector3 direction) => _rigidbody.AddForce(direction * _bulletConfig.StartBulletImpactForce, ForceMode.Impulse);
}