using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class SlideController : MonoBehaviour
{
    [SerializeField] private float _slideDuration = 1;
    [SerializeField] private float _speedIncrement = 8;
    [Space(5)]
    
    [SerializeField] private AudioSource _slideAudio;
    [Space(5)]
    
    [Header("Trail Setup")]
    [SerializeField] private GameObject _trail;
    [SerializeField] private Color _trailColor;
    
    private PlayerController _playerController;

    private void Awake() => _playerController = GetComponent<PlayerController>();
    public void Slide() => StartCoroutine(SlideCoroutine());
    
    private IEnumerator SlideCoroutine()
    {
        TrailRenderer trail = Instantiate(_trail, 
                                    transform.position + new Vector3(0, .5f, 0), 
                                           Quaternion.identity, 
                                           transform)
                                           .GetComponent<TrailRenderer>();
        trail.endColor = _trailColor;
        trail.startColor = _trailColor;
        trail.time = _slideDuration + .5f;
        
        Destroy(trail.gameObject, _slideDuration);
        
        _slideAudio.Play();
        
        _playerController.ChangeJump(true);
        _playerController.IsSliding = true;

        _playerController.ChangeSpeed(_speedIncrement);
        yield return new WaitForSeconds(_slideDuration);
        _playerController.ChangeSpeed(-_speedIncrement);

        _playerController.ChangeJump(false);
        _playerController.IsSliding = false;
    }
}
