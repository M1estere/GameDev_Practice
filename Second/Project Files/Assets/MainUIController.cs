using System.Collections;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    [SerializeField] private Animator _mainAnimator;
    [Space(10)]
    
    [Header("After Activation")] 
    [SerializeField] private GameObject _queueMenu;
    [SerializeField] private GameObject _playlistsMenu;
    
    private bool _opened = false;
    
    private bool _canDo = true;

    public void ToggleMainUI()
    {
        if (_canDo == false) return;

        if (_opened == true) StartCoroutine(Close());
        if (_opened == false) StartCoroutine(Open());
    }
    
    private IEnumerator Open()
    {
        _canDo = false;
        
        _mainAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(.5f);

        _canDo = true;
        _opened = true;
    }

    private IEnumerator Close()
    {
        _canDo = false;
        
        _mainAnimator.SetTrigger("Close");
        yield return new WaitForSeconds(.5f);

        _canDo = true;
        _opened = false;
    }
    
    public void OpenQueueMenu()
    {
        _queueMenu.SetActive(true);
        _playlistsMenu.SetActive(false);
    }

    public void OpenPlaylistsMenu()
    {
        _queueMenu.SetActive(false);
        _playlistsMenu.SetActive(true);
    }
}
