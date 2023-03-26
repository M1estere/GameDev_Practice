using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private Animator _pauseAnimator;
    [SerializeField] private GameObject _pauseObject;

    [SerializeField] private string _mainMenuName = "Main Menu";
    
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_mainMenuName);
    }
}
