using System.Collections;
using UnityEngine;
using Zenject;

public class EnemyGenerator : MonoBehaviour
{
    [Inject] private Wave[] _waves;

    [Inject] private Enemy.Factory _enemyFactory;

    [Inject] private Camera _camera;

    [Inject] private MainUiControl _mainUiControl;

    private int _counter = 0;
    
    private IEnumerator Start()
    {
        Vector3 cameraTransform = _camera.transform.position;
        
        foreach (Wave wave in _waves)
        {
            _mainUiControl.SetWavesText(++_counter, _waves.Length);
            for (int i = 0; i < wave.Count; i++)
            {
                int index = Random.Range(0, wave.EnemyPrefabs.Length);
                Enemy enemy = _enemyFactory.Create(wave.EnemyPrefabs[index]);
                
                cameraTransform.y = .5f * enemy.transform.localScale.y;
                
                Vector3 randomSpawnPosition = Random.insideUnitSphere.normalized;
                randomSpawnPosition.y = 0;
                
                enemy.transform.position = cameraTransform + (randomSpawnPosition * 20);
                
                yield return new WaitForSeconds(wave.SpawnDelay);
            }
            
            yield return new WaitForSeconds(wave.Delay);
        }
    }
}
