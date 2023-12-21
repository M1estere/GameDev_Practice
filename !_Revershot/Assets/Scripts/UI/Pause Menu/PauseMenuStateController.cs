using UnityEngine;

public class PauseMenuStateController : MonoBehaviour
{
    [SerializeField] private KeyCode _pauseKey;
    [Space(5)]

    [SerializeField] private GameObject _pauseMenuObject;
    [SerializeField] private GameObject _optionsMenuObject;

    private void Awake() => Time.timeScale = 1;

    private void Update()
    {
        if (Input.GetKeyDown(_pauseKey)) TogglePause();
    }

    public void TogglePause()
    {
        if (_pauseMenuObject.activeSelf || _optionsMenuObject.activeSelf) DeactivatePause();
        else ActivatePause();
    }

    private void ActivatePause()
    {
        if (Time.timeScale != 1) return;

        Time.timeScale = 0;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        _pauseMenuObject.SetActive(true);
    }

    private void DeactivatePause() 
    {
        if (Time.timeScale != 0) return;

        Time.timeScale = 1;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _pauseMenuObject.SetActive(false);
        _optionsMenuObject.SetActive(false);
    }
}
