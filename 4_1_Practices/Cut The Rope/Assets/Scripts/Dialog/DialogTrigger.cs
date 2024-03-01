using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue Dialogue;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            FindObjectOfType<DialogController>().StartDialogue(Dialogue);
    }
}