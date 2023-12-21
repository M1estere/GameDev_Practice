using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelCompletedScreenController : MonoBehaviour
{
    [SerializeField] private string _mainMenuSceneName = "Main_Menu";

    private float _timeDecrement = .35f;

    private void Start()
    {
        StartCoroutine(EnableLevelCompletion());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) GoToMenu();
    }

    private void GoToMenu()
    {
        LevelRestartController.Instance.ShowCountdown = true;
        PlayerPrefs.SetInt("Display Title", 1);

        SceneManager.LoadScene(_mainMenuSceneName);
    }

    private IEnumerator EnableLevelCompletion()
    {
        while (Time.timeScale > _timeDecrement)
        {
            Time.timeScale -= _timeDecrement;
            yield return null;
        }
    }
}
