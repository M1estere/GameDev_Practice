using System.Collections;
using UnityEngine;
using Zenject;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField] private Transform _cameraDefaultPosition;
    [SerializeField] private Transform _enemiesCameraPosition;
    [Space(5)] 
    
    [SerializeField] private float _cameraTranslationTime;

    [Inject] private SceneFader _sceneFader;
    
    [Inject] private Camera _mainCamera;
    
    public void GoToEnemiesScreen() => StartCoroutine(CameraMovementToEnemies());
    public void GoToDefaultScreen() => StartCoroutine(CameraMovementToDefault());
    public void GoToAnotherScene(string sceneName) => _sceneFader.FadeTo(sceneName);
    public void ExitGame() => Application.Quit();
    
    private IEnumerator CameraMovementToEnemies()
    {
        float lerp = 0;
        do
        {
            lerp += Time.deltaTime / _cameraTranslationTime;
            _mainCamera.transform.position = Vector3.Lerp(_cameraDefaultPosition.position, _enemiesCameraPosition.position, lerp);
            _mainCamera.transform.rotation = Quaternion.Lerp(_cameraDefaultPosition.rotation, _enemiesCameraPosition.rotation, lerp);
            
            yield return null;
        } while (lerp < 1);
    }
    
    private IEnumerator CameraMovementToDefault()
    {
        float lerp = 0;
        do
        {
            lerp += Time.deltaTime / _cameraTranslationTime;
            _mainCamera.transform.position = Vector3.Lerp(_enemiesCameraPosition.position, _cameraDefaultPosition.position, lerp);
            _mainCamera.transform.rotation = Quaternion.Lerp(_enemiesCameraPosition.rotation, _cameraDefaultPosition.rotation, lerp);
            
            yield return null;
        } while (lerp < 1);
    }
}
