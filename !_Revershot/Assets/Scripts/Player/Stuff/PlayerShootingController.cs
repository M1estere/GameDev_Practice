using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    [SerializeField] private LayerMask _enemyLayerMask;

    private void Update()
    {
        if (Time.timeScale != 1) return;

        float x = Screen.width / 2;
        float y = Screen.height / 2;

        Ray ray = _mainCamera.ScreenPointToRay(new Vector3(x, y, 0));
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);

        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, ~_enemyLayerMask))
        {
            BulletDecalController bulletDecalController = hit.collider.gameObject.GetComponentInParent<BulletDecalController>();

            if (bulletDecalController == null) return;

            if (Input.GetMouseButtonDown(0)) InitiateSequence(bulletDecalController);
        }
    }

    private void InitiateSequence(BulletDecalController bulletDecalController)
    {
        bulletDecalController.StartSequence();

        bulletDecalController.enabled = false;
        Destroy(bulletDecalController.gameObject);
    }
}
