using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [Space(7)]
    
    [SerializeField, Tooltip("Canvas follow speed")] private float _followSpeed;

    private void Update()
    {
        var mousePosition = new Vector3
        {
            x = Input.mousePosition.x,
            y = Input.mousePosition.y,
            z = 0
        };

        var output = _mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _mainCamera.nearClipPlane));
        transform.position = Vector2.Lerp(transform.position, new Vector2(output.x, output.y),
            Time.deltaTime * _followSpeed);
    }
}