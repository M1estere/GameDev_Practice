using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class JumpController : MonoBehaviour
{
    [Header("Jump Setup")] 
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpDuration;
    [Space(5)]
    
    [SerializeField] private AudioSource _jumpAudio;
    
    private PlayerController _playerController;

    private void Awake() => _playerController = GetComponent<PlayerController>();
    
    public void Jump() => StartCoroutine(JumpCoroutine());
    private IEnumerator JumpCoroutine()
    {
        _jumpAudio.Play();
        
        _playerController.ChangeJump(true);
        
        transform.DOJump(new Vector3(transform.position.x, 0, transform.position.z), _jumpHeight, 1, _jumpDuration, false);

        yield return new WaitForSeconds(_jumpDuration);
        
        _playerController.ChangeJump(false);
    }
}
