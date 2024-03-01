using System.Collections;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class SlingShotController : MonoBehaviour
{
    [Header("Line Renderers")]
    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;
    [Space(5)]

    [Header("Positions")]
    [SerializeField] private Transform _leftStartPosition;
    [SerializeField] private Transform _rightStartPosition;
    [SerializeField] private Transform _centerPosition;
    [SerializeField] private Transform _idlePosition;
    [SerializeField] private Transform _elasticTransform;
    [Space(5)]

    [Header("General")]
    [SerializeField] private float _maxDistance = 3.5f;
    [SerializeField] private float _shotForce = 10;
    [SerializeField] private float _birdSpawnDelay = 2;
    [SerializeField] private float _elasticDivider = 1.2f;
    [SerializeField] private AnimationCurve _elasticCurve;

    [SerializeField] private SlingShotAreaController _slingShotAreaController;
    [SerializeField] private CameraController _cameraController;
    [Space(5)]

    [Header("Bird")]
    [SerializeField] private BirdController _birdPrefab;
    [SerializeField] private float _positionOffset = 2;

    private Vector2 _slingShotLinesPosition;

    private Vector2 _direction;
    private Vector2 _directionNormalized;

    private bool _clickedWithinArea;
    private bool _birdOnSlingshot;

    private BirdController _currentBird;

    private void Awake()
    {
        _leftLineRenderer.enabled = false;
        _rightLineRenderer.enabled = false;

        DrawBird();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _slingShotAreaController.WithinSlingshotArea())
        {
            _clickedWithinArea = true;

            if (_birdOnSlingshot)
            {
                _cameraController.SwitchToFollow(_currentBird.transform);
            }
        }

        if (Input.GetMouseButton(0) && _clickedWithinArea && _birdOnSlingshot)
        {
            DrawLines();
            RotateMoveBird();
        }

        if (Input.GetMouseButtonUp(0) && _birdOnSlingshot && _clickedWithinArea)
        {
            if (GameController.Instance.HasEnoughShots())
            {
                _clickedWithinArea = false;

                _currentBird.LaunchBird(_direction, _shotForce);
                _birdOnSlingshot = false;
                GameController.Instance.UseShot();

                AnimateSling();

                if (GameController.Instance.HasEnoughShots())
                    StartCoroutine(SpawnBirdDelay());
            }
        }
    }

    private void DrawBird()
    {
        _elasticTransform.DOComplete();
        SetLines(_idlePosition.position);

        Vector2 direction = (_centerPosition.position - _idlePosition.position).normalized;
        Vector2 spawnPosition = (Vector2)_idlePosition.position + direction * _positionOffset;

        _currentBird = Instantiate(_birdPrefab.gameObject, spawnPosition, Quaternion.identity).GetComponent<BirdController>();
        _currentBird.transform.right = direction;

        _birdOnSlingshot = true;
    }

    private void RotateMoveBird()
    {
        _currentBird.transform.position = _slingShotLinesPosition + _directionNormalized * _positionOffset;

        _currentBird.transform.right = _directionNormalized;
    }

    private IEnumerator SpawnBirdDelay()
    {
        yield return new WaitForSeconds(_birdSpawnDelay);

        DrawBird();

        _cameraController.SwitchToIdle();
    }

    private void DrawLines()
    {
        _leftLineRenderer.enabled = true;
        _rightLineRenderer.enabled = true;

        Vector3 inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _slingShotLinesPosition = _centerPosition.position + Vector3.ClampMagnitude(inputPosition - _centerPosition.position, _maxDistance);

        SetLines(_slingShotLinesPosition);

        _direction = (Vector2)_centerPosition.position - _slingShotLinesPosition;
        _directionNormalized = _direction.normalized;
    }

    private void SetLines(Vector2 position)
    {
        _leftLineRenderer.SetPosition(0, position);
        _leftLineRenderer.SetPosition(1, _leftStartPosition.position);

        _rightLineRenderer.SetPosition(0, position);
        _rightLineRenderer.SetPosition(1, _rightStartPosition.position);
    }

    private void AnimateSling()
    {
        _elasticTransform.position = _leftLineRenderer.GetPosition(0);

        float distance = Vector2.Distance(_elasticTransform.position, _centerPosition.position);

        float time = distance / _elasticDivider;

        _elasticTransform.DOMove(_centerPosition.position, time).SetEase(_elasticCurve);
        StartCoroutine(AnimateLines(_elasticTransform, time));
    }

    private IEnumerator AnimateLines(Transform tTransform, float time)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            SetLines(tTransform.position);

            yield return null;
        }
    }
}
