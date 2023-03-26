using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Swipe : MonoBehaviour
{
    private PlayerController _playerController;
    private Vector2 _startTouch, _swipeDelta;
    
    private bool _swipeLeft, _swipeRight;
    private bool _swipeUp, _swipeDown;
    
    private bool _isDragging = false;

    private void Awake() => _playerController = GetComponent<PlayerController>();
    
    private void Update()
    {
        _swipeDown = _swipeUp = _swipeLeft = _swipeRight = false;

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _isDragging = true;
                _startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase is TouchPhase.Ended or TouchPhase.Canceled)
            {
                _isDragging = false;
                Reset();
            }
        }

        _swipeDelta = Vector2.zero;
        if (_isDragging == true)
        {
            if (Input.touches.Length > 0)
                _swipeDelta = Input.touches[0].position - _startTouch;
        }

        if (_swipeDelta.magnitude <= 100) return;
        
        float x = _swipeDelta.x;
        float y = _swipeDelta.y;
        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if (x < 0) _swipeLeft = true;
            else _swipeRight = true;
        }
        else
        {
            if (y < 0) _swipeDown = true;
            else _swipeUp = true;
        }

        CheckInputs();
        Reset();
    }

    private void CheckInputs()
    {
        if (_swipeLeft == true)
            _playerController.SideStep(1);
        else if (_swipeRight == true)
            _playerController.SideStep(-1);
        else if (_swipeUp == true)
            _playerController.Jump();
        else if (_swipeDown == true)
            _playerController.Slide();
    }

    private void Reset()
    {
        _startTouch = Vector2.zero;
        _swipeDelta = Vector2.zero;
        
        _isDragging = false;
    }
}
