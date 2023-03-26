using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ShootingController))]
public class ShootingGraphics : MonoBehaviour
{
    [Header("Graphics Setup")]
    [SerializeField] private GameObject _shootingSystem;
    [SerializeField] private GameObject _impactSystem;
    [SerializeField] private TrailRenderer _bulletTrail;
    [SerializeField] private Transform _bulletSpawnPoint;
    [Space(5)]
    
    [SerializeField] private float _trailSpeedMultiplier = 13f;
    
    public void Shoot(RaycastHit hit)
    {
        GameObject particles = Instantiate(_shootingSystem, _bulletSpawnPoint.position, Quaternion.identity);
        TrailRenderer trail = Instantiate(_bulletTrail, _bulletSpawnPoint.position, Quaternion.identity);

        StartCoroutine(SpawnTrail(trail, hit));
        
        Destroy(particles, 1);
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += (Time.deltaTime / trail.time) * _trailSpeedMultiplier;

            yield return null;
        }

        trail.transform.position = hit.point;
        GameObject particles = Instantiate(_impactSystem, hit.point, Quaternion.LookRotation(hit.normal));
        
        Destroy(trail.gameObject, trail.time);
        Destroy(particles, 1);
    }
}
