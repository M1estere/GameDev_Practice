using System.Collections;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text timerText;
    
    private void Start() => StartCoroutine(Launch());

    private IEnumerator Launch()
    {
        int counter = 0;
        Time.timeScale = 0;
        timerText.gameObject.SetActive(true);
        
        while (counter < 3)
        {
            timerText.SetText($"{3 - counter}");
            counter++;
            yield return new WaitForSecondsRealtime(1);
        }
        
        timerText.gameObject.SetActive(false);
        Time.timeScale = 1;

        gameObject.SetActive(false);
    }
}
