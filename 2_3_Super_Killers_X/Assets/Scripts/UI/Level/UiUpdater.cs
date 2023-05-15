using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UiUpdater : MonoBehaviour
{
    [SerializeField] private Animator _damageAnimator;
    [Space(5)]
    
    [Header("Finish Setup")]
    [SerializeField] private TMPro.TMP_Text _title;
    [SerializeField] private GameObject _finishLevelUi;
    
    [Inject(Id = "healthBar")] private Image _healthBar;
    [Inject(Id = "ammoText")] private TMPro.TMP_Text _ammoText;
    [Inject(Id = "waveText")] private TMPro.TMP_Text _waveText;
    [Inject(Id = "reloadText")] private TMPro.TMP_Text _reloadingText;

    [Inject(Id = "gunIcon")] private Image _currentGunIcon;
    
    public void SetHealth(float value, float maxValue) => _healthBar.fillAmount = value / maxValue;
    
    public void UpdateWave(int current, int whole) => _waveText.SetText($"Wave {current}/{whole}");
    public void UpdateAmmo(int current, int whole) => _ammoText.SetText($"{current}/{whole}");
    public void UpdateIcon(Sprite icon) => _currentGunIcon.sprite = icon;
    
    public void SetReload(float reloadTime) => StartCoroutine(Reload(reloadTime));

    public void SetDamage(float value, float maxValue)
    {
        _healthBar.fillAmount = value / maxValue;
        _damageAnimator.SetTrigger("Damage");
    }

    private IEnumerator Reload(float reloadTime)
    {
        _reloadingText.gameObject.SetActive(true);
        _ammoText.color = Color.grey;
        
        yield return new WaitForSeconds(reloadTime);
        
        _reloadingText.gameObject.SetActive(false);
        _ammoText.color = Color.white;
    }
    
    public void ShowFinish(bool won)
    {
        Time.timeScale = 0;
        if (won == true)
        {
            _title.SetText("Congratulations!");
            _finishLevelUi.SetActive(true);
        }
        else
        {
            _title.SetText("Better luck next time");
            _finishLevelUi.SetActive(true);
        }
    }
}
