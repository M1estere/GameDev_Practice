using UnityEngine;

public class Lever : MonoBehaviour
{
    public delegate void OnUseEvent();
    public static event OnUseEvent OnUse;

    [SerializeField] private Door _door;
    [SerializeField] private GameObject _UI;
    [Space(5)] 
    
    [SerializeField] private Transform _handle;
    
    private void OnEnable() => Sub(_door.Interact);
    private void OnDisable() => UnSub(_door.Interact);

    private void Sub(OnUseEvent eve) => OnUse += eve;
    private void UnSub(OnUseEvent eve) => OnUse -= eve;

    public void Activate()
    {
        RotateHandle(_door.Opened);
        OnUse?.Invoke();

        UnSub(_door.Interact);
        Sub(_door.Interact);
    }

    private void RotateHandle(bool isOpened)
    {
        int valueToAdd = isOpened ? -120 : 120;
        _handle.Rotate(0, 0, valueToAdd);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") == false) return;
        _UI.SetActive(true);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") == false) return;
        _UI.SetActive(false);
    }
}
