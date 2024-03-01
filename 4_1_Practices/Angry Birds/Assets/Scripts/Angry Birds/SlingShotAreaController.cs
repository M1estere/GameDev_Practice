using UnityEngine;

public class SlingShotAreaController : MonoBehaviour
{
    [SerializeField] private LayerMask _slingShotAreaMask;

    public bool WithinSlingshotArea()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Physics2D.OverlapPoint(worldPosition, _slingShotAreaMask))
        {
            return true;
        } else
        {
            return false;
        }
    }
}
