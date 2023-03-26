using UnityEngine;

[RequireComponent(typeof(ShootingController))]
public class EnemyRanged : Enemy
{
    [Header("Speed Control")]
    [SerializeField, Range(1, 10)] private float moveSpeed;
    [SerializeField, Range(5, 45)] private float turnSpeed;
    [Space(5)]
    
    [SerializeField] private float distanceToStop = 3;

    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
    }

    private void Update()
    {
        if (_playerTransform == null) return;
        if (Vector3.Distance(_playerTransform.position, transform.position) >= distanceToStop)
            Movement();

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
        return;
    }
}
