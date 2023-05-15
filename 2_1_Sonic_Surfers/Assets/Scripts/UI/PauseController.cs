using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private Animator _pauseAnimator;
    [SerializeField] private GameObject _pauseObject;
    [Space(2)]
    
    [SerializeField] private string _mainMenuName = "Main Menu";

    private SceneFader _fader;

    private void Start() => _fader = FindObjectOfType<SceneFader>();

    public void OpenPause()
    {
        _pauseObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePause() => StartCoroutine(Close());

    private IEnumerator Close()
    {
        _pauseAnimator.SetTrigger("Close");
        yield return new WaitForSecondsRealtime(.5f);
        _pauseObject.SetActive(false);

        Time.timeScale = 1;
    }
    
    public void ReloadLevel()
    {
        Time.timeScale = 1;
        _fader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1;
        _fader.FadeTo(_mainMenuName);
    }
}
