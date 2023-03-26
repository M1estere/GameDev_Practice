using UnityEngine;

public class BlocksSpawner : MonoBehaviour
{
    [Header("Spawned Blocks Parameters")]
    [SerializeField] private GameObject[] _blockVariants;
    [Space(5)]
    
    [SerializeField] private Transform _blocksParent;
    [Space(2)]
    
    [SerializeField] private Transform _startLastBlock;
    
    private Transform _currentLastBlock;
    private float _blockLength = 30;

    private void Awake() => _currentLastBlock = _startLastBlock;

    public void SpawnBlock()
    {
        int index = Globals.Random.Next(0, _blockVariants.Length);
        Vector3 point = _currentLastBlock.position + new Vector3(_blockLength, 0, 0);
        
        Transform block = Instantiate(_blockVariants[index], point, Quaternion.identity, _blocksParent).transform;
        _currentLastBlock = block;
    }
}
