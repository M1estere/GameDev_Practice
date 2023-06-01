using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Speed Control")]
    [SerializeField, Range(0.1f, 1)] private float _moveSpeed;
    [SerializeField, Range(15, 45)] private float _turnSpeed;
    [Space(5)]
    
    [SerializeField] protected float _distanceToStop = 3;
    
    protected Transform _playerTransform;
    protected Rigidbody _rigidbody;
    
    protected virtual void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    protected void Movement()
    {
        Vector3 direction = _playerTransform.position - transform.position;
            
        Vector3 velocityVector = direction * _moveSpeed;
        _rigidbody.velocity = velocityVector;
    }
    
    protected void RotateTowardsPlayer()
    {
        var dir = _playerTransform.position - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _turnSpeed).eulerAngles;
        
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    
    public abstract void Attack();
}
