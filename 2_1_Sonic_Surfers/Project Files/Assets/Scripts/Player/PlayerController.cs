using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(JumpController))]
[RequireComponent(typeof(SlideController))]
[RequireComponent(typeof(SideStepController))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public bool IsSliding { get; set; }
    
    [Header("General Setup")]
    [SerializeField, Range(15, 50)] private float _maxSpeed = 25; 
    [SerializeField] private float _speedIncreaseDivider = 5;
    [Space(5)]

    private Animator _animator;
    private JumpController _jumpController;
    private SlideController _slideController;
    private SideStepController _sideStepController;

    private float _playerMoveSpeed = 15;
    private float _time = 0;
    
    private bool _isMovingSides = false;
    private bool _jumping;

    public void ChangeJump(bool state)
    {
        _isMovingSides = state;
        _jumping = state;
    } 
    
    public void ChangeSideMoving(bool state)
    {
        _isMovingSides = state;
    } 

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _jumpController = GetComponent<JumpController>();
        _slideController = GetComponent<SlideController>();
        _sideStepController = GetComponent<SideStepController>();
    }

    private void Update()
    {
        SpeedControl();
        AnimationSpeedControl();

        if (Input.GetKeyDown(KeyCode.D)) SideStep(-1);
        if (Input.GetKeyDown(KeyCode.A)) SideStep(1);

        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetKeyDown(KeyCode.S)) Slide();
    }

    public void ChangeSpeed(float diff) => _playerMoveSpeed += diff;
    private void SpeedControl()
    {
        Globals.CurrentMoveSpeed = _playerMoveSpeed;

        _time += Time.deltaTime / _speedIncreaseDivider;
        if (_playerMoveSpeed < _maxSpeed)
            _playerMoveSpeed = Mathf.Lerp(_playerMoveSpeed, _maxSpeed, _time);
    }

    private void AnimationSpeedControl()
    {
        if (_time * (_speedIncreaseDivider / 100) < 1) _animator.SetFloat("Speed", _time * (_speedIncreaseDivider / 100));
        _animator.SetBool("Jump", _jumping == true);
    }
    
    public void Jump()
    {
        if (_jumping == true || _isMovingSides == true) return;
        
        _jumpController.Jump();
    }
    
    public void Slide()
    {
        if (IsSliding == true || _isMovingSides == true) return;
        
        _slideController.Slide();
    }

    public void SideStep(int direction)
    {
        if (_isMovingSides == true) return;
        
        _sideStepController.SideStep(direction);
    }
}
