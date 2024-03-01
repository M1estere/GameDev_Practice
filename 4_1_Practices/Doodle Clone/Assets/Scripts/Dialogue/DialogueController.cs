using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueBox;
    [Space(5)]

    [SerializeField] private TMPro.TMP_Text _nameText;
    [SerializeField] private TMPro.TMP_Text _contentText;

    private Queue<string> _sentences;
    private Queue<string> _names;

    private void Start()
    {
        _sentences = new();
        _names = new ();
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

        _nameText.text = LocalizationManager.GetTermTranslation(_names.Dequeue());

        string sentenceKey =  _sentences.Dequeue();
        StartCoroutine(TypeSentence(sentenceKey));
    }

    private IEnumerator TypeSentence(string sentenceKey)
    {
        _contentText.text = "";
        foreach (char letter in LocalizationManager.GetTermTranslation(sentenceKey).ToCharArray())
        {
            _contentText.text += letter;
            yield return new WaitForSecondsRealtime(.01f);
        }
    }

    private void EndDialogue()
    {
        SceneManager.LoadScene(1);
    }
}
