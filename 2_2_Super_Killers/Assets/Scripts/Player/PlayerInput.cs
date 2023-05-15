using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private enum ControlType { Keyboard, Joystick }

    [SerializeField] private ControlType _controlType;
    [Space(5)]
    
    [SerializeField] private Joystick _controlJoystick;
    
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private PlayerMovement _playerMovement;
    
    private void Awake() => _playerMovement = GetComponent<PlayerMovement>();

    private void Update()
    {
        Vector3 moveVector = Vector3.zero;
        switch (_controlType)
        {
            case ControlType.Keyboard:
            {
                float horizontalInput = Input.GetAxis(HORIZONTAL);
                float verticalInput = Input.GetAxis(VERTICAL);

                moveVector = new Vector3(horizontalInput, 0, verticalInput);
                break;
            }
            case ControlType.Joystick:
            {
                float horizontalInput = _controlJoystick.Horizontal;
                float verticalInput = _controlJoystick.Vertical;
            
                moveVector = new Vector3(-verticalInput, 0, horizontalInput);
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }

        Matrix4x4 matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 skewedInput = matrix.MultiplyPoint3x4(moveVector);
        
        SetInput(skewedInput);
    }

    private void SetInput(Vector3 resultMovement) => _playerMovement.SetInput(resultMovement);
}
