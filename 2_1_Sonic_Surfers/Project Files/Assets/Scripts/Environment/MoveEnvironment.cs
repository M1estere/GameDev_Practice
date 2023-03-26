using UnityEngine;

public class MoveEnvironment : MonoBehaviour
{
    private void Update()
    {
        Vector3 moveVector = new Vector3(-1, 0, 0);
        transform.Translate(moveVector * Globals.CurrentMoveSpeed * Time.deltaTime);
    }
}