using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogController : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueBox;

    [SerializeField] private TMPro.TMP_Text _nameTextField;
    [SerializeField] private TMPro.TMP_Text _mainTextField;

    private Queue<string> _sentences;
    private Queue<string> _names;

    private void Start()
    {
        _sentences = new();
        _names = new();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _sentences.Clear();

        _dialogueBox.SetActive(true);

        foreach (DialogueNode node in dialogue.Nodes)
        {
            _sentences.Enqueue(node.SentenceTranslateKey);
            _names.Enqueue(node.NameTranslateKey);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        _nameTextField.text = LocalizationManager.GetTermTranslation(_names.Dequeue());

        string sentenceKey = _sentences.Dequeue();
        StartCoroutine(TypeSentence(sentenceKey));
    }

    private IEnumerator TypeSentence(string sentenceKey)
    {
        _mainTextField.text = "";
        foreach (char letter in LocalizationManager.GetTermTranslation(sentenceKey))
        {
            _mainTextField.text += letter;
            yield return new WaitForSecondsRealtime(.01f);
        }
    }

    private void EndDialogue()
    {
        SceneManager.LoadScene(1);
    }
}