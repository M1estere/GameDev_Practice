using System.Collections;
using UnityEngine;

public class StartTutorial : MonoBehaviour
{
    private TutorialsController _tutorialsController;

    private void Awake() => _tutorialsController = FindObjectOfType<TutorialsController>();

    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(4);
        _tutorialsController.StartTutorial(1);
    }
}
