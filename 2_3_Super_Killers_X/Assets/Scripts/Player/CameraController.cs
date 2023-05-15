using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour, IFixedTickable
{
    [Inject] private Player _player;

    [Inject] private GameConfig _gameConfig;
    
    [Inject]
    private void Constructor(TickableManager tickableManager) => tickableManager.AddFixed(this);

    public void FixedTick()
    {
        Vector3 targetVector = _player.transform.position + _gameConfig.Offset;

        Vector3 thisPosition = transform.position;
        float xValue = Mathf.Lerp(thisPosition.x, targetVector.x, _gameConfig.CameraFollowInterpolation * Time.deltaTime);
        float zValue = Mathf.Lerp(thisPosition.z, targetVector.z, _gameConfig.CameraFollowInterpolation * Time.deltaTime);

        thisPosition = new Vector3(xValue, thisPosition.y, zValue);
        transform.position = thisPosition;
    }
}
