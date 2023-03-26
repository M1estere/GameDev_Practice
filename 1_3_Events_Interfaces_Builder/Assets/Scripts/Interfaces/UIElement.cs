using System.Collections;
using UnityEngine;

public class UIElement : MonoBehaviour, IOpenable
{
    [SerializeField] private GameObject _mainObject;
    
    private static readonly int CloseString = Animator.StringToHash("Close");
    
    public void Open() => _mainObject.SetActive(true);
    public void Close() => StartCoroutine(CloseCoroutine());

    private IEnumerator CloseCoroutine()
    {
        if (_mainObject.TryGetComponent(out Animator animator) == false) yield return null;
        animator.SetTrigger(CloseString);
        
        yield return new WaitForSecondsRealtime(.55f);
        
        _mainObject.SetActive(false);
    }

    public void ButtonClick()
    {
        if (_mainObject.activeSelf == false) Open();
        else Close();
    }
}
