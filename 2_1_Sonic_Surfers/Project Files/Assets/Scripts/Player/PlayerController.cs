using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(JumpController))]
[RequireComponent(typeof(SlideController))]
[RequireComponent(typeof(MeshTrail))]
public class PlayerController : MonoBehaviour
{
    public bool IsSliding { get; set; }
    
    [Header("Velocity Setup")]
    [SerializeField, Range(15, 50)] private float _maxSpeed = 25; 
    [SerializeField] private float _speedIncreaseDivider = 5;
    [Space(5)]
    
    [Header("Side Step Setup")]
    [SerializeField] private float _sideStepLength = 3;
    [SerializeField] private float _sideStepDuration = .2f;
    [Space(2)] 
    
    [Header("Audio")]
    [SerializeField] private AudioSource _sideStepAudio;

    private Animator _animator;
    private JumpController _jumpController;
    private SlideController _slideController;
    private MeshTrail _meshTrail;
    
    private float _playerMoveSpeed = 15;
    private float _time = 0;
    
    private bool _isMovingSides = false;
    private bool _canMoveRight = true;
    private bool _canMoveLeft = true;
    private bool _jumping;

    public void ChangeJump(bool state)
    {
        _isMovingSides = state;
        _jumping = state;
    } 

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _jumpController = GetComponent<JumpController>();
        _slideController = GetComponent<SlideController>();
        _meshTrail = GetComponent<MeshTrail>();
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
    
    public void SideStep(float direction)
    {
        if (_isMovingSides == true) return;
    
        if (direction == 1 && _canMoveLeft == false) return;
        if (direction == -1 && _canMoveRight == false) return;
        
        _meshTrail.Trail();
        
        transform.DOMoveZ(transform.position.z + (_sideStepLength * direction), _sideStepDuration, false);
        StartCoroutine(nameof(StepDelay));
    }

    private IEnumerator StepDelay()
    {
        _sideStepAudio.Play();
        
        _isMovingSides = true;
        yield return new WaitForSeconds(_sideStepDuration);
        _isMovingSides = false;
        
        CheckBorders();
    }
    
    private void CheckBorders()
    {
        _canMoveLeft = transform.position.z == _sideStepLength ? false : true;
        _canMoveRight = transform.position.z == -_sideStepLength ? false : true;
    }
}
