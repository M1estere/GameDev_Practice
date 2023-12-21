using System.Collections;
using UnityEngine;

public class StartCountDownController : MonoBehaviour
{
    [SerializeField] private GameObject _countDownScreen;

    private IEnumerator Start()
    {
        if (PlayerPrefs.GetInt("Show_Count") == 1) yield break;

        Time.timeScale = 0;
        _countDownScreen.SetActive(true);

        yield return new WaitForSecondsRealtime(3.5f);

        _countDownScreen.SetActive(false);
        Time.timeScale = 1;
    }
}
