using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [Header("Frog Jump Setup")]
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpDuration;
    [Space(5)] 
    
    private Animator _animator;
    
    private float _jumpDelay;
    private float _startJumpDelay;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        
        _jumpDelay = Random.Range(2f, 5f);
        _startJumpDelay = _jumpDelay;
    }

    private void Update()
    {
        if (_startJumpDelay <= 0)
        {
            Jump();
            
            _jumpDelay = Random.Range(2f, 5f);
            _startJumpDelay = _jumpDelay;
        }
        
        _startJumpDelay -= Time.deltaTime;
    }

    private void Jump()
    {
        Vector3 position = transform.position;
        Vector3 jumpEndPosition = new Vector3(position.x - (Globals.CurrentMoveSpeed * _jumpDuration * Globals.Random.Next(2, 3)), 
                                                position.y,
                                                position.z);
        
        transform.DOJump(jumpEndPosition, _jumpHeight, 1, _jumpDuration, false).SetEase(Ease.InOutFlash);
        _animator.SetTrigger("Jump");
    }
}
