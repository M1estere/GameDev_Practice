using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Level levelConfiguration;
    [SerializeField] private Spawner spawner;

    private UiUpdater _updater;
    private float _lastSpawnTime;
    private int _counter = 0;

    private void Awake()
    {
        KillCounter.KillCount = 0;
        KillCounter.TotalEnemies = 0;
        _updater = FindObjectOfType<UiUpdater>();
        
        _updater.UpdateWave(_counter + 1, levelConfiguration.WavesAmount);
    }

    private void Start()
    {
        foreach (int enemiesAmount in levelConfiguration.EnemiesPerWave)
            KillCounter.TotalEnemies += enemiesAmount;
        
        SpawnWave();
    }

    private void Update()
    {
        if (_counter >= levelConfiguration.WavesAmount) CheckWaves();
        if (_counter > levelConfiguration.WavesAmount - 1) return;
        if (Time.time > levelConfiguration.WavesTime[_counter] + _lastSpawnTime) 
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
        spawner.Spawn(levelConfiguration.EnemiesPerWave[_counter]);
        _counter++;
        
        _updater.UpdateWave(_counter, levelConfiguration.WavesAmount);
    }
}
