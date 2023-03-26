using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Ring : MonoBehaviour
{
    [SerializeField] private float _spinSpeed;
    [Space(5)]

    [Header("FX")]
    [SerializeField] private GameObject _takeParticles;
    [SerializeField] private GameObject _ringGfx;
    
    private AudioSource _takeAudio;

    private void Awake() => _takeAudio = GetComponent<AudioSource>();
    private void Update() => transform.Rotate(0, _spinSpeed * Time.deltaTime, 0, Space.World);

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out PlayerController playerController)) return;
        
        _takeAudio.Play();
            
        GameObject particles = Instantiate(_takeParticles, transform.position, Quaternion.identity);
        Destroy(particles, 1);
        
        GameController.Instance.IncreaseRings(1);
        _ringGfx.SetActive(false);
        
        Destroy(gameObject, 2);
    }
}
