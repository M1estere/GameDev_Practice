using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyCollision : MonoBehaviour
{
    [SerializeField, Tooltip("To disable before death")] private GameObject _model;
    [Space(5)]
    
    [SerializeField] private GameObject _dieParticles;
    
    private AudioSource _dieAudio;
    private int _collisionCounter = 0;

    private void Awake() => _dieAudio = GetComponent<AudioSource>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out HealthController healthController)) return;
        if (!other.TryGetComponent(out PlayerController playerController)) return;
        if (!other.TryGetComponent(out BoostController boostController)) return;

        if (_collisionCounter++ != 0) return;

        if (boostController.Boosting == true)
        {
            Death();
            return;
        } 
        
        if (playerController.IsSliding == true)
        {
            Death();
            boostController.IncreaseBoost(5);
        }
        else
        {
            healthController.GetDamage();
        }
    }

    private void Death()
    {
        _dieAudio.Play();
        
        _model.SetActive(false);
        Destroy(gameObject, .5f);    
        
        GameObject particles = Instantiate(_dieParticles, transform.position, Quaternion.identity);
        Destroy(particles, 2);
    }
}
