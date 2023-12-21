using UnityEngine;
using UnityEngine.AI;

public class TriggerEnemyTutorial : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyMovementController _controller;

    private TutorialsController _tutorialsController;

    private int _counter = 0;

    private void Awake() => _tutorialsController = FindObjectOfType<TutorialsController>();

    private void OnTriggerEnter(Collider other)
    {
        if (_counter < 1 && other.gameObject.TryGetComponent(out PlayerController player))
        {
            _agent.enabled = true;
            _controller.enabled = true;

            _tutorialsController.StartTutorial(3);

            gameObject.SetActive(false);
            _counter++;
        }
    }
}
