using UnityEngine;

[RequireComponent(typeof(ShootingController))]
public class EnemyRanged : Enemy
{
    private void FixedUpdate()
    {
        if (_playerTransform == null) return;
        if (Vector3.Distance(_playerTransform.position, transform.position) >= _distanceToStop)
            Movement();

        RotateTowardsPlayer();
    }

    public override void Attack() {  }
}
