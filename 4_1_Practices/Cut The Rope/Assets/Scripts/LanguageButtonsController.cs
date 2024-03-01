using I2.Loc;
using UnityEngine;

public class LanguageButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject _enButton;
    [SerializeField] private GameObject _rusButton;

    private void Start()
    {
        _enButton.SetActive(false);
        _rusButton.SetActive(false);
        
        string lang = LocalizationManager.CurrentLanguage;

        if (lang == "Russian")
        {
            _enButton.SetActive(true);
        }
        else
        {
            _rusButton.SetActive(true);
        }
    }
}
