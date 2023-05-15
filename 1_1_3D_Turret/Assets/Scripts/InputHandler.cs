using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class InputHandler : MonoBehaviour
{
    private PlayerController _playerController;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        var input = Vector3.zero;

        input.x = -Input.GetAxis(VERTICAL);
        input.z = Input.GetAxis(HORIZONTAL);
        
        _playerController.SetInput(-input.x, input.z);
    }
}