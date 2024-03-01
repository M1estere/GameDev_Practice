using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public DialogueNode[] Nodes;
}

[System.Serializable]
public struct DialogueNode
{
    public string NameTranslateKey;
    public string SentenceTranslateKey;
}
