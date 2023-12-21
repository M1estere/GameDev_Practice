using UnityEngine;

public class TriggerShootingTutorial : MonoBehaviour
{
    private TutorialsController _tutorialsController;

    private int _counter = 0;

    private void Awake() => _tutorialsController = FindObjectOfType<TutorialsController>();

    private void OnTriggerEnter(Collider other)
    {
        if (_counter < 1 && other.gameObject.TryGetComponent(out PlayerController player))
        {
            _tutorialsController.StartTutorial(2);

            gameObject.SetActive(false);
            _counter++;
        }
    }
}
