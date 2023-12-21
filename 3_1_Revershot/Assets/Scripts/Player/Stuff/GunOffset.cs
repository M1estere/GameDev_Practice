using UnityEngine;

public class GunOffset : MonoBehaviour
{
    [Header("General Setup")]
    [SerializeField] private Vector3 _gunPositionOffset;
    [SerializeField] private Rigidbody _playerRigidbody;
    [Space(5)]

    [Header("Sway Setup")]
    [SerializeField] private float _step = .01f;
    [SerializeField] private float _maxStepDistance = .06f;
    [Space(5)]

    [Header("Sway Rotation Setup")]
    [SerializeField] private float _rotationStep = 4;
    [SerializeField] private float _maxRotationStep = 5;
    [Space(5)]

    [Header("Bobbing Setup")]
    [SerializeField] private float _speedCurve;

    [SerializeField] private Vector3 _travelLimit = Vector3.one * .025f;
    [SerializeField] private Vector3 _bobLimit = Vector3.one * .01f;
    [Space(5)]

    [Header("Bob Rotation Setup")]
    [SerializeField] private Vector3 _multiplier;

    private float CurveSin { get => Mathf.Sin(_speedCurve); }
    private float CurveCos { get => Mathf.Cos(_speedCurve); }

    private readonly float _smooth = 10;
    private readonly float _smoothRotation = 12;

    private Vector3 _bobEulerRotation;
    private Vector3 _bobPosition;

    private Vector3 _swayPosition;
    private Vector3 _swayEulerRotation;

    private Vector2 _walkInput;
    private Vector2 _lookInput;

    private void Update()
    {
        if (Time.timeScale != 1) return;

        GetInput();

        Sway();
        SwayRotation();

        BobOffset();
        BobRotation();

        CompositePositionRotation();
    }

    private void GetInput()
    {
        _walkInput = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")).normalized;
        _lookInput = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
    }

    private void Sway()
    {
        Vector3 invertLook = _lookInput * _step;
        invertLook.x = Mathf.Clamp(invertLook.x, -_maxStepDistance, _maxStepDistance);
        invertLook.y = Mathf.Clamp(invertLook.y, -_maxStepDistance, _maxStepDistance);
    
        _swayPosition = invertLook;
    }

    private void SwayRotation()
    {
        Vector3 invertLook = _lookInput * -_rotationStep;
        invertLook.x = Mathf.Clamp(invertLook.x, -_maxRotationStep, _maxRotationStep);
        invertLook.y = Mathf.Clamp(invertLook.y, -_maxRotationStep, _maxRotationStep);

        _swayEulerRotation = new Vector3(invertLook.x, invertLook.y, invertLook.z);
    }

    private void BobOffset()
    {
        _speedCurve += Time.deltaTime * _playerRigidbody.velocity.magnitude + .01f;

        _bobPosition.x = (CurveCos * _bobLimit.x) - (_walkInput.x * _travelLimit.x);
        _bobPosition.y = (CurveSin * _bobLimit.y) - (_walkInput.y * _travelLimit.y);
        _bobPosition.z = - (_walkInput.y * _travelLimit.z);
    }

    private void BobRotation()
    {
        _bobEulerRotation.x = (_walkInput != Vector2.zero ? _multiplier.x * (Mathf.Sin(2 * _speedCurve)) : _multiplier.x * (Mathf.Sin(2 * _speedCurve) / 2));
        _bobEulerRotation.y = (_walkInput != Vector2.zero ? _multiplier.y * CurveCos : 0);
        _bobEulerRotation.z = (_walkInput != Vector2.zero ? _multiplier.z * CurveCos * _walkInput.x : 0);
    }

    private void CompositePositionRotation()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, (_swayPosition + _bobPosition) + _gunPositionOffset, Time.deltaTime * _smooth);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(_swayEulerRotation) * Quaternion.Euler(_bobEulerRotation), Time.deltaTime * _smoothRotation);
    }
}
