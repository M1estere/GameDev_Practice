using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
            Destroy(other.transform.parent.gameObject);
        
        if (other.gameObject.TryGetComponent(out Enemy enemy))
            Destroy(other.gameObject);
    }
}
