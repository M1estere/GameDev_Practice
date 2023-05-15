using UnityEngine;

public class MovingEnemiesControl : MonoBehaviour
{
    [SerializeField] private GameObject _enemies;
    [SerializeField] private GameObject[] _showEnemies;

    [SerializeField] private float _objectMovementDistance;
    
    private int _index;
    
    public void Next()
    {
        if (_index >= _showEnemies.Length - 1) return;
        
        _index++;
        _enemies.transform.position = new Vector3(_enemies.transform.position.x, _enemies.transform.position.y, _enemies.transform.position.z - _objectMovementDistance);
    }

    public void Previous()
    {
        if (_index <= 0) return;
        
        _index--;
        _enemies.transform.position = new Vector3(_enemies.transform.position.x, _enemies.transform.position.y, _enemies.transform.position.z + _objectMovementDistance);
    }
}
