using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameObject _timeBarInner;

    private Dialogue _dialogueController;

    private void Awake() => _dialogueController = FindObjectOfType<Dialogue>();

    public void StartCycle(float duration, UserResponse response)
    {
        _timeBarInner.transform.localScale = new Vector3(1, 1, 1);
        _timeBarInner.SetActive(true);

        StartCoroutine(CycleCoroutine(duration, response));
    }

    public void Stop() => StopAllCoroutines();

    private IEnumerator CycleCoroutine(float duration, UserResponse response)
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            var someValueFrom0To1 = currentTime / duration;

            Vector3 temp = _timeBarInner.transform.localScale;
            temp.x = 1 - someValueFrom0To1;
            _timeBarInner.transform.localScale = temp;

            currentTime += Time.deltaTime;

            yield return null;
        }

        if (response != null)
            _dialogueController.GiveResponse(response);
    }
}

