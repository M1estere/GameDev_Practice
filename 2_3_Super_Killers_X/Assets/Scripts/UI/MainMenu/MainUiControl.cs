using UnityEngine;
using Zenject;

public class MainUiControl : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _wavesText;
    
    [Inject] private SceneFader _sceneFader;
    
    public void ChangeTimeScale(int newValue) => Time.timeScale = newValue;

    public void GoToAnotherScene(string sceneName)
    {
        Time.timeScale = 1;
        _sceneFader.FadeTo(sceneName);
    }

    public void SetWavesText(int current, int whole) =>
        _wavesText.SetText($"Wave {current}/{whole}");
}
