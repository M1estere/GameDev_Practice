using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Node", menuName = "Dialogue/Node", order = 2)]
public class DialogueNode : ScriptableObject
{
    [field: SerializeField, TextArea] public string MessageTranslateKey { get; set; }
    [field: Space(3)]

    [field: SerializeField] public UserResponse[] UserResponses { get; set; }
}

[System.Serializable]
public class UserResponse
{
    [field: SerializeField, TextArea] public string ResponseTranslateKey { get; set; }

    [field: SerializeField] public DialogueNode ResponseNode { get; set; }
}