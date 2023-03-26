using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract void Movement();
    public abstract void RotateTowardsPlayer();
    public abstract void Attack();
}
