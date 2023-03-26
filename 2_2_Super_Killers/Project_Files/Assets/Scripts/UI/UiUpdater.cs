using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiUpdater : MonoBehaviour
{
    [Header("Gun UI")]
    [SerializeField] private TMPro.TMP_Text ammoText;
    [SerializeField] private GameObject reloadingText;
    [SerializeField] private Image currentGunIcon;
    [Space(5)]
    
    [Header("Waves UI")]
    [SerializeField] private TMPro.TMP_Text waveText;
    [Space(5)]
    
    [Header("Health UI")]
    [SerializeField] private Image healthBar;
    [Space(5)] 
    
    [Header("Finish UI")] 
    [SerializeField] private TMPro.TMP_Text title;
    [SerializeField] private GameObject finishLevelUI;
    
    public void SetUI(float value, float maxValue) => healthBar.fillAmount = value / maxValue;
    public void UpdateAmmo(int current, int whole) => ammoText.SetText($"{current}/{whole}");
    public void UpdateWave(int current, int whole) => waveText.SetText($"Wave {current}/{whole}");
    public void UpdateIcon(Sprite icon) => currentGunIcon.sprite = icon;
    public void SetReload(float reloadTime) => StartCoroutine(Reload(reloadTime));

    private IEnumerator Reload(float reloadTime)
    {
        reloadingText.SetActive(true);
        ammoText.color = Color.grey;
        
        yield return new WaitForSeconds(reloadTime);
        
        reloadingText.SetActive(false);
        ammoText.color = Color.white;
    }

    public void ShowFinish(bool won)
    {
        Time.timeScale = 0;
        if (won == true)
        {
            title.SetText("Congratulations!");
            finishLevelUI.SetActive(true);
        }
        else
        {
            title.SetText("Better luck next time");
            finishLevelUI.SetActive(true);
        }
    }
}
