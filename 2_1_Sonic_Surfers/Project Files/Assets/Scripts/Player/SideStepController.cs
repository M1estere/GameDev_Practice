using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshTrail))]
[RequireComponent(typeof(PlayerController))]
public class SideStepController : MonoBehaviour
{
    [Header("General Setup")]
    [SerializeField] private float _sideStepLength = 3;
    [SerializeField] private float _sideStepDuration = .2f;
    [Space(2)] 
    
    [Header("Audio Setup")]
    [SerializeField] private AudioSource _sideStepAudio;

    private PlayerController _playerController;
    private MeshTrail _meshTrail;
    
    private bool _canMoveRight = true;
    private bool _canMoveLeft = true;

    private void Awake() => _playerController = GetComponent<PlayerController>();
    private void Start() => _meshTrail = GetComponent<MeshTrail>();
    
    public void SideStep(int direction)
    {
        if (direction == 1 && _canMoveLeft == false) return;
        if (direction == -1 && _canMoveRight == false) return;
        
        _meshTrail.Trail();
        
        transform.DOMoveZ(transform.position.z + (_sideStepLength * direction), _sideStepDuration, false);
        StartCoroutine(nameof(StepDelay));
    }
    
    private IEnumerator StepDelay()
    {
        try
        {
            _sideStepAudio.Play();
        }
        catch (NullReferenceException exception)
        {
            Debug.Log(gameObject.name + " has no side step audio");
        }
        
        _playerController.ChangeSideMoving(true);
        yield return new WaitForSeconds(_sideStepDuration);
        _playerController.ChangeSideMoving(false);
        
        CheckBorders();
    }
    
    private void CheckBorders()
    {
        float zPosition = transform.position.z;
        
        _canMoveLeft = zPosition != _sideStepLength;
        _canMoveRight = zPosition != -_sideStepLength;
    }
}