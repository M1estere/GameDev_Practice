using UnityEngine;

public class ModelRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 20;

    private string _mouseX = "Mouse X";
    
    private void OnMouseDrag()
    {
        float xRot = Input.GetAxis(_mouseX) * _rotationSpeed * Mathf.Deg2Rad;

        transform.RotateAround(Vector3.up, -xRot);
    }
}
