using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _sensitivityY;
    private float _sensitivityX;

    private float _yaw;
    private float _pitch;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _sensitivityX = PlayerPrefs.HasKey("X_Sens") ? PlayerPrefs.GetFloat("X_Sens") : 3;
        _sensitivityY = PlayerPrefs.HasKey("Y_Sens") ? PlayerPrefs.GetFloat("Y_Sens") : 3;
    }

    private void Update() 
    {
        if (Time.timeScale != 1) return;

        Look();
    }

    private void Look()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            _pitch -= Input.GetAxis("Mouse Y") * _sensitivityY;
            _pitch = Mathf.Clamp(_pitch, -90, 90);

            _yaw += Input.GetAxisRaw("Mouse X") * _sensitivityX;

            transform.localRotation = Quaternion.Euler(_pitch, _yaw, 0);
        }
    }

    public void UpdateSensitivity()
    {
        _sensitivityX = PlayerPrefs.GetFloat("X_Sens");
        _sensitivityY = PlayerPrefs.GetFloat("Y_Sens");
    }
}
