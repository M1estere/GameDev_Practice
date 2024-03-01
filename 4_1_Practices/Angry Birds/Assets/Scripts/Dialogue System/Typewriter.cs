using System.Collections;
using UnityEngine;

public class Typewriter : MonoBehaviour
{
    [Header("Text Setup")]
    [SerializeField] private TMPro.TMP_Text _tmpText;
    [Space(5)]

    [Header("Params Setup")]
    [SerializeField] private float _delayBeforeStart = 0f;

    [field: SerializeField] public float TimeBtwChars { get; private set; } = 0.1f;

    public string Writer { get; set; }

    private void Awake()
    {
        if (_tmpText != null) Writer = _tmpText.text;
    }

    private void Start()
    {
        if (_tmpText != null) _tmpText.text = "";
    }

    public void StartTypewriter(string text)
    {
        Writer = text;
        StopAllCoroutines();

        if (_tmpText != null)
        {
            _tmpText.text = "";
            StartCoroutine(TypeWriterTMP());
        }
    }

    private void OnDisable() => StopAllCoroutines();

    private IEnumerator TypeWriterTMP()
    {
        yield return new WaitForSeconds(_delayBeforeStart);

        foreach (char c in Writer)
        {
            if (_tmpText.text.Length > 0)
                _tmpText.text = _tmpText.text.Substring(0, _tmpText.text.Length);

            _tmpText.text += c;

            yield return new WaitForSeconds(TimeBtwChars);
        }
    }
}