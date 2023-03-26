using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Basic params")]
    [SerializeField] private float _speed = 70f;
    [SerializeField] private int _damage = 50;
    [Space(7)]
    
    [Header("Add params"), Tooltip("Not necessary")]
    [SerializeField] private GameObject _impactEffect;
    
    private Transform _target;
    
    public void Seek(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        var dir = _target.position - transform.position;
        var distance = _speed * Time.deltaTime;

        if (dir.magnitude <= distance)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distance, Space.World);
        transform.LookAt(_target);
    }

    private void HitTarget()
    {
        var effect = (GameObject)Instantiate(_impactEffect, transform.position, transform.rotation);
        
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}