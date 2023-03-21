using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private Animator pauseAnimator;
    [SerializeField] private GameObject pauseObject;

    [SerializeField] private string mainMenuName = "Main Menu";
    
    public void OpenPause()
    {
        pauseObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePause() => StartCoroutine(Close());

    private IEnumerator Close()
    {
        pauseAnimator.SetTrigger("Close");
        yield return new WaitForSecondsRealtime(.5f);
        pauseObject.SetActive(false);

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
        SceneManager.LoadScene(mainMenuName);
    }
}
