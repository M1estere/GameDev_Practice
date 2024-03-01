using Unity.VisualScripting;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue Dialogue;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out DialogueCharacter dialogueCharacter))
            FindObjectOfType<DialogueController>().StartDialogue(Dialogue);
    }
}
