using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _lookTarget;
    [Space(3)]
    
    [SerializeField] private float _turnSpeed;
    
    private void Update()
    {
        var dir = _lookTarget.position - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _turnSpeed).eulerAngles;
        
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
