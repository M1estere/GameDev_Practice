using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiUpdater : MonoBehaviour
{
    [Header("Gun UI")]
    [SerializeField] private TMPro.TMP_Text _ammoText;
    [SerializeField] private GameObject _reloadingText;
    [SerializeField] private Image _currentGunIcon;
    [Space(5)]
    
    [Header("Waves UI")]
    [SerializeField] private TMPro.TMP_Text _waveText;
    [Space(5)]
    
    [Header("Health UI")]
    [SerializeField] private Image _healthBar;
    [Space(5)] 
    
    [Header("Finish UI")] 
    [SerializeField] private TMPro.TMP_Text _title;
    [SerializeField] private GameObject _finishLevelUI;
    
    public void SetUI(float value, float maxValue) => _healthBar.fillAmount = value / maxValue;
    public void UpdateAmmo(int current, int whole) => _ammoText.SetText($"{current}/{whole}");
    public void UpdateWave(int current, int whole) => _waveText.SetText($"Wave {current}/{whole}");
    public void UpdateIcon(Sprite icon) => _currentGunIcon.sprite = icon;
    public void SetReload(float reloadTime) => StartCoroutine(Reload(reloadTime));

    private IEnumerator Reload(float reloadTime)
    {
        _reloadingText.SetActive(true);
        _ammoText.color = Color.grey;
        
        yield return new WaitForSeconds(reloadTime);
        
        _reloadingText.SetActive(false);
        _ammoText.color = Color.white;
    }

    public void ShowFinish(bool won)
    {
        Time.timeScale = 0;
        if (won == true)
        {
            _title.SetText("Congratulations!");
            _finishLevelUI.SetActive(true);
        }
        else
        {
            _title.SetText("Better luck next time");
            _finishLevelUI.SetActive(true);
        }
    }
}
