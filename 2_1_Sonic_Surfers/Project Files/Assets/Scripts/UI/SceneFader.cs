using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private Image _blackImage;
    [SerializeField] private AnimationCurve _fadeCurve;

    private void Start() => StartCoroutine(FadeIn());

    public void FadeTo(string scene) => StartCoroutine(FadeOut(scene));

    private IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0)
        {
            t -= Time.deltaTime;
            float alpha = _fadeCurve.Evaluate(t);
            _blackImage.color = new Color (0f, 0f, 0f, alpha);
            
            yield return 0;
        }
    }

    private IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime;
            float alpha = _fadeCurve.Evaluate(t);
            _blackImage.color = new Color (0f, 0f, 0f, alpha);
            
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
