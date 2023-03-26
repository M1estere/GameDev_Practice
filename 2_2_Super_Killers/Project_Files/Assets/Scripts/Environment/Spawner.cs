using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Enemy Types")]
    [SerializeField] private GameObject rangedEnemy;
    [SerializeField] private GameObject meleeEnemy;
    [Space(5)]
    
    [SerializeField] private float spawnRadius;
    
    private Transform _playerTransform;

    private void Awake() => _playerTransform = FindObjectOfType<PlayerMovement>().transform;

    public void Spawn(int amount) => StartCoroutine(SpawnWave(amount));

    private IEnumerator SpawnWave(int amount)
    {
        while (amount > 0)
        {
            int type = Random.Range(0, 2);

            Vector3 spawnPosition = _playerTransform.position;
            spawnPosition += Random.insideUnitSphere.normalized * spawnRadius;
            spawnPosition.y = 1.75f;
            
            Instantiate(type == 0 ? rangedEnemy : meleeEnemy, spawnPosition, Quaternion.identity);
            
            amount--;
            yield return new WaitForSecondsRealtime(.05f);
        }

        yield return null;
    }
}
