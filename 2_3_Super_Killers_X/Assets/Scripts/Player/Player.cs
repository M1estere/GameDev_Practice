using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IFixedTickable
{
    [Inject] private Rigidbody _rigidbody;
    
    [Inject] private GameConfig _gameConfig;

    [Inject] private FixedJoystick _joystick;
    
    [Inject]
    private void Constructor(TickableManager tickableManager) => tickableManager.AddFixed(this);

    public void FixedTick()
    {
        float horizontal = _joystick.Horizontal;
        float vertical = _joystick.Vertical;
        
        Vector3 force = new Vector3(-vertical, 0, horizontal).normalized * (_gameConfig.PlayerMovingForceMultiplier / 2);
        Matrix4x4 matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 skewedInput = matrix.MultiplyPoint3x4(force);
        
        _rigidbody.AddForce(skewedInput, ForceMode.Impulse);
    }
}
