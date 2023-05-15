using System.Collections;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _timerText;
    
    private void Start() => StartCoroutine(Launch());

    private IEnumerator Launch()
    {
        int counter = 0;
        Time.timeScale = 0;
        _timerText.gameObject.SetActive(true);
        
        while (counter < 3)
        {
            _timerText.SetText($"{3 - counter}");
            counter++;
            yield return new WaitForSecondsRealtime(1);
        }
        
        _timerText.gameObject.SetActive(false);
        Time.timeScale = 1;

        gameObject.SetActive(false);
    }
}