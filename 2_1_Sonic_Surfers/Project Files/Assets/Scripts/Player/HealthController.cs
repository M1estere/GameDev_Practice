using System;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class HealthController : MonoBehaviour
{
    [Header("Visual Setup")]
    [SerializeField] private ParticleSystem _damageParticles;
    [Space(2)]
    
    [SerializeField] private GameObject _model;
    [Space(5)]
    
    [Header("Audio Setup")]
    [SerializeField] private AudioSource _loseRings;
    [SerializeField] private AudioSource _death;

    private int Rings => GameController.Instance.CurrentRings;
    
    private FinishController _finishController;
    private PlayerController _playerController;

    private void Awake()
    {
        _finishController = FindObjectOfType<FinishController>();
        _playerController = GetComponent<PlayerController>();
    } 

    public void GetDamage()
    {
        if (Rings > 0) LoseRings();
        else Death();
    }

    private void LoseRings()
    {
        try
        {
            _loseRings.Play();
        }
        catch (NullReferenceException exception)
        {
            Debug.Log(gameObject.name + " has no lose rings audio");
        }

        ParticleSystem.EmissionModule emission = _damageParticles.emission;
        emission.SetBurst(0, new ParticleSystem.Burst(0, Rings));
            
        ParticleSystem particles = Instantiate(_damageParticles, transform.position, Quaternion.identity);
            
        Destroy(particles.gameObject, 2);
        GameController.Instance.LoseRings();
    } 

    private void Death()
    {
        _playerController.enabled = false;
        try
        {
            _death.Play();
        }
        catch (NullReferenceException exception)
        {
            Debug.Log(gameObject.name + " has no death audio");
        }
        
        Globals.CurrentMoveSpeed = 0;
        
        _model.SetActive(false);
        Destroy(gameObject, .5f);
        
        _finishController.ShowFinish();
    }
}
