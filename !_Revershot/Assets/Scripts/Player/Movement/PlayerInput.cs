using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
{
    private PlayerController _playerController;

    private const string HORIZONTAL_INPUT_STRING = "Horizontal";
    private const string VERTICAL_INPUT_STRING = "Vertical";

    private void Awake() => _playerController = GetComponent<PlayerController>();

    private void Update()
    {
        if (Time.timeScale < 1) return;

        float verticalInputValue = Input.GetAxis(VERTICAL_INPUT_STRING);
        float horizontalInputValue = Input.GetAxis(HORIZONTAL_INPUT_STRING);

        Vector2 inputVector = new Vector2(verticalInputValue, horizontalInputValue);

        _playerController.SetInput(inputVector);
    }
}
