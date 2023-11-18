using UnityEngine;
using UnityEngine.SceneManagement;

public class AcquireUserName : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField _nameInputField;
    [SerializeField] private TMPro.TMP_InputField _passwordInputField;

    public void GetUserInfo(string sceneName)
    {
        if (_nameInputField.text.Length < 2 || _passwordInputField.text.Length < 2) return;

        PlayerPrefs.SetString("player_name", _nameInputField.text);
        PlayerPrefs.SetString("player_password", _passwordInputField.text);

        SceneManager.LoadScene(sceneName);
    }
}
