using UnityEngine;

[System.Serializable]
public struct Wave
{
    [field: SerializeField]
    public Enemy[] EnemyPrefabs { get; private set; }
    
    [field: SerializeField]
    public int Count { get; private set; }
    
    [field: SerializeField]
    public float SpawnDelay { get; private set; }
    
    [field: SerializeField]
    public float Delay { get; private set; }
}
