using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Rigidbody))]
public class JumpController : MonoBehaviour
{
    [Header("Jump Setup")] 
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpDuration;
    [Space(5)]
    
    [Header("Audio Setup")]
    [SerializeField] private AudioSource _jumpAudio;
    
    private PlayerController _playerController;
    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _rigidbody = GetComponent<Rigidbody>();
        
        LockNecessaryConstraints();
    }

    private void LockNecessaryConstraints()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX |
                                 RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | 
                                 RigidbodyConstraints.FreezeRotationZ;   
    }
    
    public void Jump() => StartCoroutine(JumpCoroutine());
    private IEnumerator JumpCoroutine()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        
        try
        {
            _jumpAudio.Play();
        }
        catch (NullReferenceException exception)
        {
            Debug.Log(gameObject.name + " has no jump audio");
        }

        _playerController.ChangeJump(true);
        
        transform.DOJump(new Vector3(transform.position.x, 0, transform.position.z), _jumpHeight, 1, _jumpDuration, false);

        yield return new WaitForSeconds(_jumpDuration);
        
        _playerController.ChangeJump(false);
        
        LockNecessaryConstraints();
    }
}
