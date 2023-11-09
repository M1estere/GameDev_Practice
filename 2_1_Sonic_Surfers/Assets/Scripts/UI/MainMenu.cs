using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _bankRingsText;
    [Space(5)]
    
    [Header("Buttons")]
    [SerializeField] private string _startLevel = "Main Scene";
    [Space(5)] 
    
    [Header("Character Select")]
    [SerializeField] private GameObject[] _charactersShow;
    [Space(5)]
    
    [Header("Pages")]
    [SerializeField] private GameObject _thisPage;
    [SerializeField] private GameObject _aboutPage;
    [SerializeField] private GameObject _settingsPage;
    [SerializeField] private GameObject _leadersPage;

    private SceneFader _fader;
    
    private int _characterIndex;

    private void Awake()
    {
        _fader = FindObjectOfType<SceneFader>();
        
        _bankRingsText.SetText($"{PlayerPrefs.GetInt("BankRings")}");
        Time.timeScale = 1;
        
        _characterIndex = PlayerPrefs.GetInt("Character");
        ChooseCharacter(_characterIndex);
    }

    public void NextCharacter() => Next();
    public void PreviousCharacter() => Previous();

    private void Next()
    {
        if (_characterIndex != _charactersShow.Length - 1) _characterIndex++;
        else _characterIndex = 0;
        
        ChooseCharacter(_characterIndex);
    }

    private void Previous()
    {
        if (_characterIndex != 0) _characterIndex--;
        else _characterIndex = _charactersShow.Length - 1;
        
        ChooseCharacter(_characterIndex);
    }
    
    public void ChooseCharacter(int id)
    {
        foreach (GameObject obj in _charactersShow)
            obj.SetActive(false);
        
        _charactersShow[id].SetActive(true);
        PlayerPrefs.SetInt("Character", id);
    }

    public void Play() => _fader.FadeTo(_startLevel);
    public void VisitSite(string address) => Application.OpenURL(address);
    
    public void About()
    {
        _thisPage.SetActive(false);
        _aboutPage.SetActive(true);
        
        foreach (GameObject obj in _charactersShow)
            obj.SetActive(false);
    }

    public void Leaders()
    {
        _thisPage.SetActive(false);
        _leadersPage.SetActive(true);

        foreach (GameObject obj in _charactersShow)
            obj.SetActive(false);
    }
    
    public void Settings()
    {
        _thisPage.SetActive(false);
        _settingsPage.SetActive(true);
        
        foreach (GameObject obj in _charactersShow)
            obj.SetActive(false);
    }

    public void MainPage()
    {
        _thisPage.SetActive(true);
        _aboutPage.SetActive(false);
        _settingsPage.SetActive(false);
        _leadersPage.SetActive(false);

        ChooseCharacter(PlayerPrefs.GetInt("Character"));
    }

    public void Exit() => Application.Quit();
}
