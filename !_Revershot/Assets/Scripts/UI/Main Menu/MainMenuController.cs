using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _titleMenu;
    [SerializeField] private GameObject _mainMenu;

    [SerializeField] private GameObject _optionsMenuObject;

    private void Awake() 
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Application.targetFrameRate = -1;
        PlayerPrefs.SetInt("Show_Count", 0);

        int show = PlayerPrefs.GetInt("Display Title");
        if (show == 0)
        {
            _titleMenu.SetActive(true);
            _mainMenu.SetActive(false);
        } else
        {
            _titleMenu.SetActive(false);
            _mainMenu.SetActive(true);
        }
    }

    public void StartGame(string gameScene) => SceneManager.LoadScene(gameScene);

    public void OpenOptions()
    {
        _optionsMenuObject.SetActive(true);
        _mainMenu.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        PlayerPrefs.SetInt("Display Title", 0);
        Application.Quit();
    }
}
