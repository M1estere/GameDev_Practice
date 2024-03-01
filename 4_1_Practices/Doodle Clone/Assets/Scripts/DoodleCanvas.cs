using UnityEngine;
using UnityEngine.SceneManagement;

public class DoodleCanvas : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
