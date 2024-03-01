using I2.Loc;
using UnityEngine;

public class ButtonsState : MonoBehaviour
{
    [SerializeField] private GameObject _englishLangButton;
    [SerializeField] private GameObject _russianLangButton;

    private void Start()
    {
        _englishLangButton.SetActive(false);
        _russianLangButton.SetActive(false);

        string lang = LocalizationManager.CurrentLanguage;

        if (lang == "Russian")
        {
            _englishLangButton.SetActive(true);
        }
        else
        {
            _russianLangButton.SetActive(true);
        }
    }
}
