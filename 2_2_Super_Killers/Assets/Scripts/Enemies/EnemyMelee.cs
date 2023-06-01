using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyMelee : Enemy
{
    [SerializeField] private float _attackDelay;
    private float _lastAttackTime;

    private Animator _animator;
    
    protected override void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_playerTransform == null) return;
        if (Vector3.Distance(_playerTransform.position, transform.position) > _distanceToStop)
        {
            Movement();
        }
        else
        {
            if (Time.time > _attackDelay + _lastAttackTime) 
            {
                _lastAttackTime = Time.time;
                Attack();
            }
        }
        
        RotateTowardsPlayer();
    }
    
    public override void Attack()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 2))
        {
            _animator.SetTrigger("Attack");
            if (hit.collider.TryGetComponent(out Health health))
                health.TakeDamage(25);
        }
    }
}
