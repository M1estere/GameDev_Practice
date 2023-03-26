using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawnable Object Setup")]
    [SerializeField] private GameObject _frog;
    [SerializeField] private Transform _frogsParent;
    [Space(5)]
    
    [Header("Spawn Properties")]
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _xSpawnPosition = 90;
    
    private readonly int[] _positions = { -2, 0, 2 };
    private float _lastSpawnTime = 0;
    
    private void Update()
    {
        if (Time.time > _spawnDelay + _lastSpawnTime)
        {
            _lastSpawnTime = Time.time;
            
            SpawnFrog();
        }
    }

    private void SpawnFrog()
    {
        int positionZ = _positions[Globals.Random.Next(0, _positions.Length)];
        Vector3 position = new Vector3(_xSpawnPosition, 0, positionZ);

        Instantiate(_frog, position, Quaternion.identity, _frogsParent);
    }
}
