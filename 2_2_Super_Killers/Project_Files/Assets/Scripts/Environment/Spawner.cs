using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Enemy Types")]
    [SerializeField] private GameObject _rangedEnemy;
    [SerializeField] private GameObject _meleeEnemy;
    [Space(5)]
    
    [SerializeField] private float _spawnRadius;
    [SerializeField] private float _playerOffsetDistance = 5;
    
    private Transform _playerTransform;

    private void Awake() => _playerTransform = FindObjectOfType<PlayerMovement>().transform;

    public void Spawn(int amount) => StartCoroutine(SpawnWave(amount));

    private IEnumerator SpawnWave(int amount)
    {
        if (_playerTransform == null) yield return null;
        
        while (amount > 0)
        {
            int type = Random.Range(0, 2);

            Vector3 spawnPosition = _playerTransform.position;
            spawnPosition += Random.insideUnitSphere.normalized * _spawnRadius;

            while (CheckDistance(spawnPosition) == false)
            {
                spawnPosition = _playerTransform.position;
                spawnPosition += Random.insideUnitSphere.normalized * _spawnRadius;
            }
            
            spawnPosition.y = 1.75f;
            
            Instantiate(type == 0 ? _rangedEnemy : _meleeEnemy, spawnPosition, Quaternion.identity);
            
            amount--;
            yield return new WaitForSecondsRealtime(.05f);
        }

        yield return null;
    }

    private bool CheckDistance(Vector3 position)
    {
        return Mathf.Abs(position.x - _playerTransform.position.x) >= _playerOffsetDistance
               && Mathf.Abs(position.z - _playerTransform.position.z) >= _playerOffsetDistance;
    }
}
