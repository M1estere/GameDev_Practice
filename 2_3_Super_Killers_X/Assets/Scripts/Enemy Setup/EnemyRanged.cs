using UnityEngine;
using Zenject;

public class EnemyRanged : Enemy
{
    [Inject] private RangedEnemyConfig _rangedEnemyConfig;

    [Inject] private ShootingController _shootingController;
    
    public override void FixedTick()
    {
        Vector3 playerPosition = _player.transform.position;
        playerPosition.y = 0;

        Vector3 position = transform.position;
        position.y = 0;
        
        Vector3 force = (playerPosition - position).normalized * _rangedEnemyConfig.MovingForceMultiplier;
        if (Vector3.Distance(_player.transform.position, transform.position) >= _rangedEnemyConfig.DistanceToStop)
            Move(force);
        else
            Attack();
        
        RotateTowardsPlayer(force);
    }

    public override void Move(Vector3 force)
    {
        _rigidbody.AddForce(force, ForceMode.Force);
        _shootingController.enabled = false;
    }

    public override void RotateTowardsPlayer(Vector3 force) => 
        _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, Quaternion.LookRotation(force), _rangedEnemyConfig.RotationInterpolation * Time.deltaTime);

    
    public override void Attack() => _shootingController.enabled = true;
}