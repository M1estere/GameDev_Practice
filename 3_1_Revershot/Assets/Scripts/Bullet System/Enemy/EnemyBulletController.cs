using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField, Range(5, 25)] private float _traversalSpeed;

    private Vector3 _directionVector;

    public void SetDirectionVector(Vector3 dir) => _directionVector = dir;

    private void Update()
    {
        transform.Translate(_directionVector * _traversalSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerDeath playerDeath))
            playerDeath.InitiateDeath();
        
        Destroy(gameObject);        
    }
}
