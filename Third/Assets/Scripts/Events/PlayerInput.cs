using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerController _playerController;
    
    private const string HORIZONTAL = "Horizontal";

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Vector2 input = Vector2.zero;

        input.x = Input.GetAxis(HORIZONTAL);

        _playerController.SetInput(input);
    }
}
