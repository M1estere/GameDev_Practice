using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private float moveSpeed;

    private Vector2 _mousePosition;
    private Vector3 _moveVector;

    private void Update() => transform.Translate(_moveVector * Time.deltaTime * moveSpeed, Space.World);
    
    public void SetInput(Vector3 inputVector) => _moveVector = inputVector;
}