using UnityEngine;
using Zenject;

public class FinishScreenSetup : MonoBehaviour
{
    [Inject] private SceneFader _sceneFader;
    
    public void GoToAnotherScene(string sceneName) => _sceneFader.FadeTo(sceneName);
}
