using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class BirdController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private CircleCollider2D _circleCollider;

    private bool _hasBeenLaunched;
    private bool _shouldFollowDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _rigidbody.isKinematic = true;
        _circleCollider.enabled = false;
    }

    private void FixedUpdate()
    {
        if (_hasBeenLaunched && _shouldFollowDirection)
            transform.right = _rigidbody.velocity;
    }

    public void LaunchBird(Vector2 direction, float impulseForce)
    {
        _rigidbody.isKinematic = false;
        _circleCollider.enabled = true;

        _rigidbody.AddForce(direction * impulseForce, ForceMode2D.Impulse);

        _hasBeenLaunched = true;
        _shouldFollowDirection = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _shouldFollowDirection = false;
    }
}
