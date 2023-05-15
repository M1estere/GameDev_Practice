using UnityEngine;
using Zenject;

public class RotationTowardsEnemy : MonoBehaviour, ITickable
{
    [Inject] private GameConfig _gameConfig;
    
    private Transform _target;

    [Inject]
    private void Constructor(TickableManager tickableManager) => tickableManager.Add(this);
    public void Tick() => RotateTowardsEnemy();
    
    private void Start() => InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    
    private void RotateTowardsEnemy() => LockOnTarget();
    private void LockOnTarget()
    {
        if (_target == null) return;
        
        Vector3 direction = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _gameConfig.RotationSpeedMultiplier).eulerAngles;
        
        transform.rotation = Quaternion.Euler (0f, rotation.y, 0f);
    }
    
    private void UpdateTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float shortestDist = Mathf.Infinity;
        Enemy nearestEnemy = null;

        foreach(Enemy enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDist)
            {
                shortestDist = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        
        if (nearestEnemy != null && shortestDist <= _gameConfig.EnemySearchRadius)
            _target = nearestEnemy.transform;
        else
            _target = null;
    }
}