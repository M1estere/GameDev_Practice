using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private BlocksSpawner _blocksSpawner;

    private void Awake() => _blocksSpawner = FindObjectOfType<BlocksSpawner>();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController playerController) == false) return;
        
        _blocksSpawner.SpawnBlock();
        Destroy(gameObject);
    }
}
