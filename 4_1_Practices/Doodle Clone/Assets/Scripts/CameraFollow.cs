using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;

    private void LateUpdate()
    {
        if (_targetTransform.position.y > transform.position.y)
        {
            Vector3 newPos = new Vector3(transform.position.x, _targetTransform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}
