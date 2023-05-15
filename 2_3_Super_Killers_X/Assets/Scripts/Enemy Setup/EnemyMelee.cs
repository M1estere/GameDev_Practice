using UnityEngine;
using Zenject;

public class EnemyMelee : Enemy, IFixedTickable
{
    [Inject] private Rigidbody _rigidbody;
    
    [Inject] private Player _player;

    [Inject] private MeleeEnemyConfig _meleeEnemyConfig;
    
    private float _lastAttackTime;
    
    [Inject]
    private void Constructor(TickableManager tickableManager) => tickableManager.AddFixed(this);

    public void FixedTick()
    {
        Vector3 playerPosition = _player.transform.position;
        playerPosition.y = 0;

        Vector3 position = transform.position;
        position.y = 0;
        
        Vector3 force = (playerPosition - position).normalized * _meleeEnemyConfig.MovingForceMultiplier;
        if (Vector3.Distance(_player.transform.position, transform.position) >= 2)
        {
            Move(force);
        }
        else
        {
            if (Time.time > _meleeEnemyConfig.AttackDelay + _lastAttackTime) 
            {
                _lastAttackTime = Time.time;
                Attack();
            }
        }
        
        RotateTowardsPlayer(force);
    }
    
    public override void Move(Vector3 force) => _rigidbody.AddForce(force, ForceMode.Force);
    public override void RotateTowardsPlayer(Vector3 force) => 
        _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, Quaternion.LookRotation(force), _meleeEnemyConfig.RotationInterpolation * Time.deltaTime);

    public override void Attack()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 2))
        {
            //_animator.SetTrigger("Attack");
            if (hit.collider.TryGetComponent(out HealthSystem health))
                health.TakeDamage(_meleeEnemyConfig.Damage);
        }
    }
}