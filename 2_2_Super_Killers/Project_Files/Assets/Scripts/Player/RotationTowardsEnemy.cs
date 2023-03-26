using UnityEngine;

public class RotationTowardsEnemy : MonoBehaviour
{
    [SerializeField, Range(10, 35)] private float _rotateSpeed;
    [Space(5)]
    
    [SerializeField] private float _searchRadius = 15f;
    
    private Transform _target;
    
    private void Start() => InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);

    private void Update() => RotateTowardsEnemy();
    
    private void RotateTowardsEnemy() => LockOnTarget();
    
    private void LockOnTarget()
    {
        if (_target == null) return;
        
        Vector3 direction = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _rotateSpeed).eulerAngles;
        
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
        
        if (nearestEnemy != null && shortestDist <= _searchRadius)
            _target = nearestEnemy.transform;
        else
            _target = null;
    }
}
