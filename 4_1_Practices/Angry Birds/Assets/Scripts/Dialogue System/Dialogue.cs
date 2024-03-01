using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private DialogueNode _startNode;
    
    [Header("Visual Part Setup")]
    [SerializeField] private Timer _timer;
    [SerializeField] private Transform _blocksParent;
    [Space(3)]

    [SerializeField] private GameObject _userBlock;
    [SerializeField] private GameObject _responseBlock;
    [Space(5)]

    [Header("Responses")]
    [SerializeField] private GameObject _responsesParent;
    [SerializeField] private TMP_Text[] _responsesObjects;
    [SerializeField] private TMP_Text[] _bgObjects;

    [SerializeField] private ScrollRect _mainRect;

    private DialogueNode _currentNode;

    private void Update()
    {
        _mainRect.verticalNormalizedPosition = 0;
    }

    public void StartDialogue()
    {
        SetNewDialogueNode(_startNode);
    }

    public void SetNewDialogueNode(DialogueNode node)
    {
        _currentNode = node;

        StartNode();
    }

    public void StartNode()
    {
        _responsesParent.SetActive(false);

        SetupMessage block;
        if (_currentNode == null) // the end
        {
            print("End");
            return;
        }

        block = Instantiate(_userBlock, _blocksParent.position, _blocksParent.rotation, _blocksParent).GetComponent<SetupMessage>();
        block.SetTranslateKey($"Dialogue/{_currentNode.MessageTranslateKey}");

        StartCoroutine(WaitForTypeCoroutine(block));
    }

    private IEnumerator WaitForTypeCoroutine(SetupMessage block)
    {
        float waitTime = block.GetTypingDuration();
        int responsesAmount = _currentNode.UserResponses.Length;

        if (responsesAmount == 1)
        {
            yield return new WaitForSecondsRealtime(waitTime + 1);
            GiveResponse(_currentNode.UserResponses[0]);
            yield break;
        }

        yield return new WaitForSecondsRealtime(waitTime + 1);

        _responsesParent.SetActive(true);
        for (int i = 0; i < 2; i++)
        {
            _responsesObjects[i].SetText(LocalizationManager.GetTranslation($"Dialogue/{_currentNode.UserResponses[i].ResponseTranslateKey}"));
            _bgObjects[i].SetText($"Dialogue/{_currentNode.UserResponses[i].ResponseTranslateKey}");
        }

        _timer.StartCycle(8, GetRandomUserResponse());
    }

    private UserResponse GetRandomUserResponse()
    {
        List<UserResponse> tempResponses = new();

        if (tempResponses.Count == 0)
        {
            if (_currentNode.UserResponses.Length == 0)
                return null;
            else
                return _currentNode.UserResponses[Random.Range(0, _currentNode.UserResponses.Length)];
        }

        return tempResponses[Random.Range(0, tempResponses.Count)];
    }

    public void ChooseResponseOption(TMP_Text buttonText)
    {
        string content = buttonText.text;

        foreach (UserResponse response in _currentNode.UserResponses)
        {
            if ($"Dialogue/{response.ResponseTranslateKey}" == content)
            {
                GiveResponse(response);

                break;
            }
        }
    }

    public void GiveResponse(UserResponse response)
    {
        _timer.Stop();
        _responsesParent.SetActive(false);

        _currentNode = response.ResponseNode;

        if (response.ResponseTranslateKey == "dialogue_angry_birds")
        {
            SceneManager.LoadScene(1);
        } else if (response.ResponseTranslateKey == "dialogue_exit_game")
        {
            Application.Quit();
        }

        SetupMessage block = Instantiate(_responseBlock, _blocksParent.position, _blocksParent.rotation, _blocksParent).GetComponent<SetupMessage>();
        block.SetTranslateKey($"Dialogue/{response.ResponseTranslateKey}");

        StartCoroutine(ResponseCoroutine(block));
    }

    private IEnumerator ResponseCoroutine(SetupMessage block)
    {
        float waitTime = block.GetTypingDuration();

        yield return new WaitForSecondsRealtime(waitTime + 1f);

        StartNode();
    }
}