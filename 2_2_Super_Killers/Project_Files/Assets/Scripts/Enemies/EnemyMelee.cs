using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyMelee : Enemy
{
    [Header("Speed Control")]
    [SerializeField, Range(1, 10)] private float moveSpeed;
    [SerializeField, Range(15, 45)] private float turnSpeed;
    [Space(5)]
    
    [SerializeField] private float attackDelay;
    private float _lastAttackTime;
    
    private Transform _playerTransform;
    private Animator _animator;

    private void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_playerTransform == null) return;
        if (Vector3.Distance(_playerTransform.position, transform.position) > 2)
        {
            Movement();
        }
        else
        {
            if (Time.time > attackDelay + _lastAttackTime) 
            {
                _lastAttackTime = Time.time;
                Attack();
            }
        }
        
        RotateTowardsPlayer();
    }

    public override void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, moveSpeed * Time.deltaTime);
    }

    public override void RotateTowardsPlayer()
    {
        var dir = _playerTransform.position - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
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
