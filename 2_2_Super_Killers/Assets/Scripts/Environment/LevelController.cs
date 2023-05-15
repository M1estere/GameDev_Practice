using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Level LevelConfiguration { get; set; }
    
    [SerializeField] private Spawner _spawner;

    private UiUpdater _updater;
    private float _lastSpawnTime;
    private int _waveCounter = 0;

    private void Awake()
    {
        KillCounter.KillCount = 0;
        KillCounter.TotalEnemies = 0;
        _updater = FindObjectOfType<UiUpdater>();
    }

    private void Start()
    {
        _updater.UpdateWave(_waveCounter + 1, LevelConfiguration.WavesAmount);

        foreach (var wave in LevelConfiguration.Waves)
                KillCounter.TotalEnemies += wave.EnemiesOnWave;

        SpawnWave();
    }

    private void Update()
    {
        if (_waveCounter >= LevelConfiguration.WavesAmount) CheckWaves();
        if (_waveCounter > LevelConfiguration.WavesAmount - 1) return;
        if (Time.time > LevelConfiguration.Waves[_waveCounter].WaveTime+ _lastSpawnTime) 
        {
            _lastSpawnTime = Time.time;
            SpawnWave();
        }
    }

    private void CheckWaves()
    {
        if (KillCounter.KillCount >= KillCounter.TotalEnemies)
            _updater.ShowFinish(true);
    }

    private void SpawnWave()
    {
        _spawner.Spawn(LevelConfiguration.Waves[_waveCounter].EnemiesOnWave);
        _waveCounter++;
        
        _updater.UpdateWave(_waveCounter, LevelConfiguration.WavesAmount);
    }
}
