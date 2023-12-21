using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Restart();
    }

    private void Restart()
    {
        LevelRestartController.Instance.ShowCountdown = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
