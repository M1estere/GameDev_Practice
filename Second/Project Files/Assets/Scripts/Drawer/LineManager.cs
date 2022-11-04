using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField, Tooltip("For looking for mouse")] private Camera _mainCamera;
    [Space(5)]
    
    [SerializeField] private GameObject _linePrefab;
    [Space(3)]
    
    [SerializeField] private Canvas _localUI;
    
    private LineRenderer _currentLineRenderer;
    private Draw _lineScript;

    private int _currentLayer;
    
    private void Awake()
    {
        _linePrefab.GetComponent<LineRenderer>().sortingOrder = 0;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) ToggleLocalUI();
        
        if (Input.GetMouseButtonDown(0))
        {
            var newLine = Instantiate(_linePrefab);

            _currentLineRenderer = _linePrefab.GetComponent<LineRenderer>();
            _currentLineRenderer.sortingOrder = _currentLayer;
            _currentLayer++;
            
            _lineScript = newLine.GetComponent<Draw>();
        }

        if (Input.GetMouseButtonUp(0))
            _lineScript = null;

        if (_lineScript != null)
        {
            var mousePosition = new Vector3
            {
                x = Input.mousePosition.x,
                y = Input.mousePosition.y,
                z = 0
            };

            var output = _mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _mainCamera.nearClipPlane));
            _lineScript.UpdateLine(new Vector2(output.x, output.y));
        }
    }

    private void ToggleLocalUI()
    {
        _localUI.enabled = !_localUI.enabled;
    }
}