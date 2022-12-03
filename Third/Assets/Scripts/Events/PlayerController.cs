using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    
    private Rigidbody2D _rigidbody;
    private Vector2 _moveVector;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_moveVector.x * _moveSpeed, _rigidbody.velocity.y);
    }

    public void SetInput(Vector2 input) => _moveVector = input;
}
