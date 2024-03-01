using UnityEngine;

public class Cutter : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!hit.collider) return;

            if (hit.collider.CompareTag("Link"))
            {
                Destroy(hit.collider.transform.parent.gameObject);
            }
        }
    }
}
