using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Choose Level Menu")]
    [SerializeField] private GameObject levels;
    [Space(5)]
    
    [Header("Pages")]
    [SerializeField] private GameObject mainPage;
    [SerializeField] private GameObject aboutPage;
    [Space(5)]
    
    [SerializeField] private TMPro.TMP_Text currentGunText;

    public void OpenLevels() => levels.SetActive(true);

    public void OpenAboutPage()
    {
        mainPage.SetActive(false);
        aboutPage.SetActive(true);
    }

    public void LeaveAboutPage()
    {
        mainPage.SetActive(true);
        aboutPage.SetActive(false);
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
                currentGunText.SetText("Current: <b>Fastel</b>");
                break;
            case 3:
                currentGunText.SetText("Current: <b>Glock</b>");
                break;
            case 4:
                currentGunText.SetText("Current: <b>Rev</b>");
                break;
            default:
                currentGunText.SetText("Current: <b>Beretta</b>");
                break;
        }
    }
    
    public void ChooseLevel(string levelName) => SceneManager.LoadScene(levelName);
}
