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

        Vector3 thisPosition = transform.position;
        float xValue = Mathf.Lerp(thisPosition.x, targetVector.x, _positionLerpSpeed * Time.deltaTime);
        float zValue = Mathf.Lerp(thisPosition.z, targetVector.z, _positionLerpSpeed * Time.deltaTime);

        thisPosition = new Vector3(xValue, thisPosition.y, zValue);
        transform.position = thisPosition;
    }
}
