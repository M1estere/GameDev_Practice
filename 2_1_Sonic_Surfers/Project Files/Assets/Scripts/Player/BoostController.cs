using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class BoostController : MonoBehaviour
{
    public bool Boosting => _isBoosting;
    
    [Header("General")]
    [SerializeField] private Camera _mainCamera;
    [Space(5)]
    
    [Header("Velocity Setup")]
    [SerializeField, Range(0, 100)] private float _maxBoostValue;
    [Space(5)]

    [SerializeField, Range(10, 30)] private float _speedIncrement;
    [SerializeField, Range(3, 8)] private float _boostDuration;

    [Header("Audio Setup")]
    [SerializeField] private AudioSource _boostScream;
    
    private float CurrentBoostRate => _boostGaugeFill / _maxBoostValue;
    
    private PoseControl _poseControl;
    private BoostUI _ui;
    private PlayerController _playerController;

    private float _boostGaugeFill;
    private bool _isBoosting = false;
    
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        
        _poseControl = FindObjectOfType<PoseControl>();
        _ui = FindObjectOfType<BoostUI>();
    }

    private void Update() => _boostGaugeFill = Mathf.Clamp(_boostGaugeFill, 0, _maxBoostValue);
    
    public void IncreaseBoost(float increment)
    {
        if (_isBoosting == true) return;
        
        _boostGaugeFill += increment;
        
        _ui.UpdateGauge(CurrentBoostRate, _boostDuration);
    }

    public void Boost()
    {
        if (_isBoosting == true) return;
        if (_boostGaugeFill < _maxBoostValue) return;

        _boostGaugeFill = 0;
        _ui.UpdateGauge(CurrentBoostRate, _boostDuration);
        _poseControl.StartPosingCycle();

        Vector3 playerTransform = transform.position;
        transform.position = new Vector3(playerTransform.x, 0, playerTransform.z);
        StartCoroutine(BoostCor());
    }

    private IEnumerator BoostCor()
    {
        yield return new WaitForSecondsRealtime(4.5f);
        
        _isBoosting = true;
        _playerController.ChangeSpeed(_speedIncrement);
        _mainCamera.DOFieldOfView(75, 1.5f);
        
        try
        {
            _boostScream.Play();
        }
        catch (ArgumentNullException exception)
        {
            Debug.Log(gameObject.name + " has no boost audio");
        }
        yield return new WaitForSeconds(_boostDuration);

        _mainCamera.DOFieldOfView(62, .5f);
        _playerController.ChangeSpeed(-_speedIncrement);
        _isBoosting = false;
    }
}
