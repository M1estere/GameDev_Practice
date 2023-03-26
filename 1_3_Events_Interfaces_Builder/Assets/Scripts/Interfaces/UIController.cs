using UnityEngine;

public class UIController : MonoBehaviour/*, IOpenable*/
{
    /*private static readonly int CloseTrigger = Animator.StringToHash("Close");

    public void ButtonClick(GameObject obj)
    {
        if (obj.activeSelf == true) Close(obj);
        else Open(obj);
    }
    
    public void Open(GameObject obj) => obj.SetActive(true);

    public void Close(GameObject obj) => StartCoroutine(CloseCoroutine(obj));

    private IEnumerator CloseCoroutine(GameObject obj)
    {
        if (obj.TryGetComponent(out Animator animator) == false) yield return null;
        animator.SetTrigger(CloseTrigger);
        
        yield return new WaitForSecondsRealtime(.55f);
        
        obj.SetActive(false);
    }*/
}
