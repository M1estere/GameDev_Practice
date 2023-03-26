using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _levels;
    [Space(5)]
    
    [Header("Pages")]
    [SerializeField] private GameObject _mainPage;
    [SerializeField] private GameObject _aboutPage;
    [Space(5)]
    
    [SerializeField] private TMPro.TMP_Text _currentGunText;

    public void OpenLevels() => _levels.SetActive(true);

    public void OpenAboutPage()
    {
        _mainPage.SetActive(false);
        _aboutPage.SetActive(true);
    }

    public void LeaveAboutPage()
    {
        _mainPage.SetActive(true);
        _aboutPage.SetActive(false);
    }

    public void Exit() => Application.Quit();

    public void ChooseGun(int number)
    {
        PlayerPrefs.SetInt("Gun", number);
        UpdateCurrentGunUI(number);
    }

    private void UpdateCurrentGunUI(int number)
    {
        switch (number)
        {
            case 2:
                _currentGunText.SetText("Current: <b>Fastel</b>");
                break;
            case 3:
                _currentGunText.SetText("Current: <b>Glock</b>");
                break;
            case 4:
                _currentGunText.SetText("Current: <b>Rev</b>");
                break;
            default:
                _currentGunText.SetText("Current: <b>Beretta</b>");
                break;
        }
    }
    
    public void ChooseLevel(string levelName) => SceneManager.LoadScene(levelName);
}
