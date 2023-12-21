using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _optionsMenu;

    public void Restart() 
    {
        Time.timeScale = 1;

        LevelRestartController.Instance.ShowCountdown = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenOtherScene(string sceneName)
    {
        Time.timeScale = 1;

        LevelRestartController.Instance.ShowCountdown = true;
        PlayerPrefs.SetInt("Display Title", 1);

        SceneManager.LoadScene(sceneName);
    }

    public void OpenOptionsMenu()
    {
        _optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
