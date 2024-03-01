using UnityEngine;

public class OmNom : MonoBehaviour
{
    [SerializeField] private float _animationDistance = 1.2f;

    [SerializeField] private Transform _candy;

    [SerializeField] private GameObject _finishScreen;

    private bool _done = false;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_done) return;

        if (Vector2.Distance(_candy.position, transform.position) < _animationDistance)
        {
            _animator.SetBool("Open", true);
        }
        else
        {
            _animator.SetBool("Open", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _done = true;
            _animator.SetBool("Open", false);
            _animator.SetBool("Eating", true);
            print("Won");

            Destroy(other.gameObject);

            _finishScreen.SetActive(true);
        }
    }
}
