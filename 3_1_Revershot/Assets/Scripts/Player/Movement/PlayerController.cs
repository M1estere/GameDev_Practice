using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [Space(5)]

    [SerializeField] private float _moveSpeed;
    [Space(5)]

    [SerializeField] private AudioSource _stepsSource;
    [SerializeField] private AudioClip[] _steps;

    private float _footstepTimer = 0f;

    private Rigidbody _rigidbody;
    private Vector2 _moveVector = Vector2.zero;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    private void FixedUpdate() => Movement();

    private void Update() => HandleFootsteps();

    private void HandleFootsteps()
    {
        if (_moveVector == Vector2.zero) return;

        _footstepTimer -= Time.deltaTime;

        if (_footstepTimer <= 0)
        {
            _stepsSource.PlayOneShot(_steps[Random.Range(0, _steps.Length)]);
            _footstepTimer = 0.4f;
        }
    }

    private void Movement()
    {
        Vector2 axis = _moveVector * _moveSpeed;
        Vector3 forwardVector = new Vector3(-_cameraTransform.right.z, 0, _cameraTransform.right.x);
        Vector3 desiredDir = (forwardVector * axis.x + _cameraTransform.right * axis.y + Vector3.up * _rigidbody.velocity.y);

        _rigidbody.velocity = desiredDir;
    }

    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(_rigidbody.velocity.x, _rigidbody.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = _rigidbody.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    public void SetInput(Vector2 inputVector) => _moveVector = inputVector;
}
