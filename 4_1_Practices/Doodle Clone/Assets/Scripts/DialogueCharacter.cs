using UnityEngine;

public class DialogueCharacter : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _jumpForce = 3;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float _move;
    private bool _grounded;
    private bool _isFacingRight = true;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _move = Input.GetAxis("Horizontal") * _moveSpeed;
        _animator.SetFloat("Speed", Mathf.Abs(_move));

        CheckFlip();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = _move;

        _rigidbody.velocity = velocity;
    }

    private void Jump()
    {
        if (_grounded)
        {
            _grounded = false;

            Vector2 velocity = _rigidbody.velocity;
            velocity.y = _jumpForce;
            _rigidbody.velocity = velocity;

            _animator.SetBool("IsJumping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 6)
        {
            _grounded = true;
            _animator.SetBool("IsJumping", false);
        }
    }

    private void CheckFlip()
    {
        bool oldState = _isFacingRight;
        if (Input.GetAxis("Horizontal") > 0 && !_isFacingRight)
        {
            _isFacingRight = true;
        }
        else if (Input.GetAxis("Horizontal") < 0 && _isFacingRight)
        {
            _isFacingRight = false;
        }

        transform.localScale =
            new Vector2(oldState != _isFacingRight ? transform.localScale.x * -1 : transform.localScale.x,
                transform.localScale.y);
    }
}
