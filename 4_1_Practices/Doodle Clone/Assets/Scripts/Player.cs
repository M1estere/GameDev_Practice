using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveSpeed = 1;

    [SerializeField] private GameObject _lostScreen;

    private float _move = 1;
    private Rigidbody2D _rigidbody;

    private bool _isFacingRight = true;

    private void Awake()
    {
        Time.timeScale = 1;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _move = Input.GetAxis("Horizontal") * _moveSpeed;

        CheckPlayerVisibility();
        CheckFlip();

        if (transform.position.x < -3)
        {
            transform.position = new Vector3(transform.position.x + 6, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 3)
        {
            transform.position = new Vector3(transform.position.x - 6, transform.position.y, transform.position.z);
        }
    }

    private void CheckFlip()
    {
        bool oldState = _isFacingRight;
        if (Input.GetAxis("Horizontal") > 0 && !_isFacingRight)
        {
            _isFacingRight = true;
        } else if (Input.GetAxis("Horizontal") < 0 && _isFacingRight)
        {
            _isFacingRight = false;
        }

        transform.localScale =
            new Vector2(oldState != _isFacingRight ? transform.localScale.x * -1 : transform.localScale.x,
                transform.localScale.y);
    }

    public void Jump(float force)
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.y = force;
        _rigidbody.velocity = velocity;

        _animator.SetTrigger("Jump");
    }

    private void FixedUpdate()
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = _move;

        _rigidbody.velocity = velocity;
    }

    private void CheckPlayerVisibility()
    {
        Vector3 playerViewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (playerViewportPosition.y < -0.1f)
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("Dead!");

        Time.timeScale = 0;
        _lostScreen.SetActive(true);
    }
}
