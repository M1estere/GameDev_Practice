using UnityEngine;

public class BulletDecalController : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;

    public void StartSequence()
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);

        Destroy(bullet, 60);
    }
}
