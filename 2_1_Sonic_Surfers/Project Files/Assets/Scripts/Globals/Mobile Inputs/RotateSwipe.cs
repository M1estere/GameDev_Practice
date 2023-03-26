using UnityEngine;

public class RotateSwipe : MonoBehaviour
{
    [SerializeField] private float _rotateSpeedMultiplier = 2;
    
    private Touch _touch;
    
    private Vector2 _touchPosition;
    private Quaternion _rotationY;

    private void Update()
    {
        if (Input.touchCount <= 0) return;
        _touch = Input.GetTouch(0);

        if (_touch.phase != TouchPhase.Moved) return;
        
        _rotationY = Quaternion.Euler(0, -_touch.deltaPosition.x * _rotateSpeedMultiplier, 0);
        transform.rotation = _rotationY * transform.rotation;
    }
}