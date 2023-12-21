using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyMovementController), typeof(EnemyShootingController), typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyRagdollActivation), typeof(Animator))]
public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private AudioSource _deathSource;

    private EnemyRagdollActivation _enemyRagdollActivation;
    private Animator _animator;

    private NavMeshAgent _navMeshAgent;
    private EnemyMovementController _enemyMovementController;
    private EnemyShootingController _enemyShootingController;

    private LevelCompletionController _levelCompletionController;

    private int _deactivationTimes = 0;

    private void Awake()
    {
        _enemyMovementController = GetComponent<EnemyMovementController>();
        _enemyShootingController = GetComponent<EnemyShootingController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _enemyRagdollActivation = GetComponent<EnemyRagdollActivation>();
        _levelCompletionController = FindFirstObjectByType<LevelCompletionController>();
    }

    public void InitiateDeath()
    {
        _animator.enabled = false;
        _enemyRagdollActivation.ActivateRagdoll();
        _deathSource.Play();

        DeactivateBattleBot();
    }

    private void DeactivateBattleBot()
    {
        _enemyMovementController.enabled = false;
        _enemyShootingController.enabled = false;

        _navMeshAgent.enabled = false;

        if (_deactivationTimes++ < 1) _levelCompletionController.AddKilled();
    }
}
