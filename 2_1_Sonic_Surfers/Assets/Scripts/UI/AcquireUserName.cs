using UnityEngine;
using UnityEngine.SceneManagement;

public class AcquireUserName : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField _inputField;

    public void SaveNameAndLeave(string sceneName)
    {
        if (_inputField.text.Length < 2) return;

        PlayerPrefs.SetString("player_name", "players:" + _inputField.text);
        SceneManager.LoadScene(sceneName);
    }
}
