using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("General Setup")]
    [SerializeField] private float _jumpForce = 10;
    [Space(5)]

    [Header("Enemy Setup")]
    [SerializeField] private bool _canSpawn = true;
    [SerializeField] private GameObject _enemy;

    private void Start()
    {
        if (!_canSpawn) return;

        if (Random.Range(0, 50) / 2 == 1)
            Instantiate(_enemy, transform.position + new Vector3(0, 0.4f), Quaternion.identity, transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y > 0) return;

        if (collision.collider.TryGetComponent(out Player player))
        {
            player.Jump(_jumpForce);
        }
    }
}
