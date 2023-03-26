using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishController : MonoBehaviour
{
    [SerializeField] private GameObject _speedEffector;
    [Space(10)]
    
    [Header("Finish UI")] 
    [SerializeField] private TMPro.TMP_Text _title;
    [SerializeField] private GameObject _finishLevelUI;
    [Space(5)]
    
    [SerializeField] private string _mainMenuName = "Main Menu";
    
    public void ShowFinish()
    {
        Bank.Instance.RecordRings();
        _speedEffector.SetActive(false);
        
        _title.SetText("Better luck next time");
        _finishLevelUI.SetActive(true);
    }

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void Leave() => SceneManager.LoadScene(_mainMenuName);
}
