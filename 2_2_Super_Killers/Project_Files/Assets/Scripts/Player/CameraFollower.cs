using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField, Range(1, 6)] private float _positionLerpSpeed;
    [Space(5)]
    
    [SerializeField, Tooltip("+")] private Vector3 _offsetVector;

    private Transform _playerObject;

    private void Start() => _playerObject = FindObjectOfType<PlayerMovement>().transform;

    private void LateUpdate()
    {
        if (_playerObject == null) return;
        Vector3 targetVector = _playerObject.position + _offsetVector;

        float xValue = Mathf.Lerp(transform.position.x, targetVector.x, _positionLerpSpeed * Time.deltaTime);
        float zValue = Mathf.Lerp(transform.position.z, targetVector.z, _positionLerpSpeed * Time.deltaTime);

        transform.position = new Vector3(xValue, transform.position.y, zValue);
    }
}
