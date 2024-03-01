using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _platform;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Platform platform))
        {
            Instantiate(_platform,
                new Vector2(Random.Range(-2.5f, 2.5f), _player.transform.position.y + 5),
                Quaternion.identity);

            Destroy(other.gameObject);
        }
    }
}
