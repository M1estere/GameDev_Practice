using UnityEngine;

public class DialogueCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private void LateUpdate()
    {
        transform.position = new Vector3(_playerTransform.position.x, transform.position.y, -10);
    }
}
