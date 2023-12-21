using UnityEngine;

public class LevelCompletionController : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompletedScreen;

    private int _startEnemiesAmount;
    private int _killedEnemiesAmount;

    private void Start()
    {
        _startEnemiesAmount = FindObjectsByType<EnemyMovementController>(FindObjectsSortMode.None).Length;
    }

    public void AddKilled()
    {
        _killedEnemiesAmount += 1;

        if (_killedEnemiesAmount >= _startEnemiesAmount)
        {
            if (Time.timeScale != 1) return;

            _levelCompletedScreen.SetActive(true);
        }
    }
}
