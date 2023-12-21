using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BulletGFX : MonoBehaviour
{
    private LineRenderer _traceRenderer;

    private Transform _gunTipTransform;

    private void Start()
    {
        _traceRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (_gunTipTransform != null) _traceRenderer.SetPosition(1, _gunTipTransform.position);

        _traceRenderer.SetPosition(0, transform.position);
    }

    public void SetGunTip(Transform _gunTip)
    {
        _gunTipTransform = _gunTip;
    }
}
