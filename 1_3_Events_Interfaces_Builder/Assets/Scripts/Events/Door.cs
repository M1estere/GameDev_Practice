using UnityEngine;

public class Door : MonoBehaviour
{
    public bool Opened { get; private set; }

    [SerializeField] private float _closedScale;
    [SerializeField] private float _openedScale;

    public void Interact()
    {
        if (Opened) Close();
        else Open();
    }
    
    private void Open()
    {
        Opened = true;
        transform.localScale = new Vector3(_openedScale, 1, 1);
    }

    private void Close()
    {
        Opened = false;
        transform.localScale = new Vector3(_closedScale, 1, 1);
    }
}
