using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class HealthController : MonoBehaviour
{
    [Header("FX")]
    [SerializeField] private ParticleSystem _damageParticles;
    [Space(5)]
    
    [Header("Model")]
    [SerializeField] private GameObject _model;
    [Space(5)]
    
    [Header("Sounds")]
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
        _loseRings.Play();
        
        ParticleSystem.EmissionModule emission = _damageParticles.emission;
        emission.SetBurst(0, new ParticleSystem.Burst(0, Rings));
            
        ParticleSystem particles = Instantiate(_damageParticles, transform.position, Quaternion.identity);
            
        Destroy(particles.gameObject, 2);
        GameController.Instance.LoseRings();
    } 

    private void Death()
    {
        _playerController.enabled = false;
        _death.Play();
        
        Globals.CurrentMoveSpeed = 0;
        
        _model.SetActive(false);
        Destroy(gameObject, .5f);
        
        _finishController.ShowFinish();
    }
}
