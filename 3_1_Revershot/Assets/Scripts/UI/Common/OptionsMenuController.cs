using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _previousScreen;
    [Space(5)]

    [SerializeField] private Slider _xSensSlider;
    [SerializeField] private Slider _ySensSlider;

    private CameraController _cameraController;

    private void OnEnable()
    {
        _cameraController = FindFirstObjectByType<CameraController>();

        _xSensSlider.value = PlayerPrefs.HasKey("X_Sens") ? PlayerPrefs.GetFloat("X_Sens") : 3;
        _ySensSlider.value = PlayerPrefs.HasKey("Y_Sens") ? PlayerPrefs.GetFloat("Y_Sens") : 3;
    }

    public void SetSensitivityX(float sensitivityValue)
    {
        PlayerPrefs.SetFloat("X_Sens", sensitivityValue);

        if (_cameraController != null) _cameraController.UpdateSensitivity();
    }

    public void SetSensitivityY(float sensitivityValue)
    {
        PlayerPrefs.SetFloat("Y_Sens", sensitivityValue);

        if (_cameraController != null) _cameraController.UpdateSensitivity();
    }

    public void ReturnToSettings()
    {
        gameObject.SetActive(false);
        _previousScreen.SetActive(true);
    }
}
