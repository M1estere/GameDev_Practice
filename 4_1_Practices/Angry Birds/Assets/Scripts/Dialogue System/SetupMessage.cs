using I2.Loc;
using UnityEngine;

[RequireComponent(typeof(Typewriter))]
public class SetupMessage : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _content;
    [SerializeField] private Localize _localizeComponent;

    private Typewriter _typewriter;

    private void Awake() => _typewriter = GetComponent<Typewriter>();

    public void SetTranslateKey(string key)
    {
        _content.SetText("This is a placeholder text");

        _localizeComponent.Term = key;

        _typewriter.StartTypewriter(_content.text);
    }

    public float GetTypingDuration() => _typewriter.TimeBtwChars * _typewriter.Writer.Length;
}