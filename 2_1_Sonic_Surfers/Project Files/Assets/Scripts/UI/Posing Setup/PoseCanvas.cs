using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PoseCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _frontBlackCanvas;
    
    private Animator _characterAnimator;
    private PoseControl _poseControl;

    private GameObject _character;
    
    public void StartPosing(GameObject character, PoseControl pc)
    {
        _character = character;
        _poseControl = pc;
        
        _frontBlackCanvas.SetActive(true);
        if (character.TryGetComponent(out Animator animator))
        {
            _characterAnimator = animator;
            character.SetActive(true);
        }

        StartCoroutine(Posing());
    }

    private IEnumerator Posing()
    {
        yield return new WaitForSecondsRealtime(3f);
        _characterAnimator.SetTrigger("Pose");
        if (_character.TryGetComponent(out AudioSource source))
            source.Play();
        
        yield return new WaitForSecondsRealtime(1.5f);
        _frontBlackCanvas.SetActive(false);
        _characterAnimator.gameObject.SetActive(false);
        
        _poseControl.EndPosingCycle();
    }
}
