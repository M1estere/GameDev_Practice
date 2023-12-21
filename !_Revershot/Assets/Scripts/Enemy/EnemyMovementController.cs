using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(EnemyShootingController))]
public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private float _distanceToPlayer = 5;
    [SerializeField] private float _rotationSpeed = 10;

    private Transform _playerTransform;

    private NavMeshAgent _navAgent;
    private EnemyShootingController _enemyShootingController;

    private void Awake()
    {
        _playerTransform = FindFirstObjectByType<PlayerInput>().transform;

        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.updateRotation = false;

        _enemyShootingController = GetComponent<EnemyShootingController>();
    }

    private void Update()
    {
        _navAgent.SetDestination(_playerTransform.position);
        RotateTowardsPlayer();

        if (Vector3.Distance(transform.position, _playerTransform.position) < _distanceToPlayer)
        {
            _navAgent.isStopped = true;

            _enemyShootingController.CanShoot = true;
        } else
        {
            _navAgent.isStopped = false;

            _enemyShootingController.CanShoot = false;
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (_playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
    }
}
