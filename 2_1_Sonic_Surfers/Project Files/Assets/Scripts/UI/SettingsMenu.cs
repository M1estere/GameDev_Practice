using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _audioRegion;
    [SerializeField] private GameObject _controlsRegion;
    [SerializeField] private GameObject _linksRegion;

    public void OpenAudio()
    {
        CloseAll();
        
        _audioRegion.SetActive(true);
    }

    public void OpenControls()
    {
        CloseAll();
        
        _controlsRegion.SetActive(true);
    }
    
    public void OpenLinks()
    {
        CloseAll();
        
        _linksRegion.SetActive(true);
    }
    
    private void CloseAll()
    {
        _audioRegion.SetActive(false);
        _controlsRegion.SetActive(false);
        _linksRegion.SetActive(false);
    }
}
