using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Level _levelConfiguration;
    [SerializeField] private Spawner _spawner;

    private UiUpdater _updater;
    private float _lastSpawnTime;
    private int _counter = 0;

    private void Awake()
    {
        KillCounter.KillCount = 0;
        KillCounter.TotalEnemies = 0;
        _updater = FindObjectOfType<UiUpdater>();
        
        _updater.UpdateWave(_counter + 1, _levelConfiguration.WavesAmount);
    }

    private void Start()
    {
        foreach (int enemiesAmount in _levelConfiguration.EnemiesPerWave)
            KillCounter.TotalEnemies += enemiesAmount;
        
        SpawnWave();
    }

    private void Update()
    {
        if (_counter >= _levelConfiguration.WavesAmount) CheckWaves();
        if (_counter > _levelConfiguration.WavesAmount - 1) return;
        if (Time.time > _levelConfiguration.WavesTime[_counter] + _lastSpawnTime) 
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
        _spawner.Spawn(_levelConfiguration.EnemiesPerWave[_counter]);
        _counter++;
        
        _updater.UpdateWave(_counter, _levelConfiguration.WavesAmount);
    }
}
