using UnityEngine;

public class MainMenuCameraRotator : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    private void Update()
    {
        transform.Rotate(0f, _rotateSpeed * Time.deltaTime, 0f, Space.World);
    }
}
