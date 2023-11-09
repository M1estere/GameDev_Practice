using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishController : MonoBehaviour
{
    [Header("User Info Display")]
    [SerializeField] private TMPro.TMP_Text _userNameField;
    [SerializeField] private TMPro.TMP_Text _userScore;
    [Space(5)]

    [SerializeField] private GameObject _speedEffector;
    [Space(10)]
    
    [Header("Finish UI")] 
    [SerializeField] private TMPro.TMP_Text _title;
    [SerializeField] private GameObject _finishLevelUI;
    [Space(5)]
    
    [SerializeField] private string _mainMenuName = "Main Menu";
    
    private SceneFader _fader;

    private void Start() => _fader = FindObjectOfType<SceneFader>();
    
    public void ShowFinish()
    {
        Bank.Instance.RecordRings();
        _speedEffector.SetActive(false);

        string name = PlayerPrefs.GetString("player_name").Replace("players:", "").Trim();
        _userNameField.SetText(name);
        _userScore.SetText($"{Bank.Instance.BankRings} ({RedisController.RedisControllerInstance.GetValue(name)})");

        _title.SetText("Better luck next time");
        _finishLevelUI.SetActive(true);
    }

    public void Restart() => _fader.FadeTo(SceneManager.GetActiveScene().name);
    public void Leave() => _fader.FadeTo(_mainMenuName);
}
