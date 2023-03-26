using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Camera")] [SerializeField] private Transform _camera;
    [Space(7)]

    [Header("Velocity Parameters")] 
    [SerializeField, Tooltip("Enfastening param")] private float _acceleration = 2;
    [SerializeField, Tooltip("Regular speed")] private float _walkSpeed;
    [Space(3)]
    
    [SerializeField, Tooltip("Lowest speed possible")] private float _crawlSpeed;
    [SerializeField] private KeyCode _crawlKey = KeyCode.LeftControl;
    [Space(3)]

    [SerializeField, Tooltip("Also is max run speed")] private float _runSpeed;
    [SerializeField] private KeyCode _runKey = KeyCode.LeftShift;
    [Space(7)]
    
    [SerializeField] private float _rotateSpeed;
    private float _currentSpeed;

    private Animator _animator;
    private const string MAIN_ANIMATION_PARAM = "Speed";
    private Rigidbody _rigidbody;
    
    private Vector3 _inputVector;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_inputVector != Vector3.zero) // if moving
        {
            if (Input.GetKey(_runKey))
                Run();
            else if (Input.GetKey(_crawlKey))
                Crawl();
            else
                Walk();
        } else
            Idle();
        
        // _rigidbody.velocity = _inputVector * _currentSpeed;
        _rigidbody.MovePosition(transform.position + _inputVector * _currentSpeed * Time.deltaTime);
        
        if (_currentSpeed >= _runSpeed) _currentSpeed = _runSpeed;
        
        if (_inputVector != Vector3.zero)
        {
            var toRotation = Quaternion.LookRotation(_inputVector, Vector3.up); // rotating quaternion
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotateSpeed * Time.deltaTime); // applying rotation vector 
        }
    }

    private void Idle()
    {
        _animator.SetFloat(MAIN_ANIMATION_PARAM, 0f, 0.1f, Time.deltaTime);
        _currentSpeed = 0;
    }
    
    private void Walk()
    {
        _animator.SetFloat(MAIN_ANIMATION_PARAM, 0.33f, 0.1f, Time.deltaTime);
        if (_currentSpeed < _walkSpeed)
            _currentSpeed += Time.deltaTime * _acceleration;
        else if (_currentSpeed > _walkSpeed)
            _currentSpeed -= Time.deltaTime * _acceleration;
    }

    private void Run()
    {
        _animator.SetFloat(MAIN_ANIMATION_PARAM, 0.666f, 0.1f, Time.deltaTime);
        if (_currentSpeed < _runSpeed)
            _currentSpeed += Time.deltaTime * _acceleration;
    }

    private void Crawl()
    {
        _animator.SetFloat(MAIN_ANIMATION_PARAM, 1, 0.1f, Time.deltaTime);
        if (_currentSpeed > _crawlSpeed)
            _currentSpeed -= Time.deltaTime * _acceleration;
    }
    
    public void SetInput(float forwardInput, float rightInput)
    {
        var camTransform = _camera.transform;

        var target = forwardInput * camTransform.forward;
        target += rightInput * camTransform.right;
        target.y = 0;
        
        var vel = target.magnitude > 0 ? target : Vector3.zero;

        _inputVector = vel;
    }
}