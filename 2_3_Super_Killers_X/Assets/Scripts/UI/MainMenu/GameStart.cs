using UnityEngine;
using Zenject;

public class GameStart : MonoBehaviour
{
    [Inject] private GameConfig _gameConfig;

    private void Awake() => Application.targetFrameRate = _gameConfig.FrameRate;
}